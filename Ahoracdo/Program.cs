List<string> words = new List<string> {"Aventurero", 
                                       "Bibleoteca", 
                                       "Caleidoscopio", 
                                       "Desafortunadamente", 
                                       "Elefante",
                                       "Fotografía", 
                                       "Guitarra", 
                                       "Hipopótamo", 
                                       "Jardinería", 
                                       "Laboratorio",
                                       "Mariposa", 
                                       "Naranja", 
                                       "Oceánico", 
                                       "Paraguas", 
                                       "Quiosco"};

Random randomizer = new Random();
int listElem = randomizer.Next(0, words.Count - 1);

Word word = new Word(words[listElem]);
HangmanGame game = new HangmanGame(word);
game.Play();

class Word{
    public string Content { get; private set; }
    public string CoveredContent { get; private set; }

    public Word(string content){
        Content = content;
        CoveredContent = GenerateCoveredContent(content);
    }

    private string GenerateCoveredContent(string content){
        Random randomizer = new Random();
        string coveredWord = "";

        for (int i = 0; i < content.Length; ++i){
            bool coverLetter = randomizer.Next(2) == 0;
            if (coverLetter){
                coveredWord += '_';
            }else{
                coveredWord += content[i];
            }
        }

        return coveredWord;
    }

    public bool GuessLetter(char letter){
        bool guessed = false;
        char[] coveredWordArray = CoveredContent.ToCharArray();

        for (int i = 0; i < Content.Length; ++i){
            if (coveredWordArray[i] == Content[i]){
                continue;
            } else if (letter == Content[i]){
                guessed = true;
                coveredWordArray[i] = letter;
            }
        }

        CoveredContent = new string(coveredWordArray);
        return guessed;
    }

    public bool IsGuessed(){
        return CoveredContent == Content;
    }
}

class HangmanGame
{
    private Word word;
    private int failedAttempts;
    private const int maxAttempts = 6;

    public HangmanGame(Word word)
    {
        this.word = word;
        failedAttempts = 0;
    }

    public void Play()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("¡Juguemos al ahorcado!\n");
        Console.ResetColor();

        while (!word.IsGuessed() && failedAttempts < maxAttempts)
        {
            Console.WriteLine("\n" + word.CoveredContent + "\n");
            Console.Write("\nAdivina la letra: ");
            ConsoleKeyInfo input = Console.ReadKey();
            char guessedLetter = input.KeyChar;

            if (!word.GuessLetter(guessedLetter))
            {
                failedAttempts++;
                Console.Write("\n");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n¡Incorrecto! Prueba otra vez.");
                Console.WriteLine("Número de intentos: " + (maxAttempts - failedAttempts));
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\n");
                Console.WriteLine("\n¡Correcto!\n");
                Console.ResetColor();
            }
        }

        if (word.IsGuessed()){
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n" + word.CoveredContent);
            Console.WriteLine("\n¡Felicidades, Ganaste! Ten un corazón");
            Console.WriteLine("  /)/)");
            Console.WriteLine("( . .)");
            Console.WriteLine("( づ♡");
            Console.ResetColor();
        }else{
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n¡Perdiste! Ten esto por matarlo");
            Console.WriteLine("  /)/)");
            Console.WriteLine("( . .)");
            Console.WriteLine("(.l. v)");
            Console.ResetColor();
        }
    }
}