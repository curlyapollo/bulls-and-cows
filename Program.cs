﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Channels;

namespace peer1
{
    /// <summary>
    /// Класс, где реализована игра "Быки и коровы".
    /// </summary>
    class Program
    {
        static Random rnd = new Random();

        /// <summary>
        /// Эта функция проверяет, является ли число, удовлетворяющим правилам игры.
        /// </summary>
        /// <param name="num">Число, которое мы проверяем.</param>
        /// <returns>Возвращает, верно ли число.</returns>
        static bool IsRightNumber(string num)
        {
            // Создаем сет для цифр заданного числа.
            HashSet<int> figures_of_num = new HashSet<int>(); 
            // Убираем ведущие нули.
            long.Parse(num).ToString(); 
            for (int i = 0; i < num.Length; i++)
            {
                //Добавляем в сет цифры.
                figures_of_num.Add(num[i]); 
            }
            // Т.к. в сете все элементы встречаются один раз, то если их кол-во == длине => в числе все цифры уникальны.
            return figures_of_num.Count == num.Length; 
        }


        /// <summary>
        /// Эта функция выполняет основную цель игры: находит колиество быков и коров.
        /// </summary>
        /// <param name="num1">Первое сравниваемое число.</param>
        /// <param name="num2">Второе сравниваемое число.</param>
        /// <returns>Возвращает количество быков и коров.</returns>
        static string Compare(string num1, string num2)
        {
            int bulls = 0;
            int cows = 0;
            HashSet<int> figures_of_num1 = new HashSet<int>();
            HashSet<int> figures_of_num2 = new HashSet<int>();
            for (int i = 0; i < num1.Length; i++)
            {
                // Посимвольное сравнение двух чисел.
                if (num1[i] == num2[i]) 
                {
                    bulls++;
                }

                figures_of_num1.Add(num1[i]);
                figures_of_num2.Add(num2[i]);
            }
            // Общие цифры двух чисел.
            figures_of_num1.IntersectWith(figures_of_num2);
            cows = figures_of_num1.Count - bulls;
            return $"Быков: {bulls}, Коров: {cows}";
        }
        
        
        /// <summary>
        /// Функция, генерирующая n-значное число.
        /// </summary>
        /// <param name="len_num">Длина числа.</param>
        /// <returns>Возвращает сгенерированное число.</returns>
        static string Construct(int len_num)
        {
            string hid_num = rnd.Next(1, 10).ToString();
            for (int i = 0; i < len_num - 1; i++)
            {
                // Посимвольное генерирование n-значного числа.
                hid_num += rnd.Next(0, 10); 
            }
            return hid_num;
        }
        
        
        /// <summary>
        /// Получение числа, заданного компьютером.
        /// </summary>
        /// <param name="len_num">Длина числа.</param>
        /// <returns>Возвращает загаданное число.</returns>
        static string OurNum(int len_num)
        {
            string hid_num;
            do
            {
                hid_num = Construct(len_num); 
            //Генерирование чисел, пока не попадется число, удовлетворяющее правилам.
            } while (IsRightNumber(hid_num) == false); 
            return hid_num;
        }

        
        /// <summary>
        ///Фунцкция, взаимодействующая с игроком.
        /// </summary>
        static void InteractionWithUser()
        {
            //Загаданное число.
            string hid_num; 
            int len_num;
            //Число, вводимое игроком.
            long num_of_user;
            Console.WriteLine("Какой длины будет наше число?");
            do
            {
                Console.Write("Введи, пожалуйста, число от 1 до 10.\nДлина: ");
            //Ввод корректной длины.
            } while (!int.TryParse(Console.ReadLine(), out len_num) || len_num > 10 || len_num < 1); 
            //Генерируем загаданное число.
            hid_num = OurNum(len_num); 
            do
            {
                Console.Write("Введи своё число с разными цифрами: ");
                //Проверяем на корректность.
                while (!long.TryParse(Console.ReadLine(), out num_of_user) || 
                       num_of_user.ToString().Length != len_num ||
                       IsRightNumber(num_of_user.ToString()) == false || 
                       long.Parse(num_of_user.ToString()) < 0) 
                {
                    Console.Write(@"Твоё число не удовлетворяет правилам игры! Попробуй ещё раз!
Введи своё число с разными цифрами: ");
                }
                //Ищем быков и коров.
                Console.WriteLine(Compare(num_of_user.ToString(), hid_num)); 
                //Выходим из цикла, если победили
            } while (Compare(num_of_user.ToString(), hid_num) != $"Быков: {len_num}, Коров: 0"); 
            Console.WriteLine("Поздравляю! Ты отгадал(а) число.");
            Console.WriteLine("Если хочешь сыграть ещё раз, напиши да. Если не хочешь, то нажми что угодно другое.");
        }
        
        
        /// <summary>
        /// Правила.
        /// </summary>
        /// <returns>Возвращает правила.</returns>
        static string Rules()
        {
            string rules;
            rules = @"Привет, мой дорогой сокурсник! Ну или моя прекрасная сокурсница. Давай поиграем!
Правила просты: ты загадываешь n - длину числа. Далее компьютер загадывает число этой длины.
Ты должен угадать это число, вводя свои. Твое число должно быть длины n и состоять из разных цифр.
Ведущие нули учитывать не будут. Тебе будет показано количество быков и коров.
Быки - совпадающие цифры, стоящие на своей позиции. Коровы - совпадающие цифры, стоящие не на своем месте.
Удачи!";
            return rules;
        }

        /// <summary>
        ///Точка входа.
        /// </summary>
        static void Main()
        {
            Console.WriteLine(Rules());
            //Проверка на желание юзера продолжать игру.
            string yes; 
            do
            {
                InteractionWithUser();
                //Узнаем, продолжаем ли игру.
                yes = Console.ReadLine(); 
            } while (yes == "да");
            Console.WriteLine("Спасибо за игру!");
        }
    }
}