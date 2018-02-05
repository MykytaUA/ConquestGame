using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
namespace ConsoleApp4
{
    public static class Master
    {
        public static void Start()
        {
            Console.WriteLine("Приветствую Вас великий правитель!");
            Console.WriteLine("Я ваш слуга Даймон, я буду исполнять все Ваши указания");
        }
        public static void Rules()
        {
            Console.WriteLine("Цель игры - победить две соседние державы: Швабрию и Тильзон, путем захвата всего их свободного населения");
            Console.WriteLine("Вы можете создавать свою армию и распределять их на отряды атаки и защиты");
            Console.WriteLine("Во время атаки, в случае победы, каждый оставшийся в живых воин забирает по одному пленному в счет свободного населения своего государства");
            Console.WriteLine("Для создания воина требуется 2 единицы золота и одна единица еды");
            Console.WriteLine("Для создания бизнесмена требуется одна единица еды, он приносит три единицы золота");
            Console.WriteLine("Для создания свободного человека не требуется ничего, он кормит сам себя и приносит еще две единицы еды");
        }
        public static void Info(AbstractCountry t)
        {
            if (t.Alive == true)
            {
                Console.WriteLine("В Королевстве {0} {1} единиц еды, {2} тонн золота и проживает {3} свободных людей", t.CountryName, t.Food, t.Gold, t.FreePeople);
            } }
        public static void EnemiesInfo(AbstractCountry t)
        {
            Console.WriteLine(t.ToString());
            Master.Info(t);
        }
        static void SaveCountry(AbstractCountry obj,string filename)
        {
            // создаем объект BinaryFormatter
            BinaryFormatter formatter = new BinaryFormatter();
            // получаем поток, куда будем записывать сериализованный объект
            using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, obj);

