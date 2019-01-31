using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SieveOfEratosthenes
{
    class PrimeNumberUtility
    {
        // The sieve data stores a true if that bits has a factor besides itself and 1.
        private static byte[] _SieveData;

        private static int _Size;

        private static byte[] MaskValues = new byte[]
        {
            128,
            64,
            32,
            16,
            8,
            4,
            2,
            1
        };

        private static int[] IncrementValues = new int[]
        {
            -1,
            2,
            -1,
            4,
            -1,
            -1,
            -1,
            2,
            -1,
            2
        };

        public static bool IsNumberPrime(int inputNumber)
        {
            if( inputNumber == 2 || inputNumber == 3 || inputNumber ==5)
            {
                return true;
            }
            if (inputNumber % 2 == 0)
            {
                return false;
            }
            if (inputNumber % 3 == 0)
            {
                return false;
            }
            if (inputNumber % 5 == 0)
            {
                return false;
            }

            // Add extra spot for 0
            _Size = inputNumber + 1;

            // capacity is for 8 bits per byte but not leaving
            // any space for even numbers
            int capacity = (int)Math.Ceiling(_Size * 1.0 / 8 / 2.0);

            if(_SieveData != null && capacity < _SieveData.Length)
            {
                return IsNumberPrimePrivate(inputNumber);
            }
            _SieveData = new byte[capacity];

            // we are starting at 7
            // anything that is a multiple of 2,3 or 5 will have already returned false
            // so we don't have to worry about them
            int i = 7;
            while(i * i < _Size)
            {
                if(IsNumberPrimePrivate(i))
                {
                    // If number is prime, set multiples as not prime
                    if(!SetMultiplesAsNotPrime(i, inputNumber))
                    {
                        return false;
                    }
                }

                var oneDigit = i % 10;

                i += IncrementValues[oneDigit];
            }

            return true;
        }

        private static bool IsNumberPrimePrivate(int number)
        {
            if (number % 3 == 0)
            {
                return false;
            }
            int byteIndex = GetByteIndex(number);
            int bitIndex = GetBitIndex(number);
            byte mask = GetMask(bitIndex);
            bool isSet = (_SieveData[byteIndex] & mask) != 0;
            return !isSet;
        }

        private void SetMultiplesOf2AsNotPrime()
        {
            for (int i = 0; i < _SieveData.Length; i++)
            {
                // Set all two bits as not prime
                byte mask = 0b10101010;
                _SieveData[i] |= mask;
            }
        }

        private void SetMultiplesOf3AsNotPrime()
        {
            int currentNumber = 9;
            while (currentNumber < _Size)
            {
                int byteIndex = GetByteIndex(currentNumber);
                int bitIndex = GetBitIndex(currentNumber);

                byte mask = GetMask(bitIndex);

                // set bit to 1, meaning not prime
                _SieveData[byteIndex] |= mask;

                // skipping even 3s
                currentNumber = currentNumber + 6;
            }
        }

        private void SetMultiplesOf5AsNotPrime()
        {
            int currentNumber = 15;
            while (currentNumber < _Size)
            {
                int byteIndex = GetByteIndex(currentNumber);
                int bitIndex = GetBitIndex(currentNumber);

                byte mask = GetMask(bitIndex);

                // set bit to 1, meaning not prime
                _SieveData[byteIndex] |= mask;

                // skipping even 5s
                currentNumber = currentNumber + 10;
            }
        }

        private static bool SetMultiplesAsNotPrime(int number, int numberToFind)
        {
            // the number itself will be prime, just set the multiples, starting with 3x
            // because 2x will be even
            int currentNumber = number + number + number;
            while(currentNumber < _Size)
            {
                if(currentNumber == numberToFind)
                {
                    return false;
                }
                int byteIndex = GetByteIndex(currentNumber);
                int bitIndex = GetBitIndex(currentNumber);

                byte mask = GetMask(bitIndex);

                // set bit to 1, meaning not prime
                _SieveData[byteIndex] |= mask;
                
                // skip every other, because every other will be even
                // we don't need to set those
                currentNumber = currentNumber + number + number;
            }

            return true;
        }

        private static byte GetMask(int bitIndex)
        {
            //byte mask = (byte)(1 << (7 - bitIndex));
            //return mask;
            byte result = MaskValues[bitIndex];
            return result;
        }

        private static int GetBitIndex(int number)
        {
            int primary = number / 2;
            int result = primary % 8;
            return result;
        }

        private static int GetByteIndex(int number)
        {
            int result = number / 16;
            return result;
        }
    }
}
