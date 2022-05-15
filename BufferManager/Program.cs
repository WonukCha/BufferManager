using System;
using System.Text;

namespace BufferManager
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            BufferManager buffer = new BufferManager();
            buffer.Init(13, 3);
            string input;
            while(true)
            {
                Console.WriteLine("1. 입력, 2. 출력, 3. 크기");

                input = Console.ReadLine();
                switch(input)
                {
                    case "1":
                        {
                            Console.WriteLine("값을 입력해주세요");
                            input = Console.ReadLine();
                            byte[] strByte = Encoding.UTF8.GetBytes(input);
                            if(buffer.Write(strByte, 0, strByte.Length))
                            {
                                Console.WriteLine("입력 성공");
                            }
                            else
                            {
                                Console.WriteLine("입력 실패");
                            }
                        }
                        break;
                    case "2":
                        Console.WriteLine("값을 입력해주세요");
                        input = Console.ReadLine();
                        int readSize = int.Parse(input);
                        byte[] bytes = new byte[readSize];
                        if (buffer.Read(bytes, 0, bytes.Length))
                        {
                            string str = Encoding.Default.GetString(bytes);
                            Console.WriteLine(str);
                            Console.WriteLine("출력 성공");
                        }
                        else
                        {
                            Console.WriteLine("출력 실패");
                        }
                        break;
                    case "3":
                        Console.WriteLine("크기는 : " + buffer.Size());

                        break;
                    case "4":
                        break;
                }

            }
        }
    }
}
