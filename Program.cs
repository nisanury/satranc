using System;
using System.Collections.Generic;
using System.IO;

namespace satranc
{
    public class Program
    {
        static string[][] arr = new string[8][];
        static Dictionary<char, float> value = new Dictionary<char, float> { { 'p', 1 }, { 'a', 3 }, { 'f', 3 }, { 'k', 5 }, { 'v', 9 }, { 's', 100 } };

        static void EvaluateKnightPositions(int i, int j, char c, ref float score) //at için hesaplama
        {
            //2 yukarı 1sağ
            if (i > 1 && j < 7 && arr[i - 2][j + 1] != "xx" && arr[i - 2][j + 1][1] != c)
            {
                score -= value[arr[i - 2][j + 1][0]] / 2;
            }
            //2yukarı 1 sol
            if (i > 1 && j > 0 && arr[i - 2][j - 1] != "xx" && arr[i - 2][j - 1][1] != c)
            {
                score -= value[arr[i - 2][j - 1][0]] / 2;
            }
            //2 aşağı 1 sol
            if (i < 6 && j > 0 && arr[i + 2][j - 1] != "xx" && arr[i + 2][j - 1][1] != c)
            {
                score -= value[arr[i + 2][j - 1][0]] / 2;
            }
            //2 aşağı 1 sağ
            if (i < 6 && j < 7 && arr[i + 2][j + 1] != "xx" && arr[i + 2][j + 1][1] != c)
            {
                score -= value[arr[i + 2][j + 1][0]] / 2;
            }
            //1 aşağı 2 sağ
            if (i < 7 && j < 6 && arr[i + 1][j + 2] != "xx" && arr[i + 1][j + 2][1] != c)
            {
                score -= value[arr[i + 1][j + 2][0]] / 2;
            }
            //1 yukarı 2 sağ
            if (i > 0 && j < 6 && arr[i - 1][j + 2] != "xx" && arr[i - 1][j + 2][1] != c)
            {
                score -= value[arr[i - 1][j + 2][0]] / 2;
            }
            //1 aşağı 2 sol
            if (i < 7 && j > 1 && arr[i + 1][j - 2] != "xx" && arr[i + 1][j - 2][1] != c)
            {
                score -= value[arr[i + 1][j - 2][0]] / 2;
            }
            //1 yukarı 2 sol
            if (i > 0 && j > 1 && arr[i - 1][j - 2] != "xx" && arr[i - 1][j - 2][1] != c)
            {
                score -= value[arr[i - 1][j - 2][0]] / 2;
            }

        }

        static void EvaluateBishopPositions(int i, int j, char c, ref float score) //fil için hesaplama
        {
            int k, l;

            //sağ aşağı çapraz
            k = i;
            l = j;
            while (k < 7 && l < 7 && arr[k + 1][l + 1] == "xx") // tahta içerisinde boşluk boyunca hareket et
            {
                k++;
                l++;
            }
            if (k < 7 && l < 7 && arr[k + 1][l + 1][1] != c && arr[k + 1][l + 1] != "xx") // karşı renk taşla karşılaşıldıysa
            {
                score -= value[arr[k + 1][l + 1][0]] / 2;
            }

            //sol aşağı çapraz
            k = i;
            l = j;
            while (k < 7 && l > 0 && arr[k + 1][l - 1] == "xx") // tahta içerisinde boşluk boyunca hareket et
            {
                k++;
                l--;
            }
            if (k < 7 && l > 0 && arr[k + 1][l - 1][1] != c && arr[k + 1][l - 1] != "xx") // karşı renk taşla karşılaşıldıysa
            {
                score -= value[arr[k + 1][l - 1][0]] / 2;
            }

            //sol yukarı çapraz
            k = i;
            l = j;
            while (k > 0 && l > 0 && arr[k - 1][l - 1] == "xx") // tahta içerisinde boşluk boyunca hareket et
            {
                k--;
                l--;
            }
            if (k > 0 && l > 0 && arr[k - 1][l - 1][1] != c && arr[k - 1][l - 1] != "xx") // karşı renk taşla karşılaşıldıysa
            {
                score -= value[arr[k - 1][l - 1][0]] / 2;
            }

            //sağ yukarı çapraz
            k = i;
            l = j;
            while (k > 0 && l < 7 && arr[k - 1][l + 1] == "xx") // tahta içerisinde boşluk boyunca hareket et
            {
                k--;
                l++;
            }
            if (k > 0 && l < 7 && arr[k - 1][l + 1][1] != c && arr[k - 1][l + 1] != "xx") // karşı renk taşla karşılaşıldıysa
            {
                score -= value[arr[k - 1][l + 1][0]] / 2;
            }
        }

        static void Main(string[] args)
        {
            int i = 0, j = 0;
            float black = 0, white = 0;
            char[] ch;

            /*
                Tahta Dosya Adı		Sonuçlar
                board1.txt		    Siyah:121 (123)   , Beyaz:118 (120)
                board2.txt		    Siyah:132.5 (133) , Beyaz:126.5 (129)
                board3.txt		    Siyah:57.5 (108)  , Beyaz:112.5 (115)

                board3.txt senaryosunda elle hesaplama yaptığımızda siyah taşların kalan puanı 55.5((100/50)+(1/2)+(1/2)+(3/2)=52,5=> 108-52,5 = 55,5)çıkıyor.
             */

            string fileName = @"board1.txt";
            //string fileName = @"board2.txt";
            //string fileName = @"board3.txt";

            // txt üzerinden verileri oku diziye at
            using (StreamReader reader = new StreamReader(fileName))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    arr[i] = line.Split(" ");
                    i++;
                }
            }

            //tüm siyahları ve beyaz taşları topla daha sonra tehlike de olanları kontrol edip çıkarma işlermi yap
            for (i = 0; i < 8; i++)
            {
                for (j = 0; j < 8; j++)
                {
                    ch = arr[i][j].ToCharArray(); // taş siyah mı beyaz mı çıkar

                    if (ch[1] == 's') //taş siyahsa
                    {
                        if (ch[0] == 'p')
                            black += value['p'];
                        if (ch[0] == 'a')
                        {
                            black += value['a'];
                            EvaluateKnightPositions(i, j, ch[1], ref white);
                        }
                        if (ch[0] == 'f')
                        {
                            black += value['f'];
                            EvaluateBishopPositions(i, j, ch[1], ref white);
                        }
                        if (ch[0] == 'k')
                            black += value['k'];
                        if (ch[0] == 'v')
                            black += value['v'];
                        if (ch[0] == 's')
                            black += value['s'];
                    }
                    else //taş beyazsa
                    {
                        if (ch[0] == 'p')
                            white += value['p'];
                        if (ch[0] == 'a')
                        {
                            white += value['a'];
                            EvaluateKnightPositions(i, j, ch[1], ref black);
                        }
                        if (ch[0] == 'f')
                        {
                            white += value['f'];
                            EvaluateBishopPositions(i, j, ch[1], ref black);
                        }
                        if (ch[0] == 'k')
                            white += value['k'];
                        if (ch[0] == 'v')
                            white += value['v'];
                        if (ch[0] == 's')
                            white += value['s'];
                    }
                }
            }
            Console.WriteLine($"Siyah: {black} | Beyaz: {white}");
        }
    }
}
