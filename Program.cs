using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
namespace ConsoleApp4
{   class Program
        {
            static void Main(string[] args)
            {
                while (true)
                {
                MyCountry MC=null;
                OpponentShvambria sh = new OpponentShvambria();
                OpponentTilzon tz = new OpponentTilzon();
                bool TempOfCycleTwo = true;
                do
                {
                    Console.WriteLine("");
                    Console.WriteLine("Государь! Вы можете:");
                    Console.WriteLine("Начать новую игру - 1");
                    Console.WriteLine("Загрузить игру - 2");
                    Console.WriteLine("Сохранить игру - 3");
                    Console.WriteLine("Покинуть игру - 4");
                    Console.WriteLine("Прочесть правила игры-5");
                    Console.WriteLine("Совершить следующий ход - 6");
                    Console.WriteLine(" ");

                    int Menu = Int32.Parse(Console.ReadLine());
                    switch (Menu)
                    {
                        case 1:

                        Master.Start();  //вспомогательный статический метод "ведущего" игры
                        Console.WriteLine("Назовите Вашу страну");
                        string CountryName = Console.ReadLine();
                        MC = new MyCountry(CountryName);
                            goto case 6;
                        case 2:
                        MC = (MyCountry)Master.LoadCountry("MyCountry");
                        sh = (OpponentShvambria)Master.LoadCountry("Shvabria");
                        tz = (OpponentTilzon)Master.LoadCountry("Tilzon"); break;
                        case 3:
                        Master.SaveGame(MC, sh, tz);
                        break;

                    case 4:
                            Environment.Exit(0);
                            break;
                        case 5:
                            Master.Rules();
                            continue;
                        case 6:
                            if (sh.Alive == true)
                            {
                                Master.Info(sh);
                                sh.TurnAI(12);
                            }
                            if (tz.Alive == true)
                            {
                                Master.Info(tz);
                                tz.TurnAI(15);
                            }
                            if (MC.Alive == false)
                            {
                                Console.WriteLine("GameOver");
                                TempOfCycleTwo = false;
                            }
                            Master.EndTurn(MC, sh, tz); break;
                    }   
                             MC.MyTurn();
                             } while (TempOfCycleTwo); 
                    }
            }
        }
    }


