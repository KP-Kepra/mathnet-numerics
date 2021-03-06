// <copyright file="StatTestData.cs" company="Math.NET">
// Math.NET Numerics, part of the Math.NET Project
// http://numerics.mathdotnet.com
// http://github.com/mathnet/mathnet-numerics
// http://mathnetnumerics.codeplex.com
// Copyright (c) 2009-2010 Math.NET
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
// </copyright>

namespace MathNet.Numerics.UnitTests.StatisticsTests
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Statistics data.
    /// </summary>
    internal class StatTestData
    {
        /// <summary>
        /// Data array.
        /// </summary>
        public readonly double[] Data;

        /// <summary>
        /// Data with nulls.
        /// </summary>
        public readonly double?[] DataWithNulls;

        /// <summary>
        /// Mean value.
        /// </summary>
        public readonly double Mean;

        /// <summary>
        /// Standard Deviation.
        /// </summary>
        public readonly double StandardDeviation;

        /// <summary>
        /// Initializes a new instance of the StatTestData class.
        /// </summary>
        /// <param name="file">Path to the file.</param>
        public StatTestData(string file)
        {
            using (var reader = new StreamReader(file))
            {
                var line = reader.ReadLine().Trim();

                while (!line.StartsWith("--"))
                {
                    if (line.StartsWith("Sample Mean"))
                    {
                        Mean = GetValue(line);
                    }
                    else if (line.StartsWith("Sample Standard Deviation"))
                    {
                        StandardDeviation = GetValue(line);
                    }

                    line = reader.ReadLine().Trim();
                }

                line = reader.ReadLine();

                IList<double> list = new List<double>();
                while (line != null)
                {
                    line = line.Trim();
                    if (!line.Equals(string.Empty))
                    {
                        list.Add(double.Parse(line));
                    }

                    line = reader.ReadLine();
                }

                Data = list.ToArray();
            }

            DataWithNulls = new double?[Data.Length + 2];
            for (var i = 0; i < Data.Length; i++)
            {
                DataWithNulls[i + 1] = Data[i];
            }
        }

        /// <summary>
        /// Get value.
        /// </summary>
        /// <param name="str">Parameter name.</param>
        /// <returns>Parameter value.</returns>
        private static double GetValue(string str)
        {
            var start = str.IndexOf(":");
            var value = str.Substring(start + 1).Trim();
            if (value.Equals("NaN"))
            {
                return 0;
            }

            return double.Parse(str.Substring(start + 1).Trim());
        }
    }
}