                Console.WriteLine("Объект сериализован");
            }
        }
        public static void SaveGame(AbstractCountry t1,AbstractCountry t2,AbstractCountry t3)
        {
            SaveCountry(t1,"MyCountry");
            SaveCountry(t2, "Shvabria");
            SaveCountry(t3, "Tilzon");

        }
        public static AbstractCountry LoadCountry(string filename)
        {
            // десериализация из файла people.dat
            BinaryFormatter formatter = new BinaryFormatter();
            AbstractCountry obj;
            using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
            {
                 obj = (AbstractCountry)formatter.Deserialize(fs);

                Console.WriteLine("Объект десериализован");
            }
            return obj; 
        }
        public static void LoadGame(AbstractCountry t1, AbstractCountry t2, AbstractCountry t3)
        {
           t1=LoadCountry("MyCountry");
           t2= LoadCountry("Shvabria");
           t3= LoadCountry("Tilzon");

        }
        //static void Attack(AbstractCountry t)
        //{
        //    t.EnemyCountry
        //}
        public static void TurnAI(this AbstractCountry t, int n)
        {
            Random rnd = new Random(n);
            t.MaxArmy = Math.Min((t.Gold / 2),(Math.Min((t.Food / 2), t.FreePeople)));
            t.AttackOrNot = rnd.Next(1, 2);

            t.Army = rnd.Next(0, t.MaxArmy);
            t.Food -= t.Army;

            t.Gold -= (t.Army * 2);

            if (t.AttackOrNot == 1)
            {
                t.EnemyCountry = rnd.Next(0, 4);
                t.ArmyOnAttack = rnd.Next(0, t.Army);

            }
            t.ArmyOnDef = t.Army - t.ArmyOnAttack;
            t.FreePeople -= t.Army;
            t.MaxBusiness = Math.Min((t.Food / 2), t.FreePeople);

            t.BusinessPeople = rnd.Next(0, t.MaxBusiness);
            t.Gold += (t.BusinessPeople * 3);
            t.Food -= t.BusinessPeople;
            t.FreePeople -= t.BusinessPeople;
            t.Food += (t.FreePeople * 2);
        }
        public static void MyTurn(this AbstractCountry t)
        {
            t.MaxArmy = Math.Min((t.Gold / 2), (Math.Min((t.Food / 2), t.FreePeople)));
            Info(t);
            Console.WriteLine("Максимально возможная армия: {0}", t.MaxArmy);
            Console.WriteLine("Милорд, я думаю что нам стоит показать нашим соседям кто здесь главный Да-1 Нет-2");
            t.AttackOrNot = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Ваше превoсходительство,насколько большую армию вы хотите?");
            t.Army = Int32.Parse(Console.ReadLine());
            t.Food -= t.Army;

            t.Gold -= (t.Army * 2); 
            if (t.AttackOrNot == 1)
            {
                Console.WriteLine("Милорд,кого из глупцов вы хотите наказать? Швамбрию-1 Тильзон-2");                
                t.EnemyCountry = Int32.Parse(Console.ReadLine());
                Console.WriteLine("Милорд,сколько людей на бойню пошлем из {0}", t.Army);
                t.ArmyOnAttack = Int32.Parse(Console.ReadLine());

            }
            t.ArmyOnDef = t.Army - t.ArmyOnAttack;
            t.FreePeople -= t.Army;
            t.MaxBusiness = Math.Min((t.Food / 2), t.FreePeople);
            Info(t);
            Console.WriteLine("Милорд,вы можете отправить часть населения зарабатывать деньги для нашей казны, как много людей Вы отправите из {0} человек?", t.MaxBusiness);
            t.BusinessPeople = Int32.Parse(Console.ReadLine());
            t.Gold += (t.BusinessPeople * 3);
            t.Food -= t.BusinessPeople;
            t.FreePeople -= t.BusinessPeople;
            t.Food += (t.FreePeople * 2);
            
        }
        public static void EndTurn(AbstractCountry t1, AbstractCountry t2, AbstractCountry t3)//адски костыльный спагетти-код :(
        {
            t1.AllPeople = t1.FreePeople + t1.BusinessPeople;
            t2.AllPeople = t2.FreePeople + t2.BusinessPeople;
            t3.AllPeople = t3.FreePeople + t3.BusinessPeople;
            if (t1.Alive == true)
            {
                if (t1.AttackOrNot == 1)
                {
                    if (t1.EnemyCountry == 1)
                    {
                        if (t2.ArmyOnDef >= t1.ArmyOnAttack)
                        {
                            t2.ArmyOnDef -= t1.ArmyOnAttack;
                            Console.WriteLine("У нас не получилось победить Швамбрию");
                        }
                        else if(t2.Alive==true)
                        {
                            t1.ArmyOnAttack -= t2.ArmyOnDef;
                            t1.FreePeople += (t1.ArmyOnAttack * 2);
                            t2.FreePeople -= t1.ArmyOnAttack;
                            Console.WriteLine("У нас получилось победить Швамбрию, мы захватили {0} населения", t1.ArmyOnAttack);
                            if (t2.FreePeople < 0) { Console.WriteLine("Швабрия повержена"); t2.Alive = false; }
                        }
                        else
                        {
                            Console.WriteLine("Милорд! Швабрия уже повержена, Ваши войска возвращаются домой целые и невредимые");
                        }
                    }
                    else if
                     (t1.EnemyCountry == 2)
                    {
                        if (t3.ArmyOnDef >= t1.ArmyOnAttack)
                        {
                            t3.ArmyOnDef -= t1.ArmyOnAttack;
                            Console.WriteLine("У нас не получилось победить Тильзон");
                        }
                        else if (t3.Alive == true)
                        {
                            t1.ArmyOnAttack -= t3.ArmyOnDef;
                            t1.FreePeople += (t1.ArmyOnAttack * 2);
                            t3.FreePeople -= t1.ArmyOnAttack;
                            Console.WriteLine("У нас получилось победить Тильзон, мы захватили {0} населения", t1.ArmyOnAttack);
                            if (t3.FreePeople < 0) { Console.WriteLine("Тильзон повержен"); t3.Alive = false; }

                        }

                    }
                }
            }

            if (t2.Alive == true)
            {
                if (t2.AttackOrNot == 1)
                {
                    if (t2.EnemyCountry == 0)
                    {
                        if (t1.ArmyOnDef >= t2.ArmyOnAttack)
                        {
                            t1.ArmyOnDef -= t2.ArmyOnAttack;
                            Console.WriteLine("Швамбрия пыталась атаковать Вас, но увы, атаку постигла неудача");
                        }
                        else if (t1.Alive == true)
                        {
                            t2.ArmyOnAttack -= t1.ArmyOnDef;
                            t2.FreePeople += (t2.ArmyOnAttack * 2);
                            t1.FreePeople -= t2.ArmyOnAttack;
                            Console.WriteLine("Швабрии получилось победить нас, они захватили {0} населения", t2.ArmyOnAttack);
                            if (t1.FreePeople < 0) { Console.WriteLine("Мы повержены"); t1.Alive = false; }

                        }
                    }
                    else

                    {
                        if (t3.ArmyOnDef >= t2.ArmyOnAttack)
                        {
                            t3.ArmyOnDef -= t2.ArmyOnAttack;
                            Console.WriteLine("Швабрия пыталась атаковать Тильзон, но увы, атаку постигла неудача");
                        }
                        else if(t3.Alive == true)
                        {
                            t2.ArmyOnAttack -= t3.ArmyOnDef;
                            t2.FreePeople += (t2.ArmyOnAttack * 2);
                            t3.FreePeople -= t2.ArmyOnAttack;
                            Console.WriteLine("Швабрии получилось победить Тильзон, они захватили {0} населения", t2.ArmyOnAttack);
                            if (t3.FreePeople < 0) { Console.WriteLine("Тильзон повержен"); t3.Alive = false; }

                        }

                    }
                }
            }
            if (t3.Alive == true)
            {
                if (t3.AttackOrNot == 1)
                {
                    if (t3.EnemyCountry == 0)
                    {
                        if (t1.ArmyOnDef >= t3.ArmyOnAttack)
                        {
                            t1.ArmyOnDef -= t3.ArmyOnAttack;
                            Console.WriteLine("Тильзон пытался атаковать нас, но увы, атаку постигла неудача");
                        }
                        else if (t1.Alive == true)
                        {
                            t3.ArmyOnAttack -= t1.ArmyOnDef;
                            t3.FreePeople += (t3.ArmyOnAttack * 2);
                            t1.FreePeople -= t3.ArmyOnAttack;
                            Console.WriteLine("Тильзону получилось победить нас, они захватили {0} населения", t3.ArmyOnAttack);
                            if (t1.FreePeople < 0) { Console.WriteLine("Мы повержены"); t1.Alive = false; }

                        }
                    }
                    else

                    {
                        if (t2.ArmyOnDef >= t3.ArmyOnAttack)
                        {
                            t2.ArmyOnDef -= t3.ArmyOnAttack;
                            Console.WriteLine("Тильзон пытался атаковать Швабрию, но увы, атаку постигла неудача");

                        }
                        else if (t2.Alive == true)
                        {
                            t3.ArmyOnAttack -= t2.ArmyOnDef;
                            t3.FreePeople += (t3.ArmyOnAttack * 2);
                            t2.FreePeople -= t3.ArmyOnAttack;
                            Console.WriteLine("Тильзону получилось победить Швабрию, они захватили {0} населения", t3.ArmyOnAttack);
                            if (t2.FreePeople < 0) { Console.WriteLine("Швабрия повержена"); t2.Alive = false; }

                        }

                    }
                }
            }
            t1.FreePeople += (t1.ArmyOnDef + t1.BusinessPeople);
            t2.FreePeople += (t2.ArmyOnDef + t2.BusinessPeople);
            t3.FreePeople += (t3.ArmyOnDef + t3.BusinessPeople);
            

        }
    } }
