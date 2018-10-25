using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace VorobyevVisualizerWork
{
    class readerFLOAT32
    {
        public static int X, Y, Z;
        public static float[,,] array;
        public readerFLOAT32() { }

        public float[,,] getArray(string path, int size)
        {
            readFLOAT32(path, size);
            return array;
        }
        public void readFLOAT32(string path, int size)
        {
            if (File.Exists(path))
            {
                BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open));
                array = new float[size, size, size];
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                        for (int k = 0; k < size; k++)
                            array[i, j, k] = reader.ReadSingle();
                }
            }
        }
    }
}
