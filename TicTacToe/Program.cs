using System.Numerics;

public class Player{
    private string Name;
    public bool Played = false;
    public int select;

    public Player (string _name){
        Name = _name;
    }

    public void Play(){
        while(true){
            if (int.TryParse(Console.ReadLine(), out select)){
                if(select<0 || select>8){
                    Console.WriteLine("Fuera del rango de la tabla");
                } else {
                    break;
                }
            }else{
                Console.WriteLine("\nEntrada inválida. debe ser un valor numerico.\n");
            }
        }
        Played = true;
    }
}

public class Opponent{
    public bool Played = false;
    public int select;
    public void Play(Player _player){
        Random random = new Random();
        select = random.Next(8, _player.select - 1);
        Played = true;
    }
}

public class TicTacToe{
    private Opponent opp;
    private Player player;
    private bool PlayerWon;
    public int Selected;
    private List<List<char>> Table;

    private void CreateTable(){
        for(int i=0; i<3; ++i){
            this.Table.Add(new List<char>());
            for(int j=0; j<3; ++j){
                this.Table[i].Add('-');
            }
        }
    }

    private void ShowTable(){
        for(int i=0; i<3; ++i){
            for(int j=0; j<3; ++j){
                Console.Write(Table[i][j]);
            }
            Console.WriteLine();
        }
    }

    public void introduceSymbol(int selected_cell){
        int cell = 0;
        for(int i=0; i<3; ++i){
            for(int j=0; j<3; ++j){
                if(selected_cell == cell){
                    if(player.Played){
                        Table[i][j] = 'O';
                    } else if(opp.Played){
                        Table[i][j] = 'X';
                    } else {
                        Table[i][j] = '-';
                    }
                }
            }
            Console.WriteLine();
        }
    }

    public void Game(){
        CreateTable();
        ShowTable();

        Console.WriteLine("WELCOME TO TIC-TAC-TOE.");
        Console.WriteLine("-----------------------");
        Console.Write("Name: ");
        string name;
        name = Console.ReadLine();
        Player _player = new Player(name);
        player = _player;
        Opponent _opp = new Opponent();
        opp = _opp;

        
    }
}