using System;
using System.Linq;
using System.Threading;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            uint[] exemplos = new uint[]
            {
                0b0000_0110_0011_1000_1111,
                0b0101_0001_0010_1111_1101,
                0b0001_0101_1110_1001_1000,
                0b1101_0101_1010_1001_1110,
                0b0011_0000_1000_1000_1011,
                0b0111_0111_1111_1011_0001,
                0b1000_0101_1000_1001_1111,
                0b1111_0100_0000_0001_1001,
                0b1011_1110_1010_1001_1100,
                0b0001_1101_1011_1011_0011,
                0b1100_1001_1000_0011_0101,
                0b1100_0000_1111_1111_0011,
            };
            uint[] portas = new uint[] { 0b0001, 0b0010, 0b0011, 0b0100 };

            // exemplo = cofre ^ nsequencial ^ tabela
            foreach (var exemplo in exemplos) 
            {
                foreach (var porta in portas)
                {
                    // desloca 8 bits para a esquerda, inverte os bits, e depois atribui mascara de 19 bits (7FFFF)
                    uint bitPortaInvertido = ~(porta << 8) & 0x7FFFF;
                    uint xorResultado = bitPortaInvertido ^ exemplo;
                    uint encrypted;
                    if (xorResultado > 983040)
                        // zera ultimo bit caso numero for maior que 1111_0000_0000_0000_0000 (para evitar numeros de 7 digitos)
                        encrypted = xorResultado & 0x7FFFF;
                    else
                        encrypted = xorResultado;

                    Console.WriteLine($@"{Convert.ToString(exemplo, 2).PadLeft(20, '0')}({exemplo}) XOR {Convert.ToString(bitPortaInvertido, 2).PadLeft(20, '0')}({bitPortaInvertido}) =>> {Convert.ToString(encrypted, 2).PadLeft(20, '0')} ({encrypted})");

                    uint reverse = exemplo ^ encrypted; //bitPortaInvertido
                    uint shift = ~(reverse >> 8) & 0xF; //porta
                    Console.WriteLine($"reversed porta = {Convert.ToString(shift, 2)}");
                }
            }
        }
    }
}
