using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace VorobyevVisualizerWork
{
    class readerUINT8
    {
        public static int X, Y, Z;
        public static float[,,] array;
        public readerUINT8() { }

        public float[,,] getArray(string path, int size)
        {
            readUNIT8(path, size);
            return array;
        }
        public void readUNIT8(string path, int size)
        {
            if (File.Exists(path))
            {
                BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open));
                array = new float[size, size, size];
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                        for (int k = 0; k < size; k++)
                            array[i, j, k] = reader.ReadSByte();
                }
            }
        }
    }
}
