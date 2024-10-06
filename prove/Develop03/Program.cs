using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

public class ScriptureWord
{
    private string _word;
    private bool _isHidden;

    public ScriptureWord(string word)
    {
        _word = word;
        _isHidden = false;
    }

    public void Hide()
    {
        _isHidden = true;
    }

    public bool IsHidden()
    {
        return _isHidden;
    }

    public override string ToString()
    {
        return _isHidden ? new string('_', _word.Length) : _word;
    }
}

public class ScriptureReference
{
    private string _book;
    private int _chapter;
    private int _startVerse;
    private int _endVerse;

    public ScriptureReference(string reference)
    {
        ParseReference(reference);
    }

    private void ParseReference(string reference)
    {
        string[] parts = reference.Split(' ');
        _book = parts[0];
        string[] chapterVerse = parts[1].Split(':');
        _chapter = int.Parse(chapterVerse[0]);
        string[] verses = chapterVerse[1].Split('-');
        _startVerse = int.Parse(verses[0]);
        _endVerse = verses.Length > 1 ? int.Parse(verses[1]) : _startVerse;
    }

    public override string ToString()
    {
        if (_startVerse == _endVerse)
        {
            return $"{_book} {_chapter}:{_startVerse}";
        }
        else
        {
            return $"{_book} {_chapter}:{_startVerse}-{_endVerse}";
        }
    }
}

public class Scripture
{
    private ScriptureReference _reference;
    private List<ScriptureWord> _words;

    public ScriptureReference Reference => _reference; // Adicione esta linha


    public Scripture(string reference, string text)
    {
        _reference = new ScriptureReference(reference);
        _words = text.Split(' ').Select(word => new ScriptureWord(word)).ToList();
    }

    public void HideRandomWord()
    {
        var visibleWords = _words.Where(w => !w.IsHidden()).ToList();
        if (visibleWords.Any())
        {
            int index = new Random().Next(visibleWords.Count);
            visibleWords[index].Hide();
        }
    }

    public bool AllWordsHidden()
    {
        return _words.All(w => w.IsHidden());
    }

    public override string ToString()
    {
        return $"{_reference}\n{string.Join(" ", _words)}";
    }
}

public class ScriptureManager
{
    private const string FILE_PATH = "scriptures.txt";

    public static void SaveScripture(string reference, string text)
    {
        using (StreamWriter writer = File.AppendText(FILE_PATH))
        {
            writer.WriteLine($"{reference}|{text}");
        }
        Console.WriteLine("Escritura salva com sucesso!");
    }

    public static List<Scripture> LoadScriptures()
    {
        List<Scripture> scriptures = new List<Scripture>();
        if (File.Exists(FILE_PATH))
        {
            string[] lines = File.ReadAllLines(FILE_PATH);
            foreach (string line in lines)
            {
                string[] parts = line.Split('|');
                scriptures.Add(new Scripture(parts[0], parts[1]));
            }
        }
        return scriptures;
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("1. Inserir nova escritura");
            Console.WriteLine("2. Usar escritura existente");
            Console.WriteLine("3. Sair");
            Console.Write("Escolha uma opção: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    InsertNewScripture();
                    break;
                case "2":
                    UseExistingScripture();
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine("Opção inválida. Pressione qualquer tecla para continuar.");
                    Console.ReadKey();
                    break;
            }
        }
    }

    private static void InsertNewScripture()
    {
        Console.Write("Digite a referência da escritura (ex: João 3:16): ");
        string reference = Console.ReadLine();

        Console.Write("Digite o texto da escritura: ");
        string text = Console.ReadLine();

        Console.Write("Deseja salvar esta escritura? (S/N): ");
        if (Console.ReadLine().ToUpper() == "S")
        {
            ScriptureManager.SaveScripture(reference, text);
        }

        UseScripture(new Scripture(reference, text));
    }

    private static void UseExistingScripture()
    {
        List<Scripture> scriptures = ScriptureManager.LoadScriptures();
        if (scriptures.Count == 0)
        {
            Console.WriteLine("Nenhuma escritura salva. Pressione qualquer tecla para continuar.");
            Console.ReadKey();
            return;
        }

        for (int i = 0; i < scriptures.Count; i++)
        {
        Console.WriteLine($"{i + 1}. {scriptures[i].Reference}"); // Modificado aqui
        }

        Console.Write("Escolha uma escritura: ");
        if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= scriptures.Count)
        {
            UseScripture(scriptures[index - 1]);
        }
        else
        {
            Console.WriteLine("Escolha inválida. Pressione qualquer tecla para continuar.");
            Console.ReadKey();
        }
    }

    private static void UseScripture(Scripture scripture)
    {
        while (!scripture.AllWordsHidden())
        {
            Console.Clear();
            Console.WriteLine(scripture);
            Console.WriteLine("\nPressione Enter para continuar ou digite 'sair' para terminar.");

            string input = Console.ReadLine();
            if (input.ToLower() == "sair")
            {
                break;
            }

            scripture.HideRandomWord();
        }

        Console.Clear();
        Console.WriteLine(scripture);
        Console.WriteLine("\nTodas as palavras foram ocultadas ou você escolheu sair. Pressione qualquer tecla para continuar.");
        Console.ReadKey();
    }
}