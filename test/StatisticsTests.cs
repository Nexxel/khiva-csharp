﻿// Copyright (c) 2019 Shapelets.io
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using NUnit.Framework;

namespace Khiva.Tests
{
    [TestFixture, Category("Statistics")]
    public class StatisticsTests
    {
        private const double Delta = 1e-6;

        [SetUp]
        public void Init()
        {
            Library.CurrentBackend = Library.Backend.KhivaBackendCpu;
        }

        [Test]
        public void TestCovarianceUnbiased()
        {
            float[,] tss = {{-2.1F, -1, 4.3F}, {3, 1.1F, 0.12F}, {3, 1.1F, 0.12F}};
            using (KhivaArray arr = KhivaArray.Create(tss), covariance = Statistics.CovarianceStatistics(arr, true))
            {
                float[,] expected =
                {
                    {11.70999999F, -4.286F, -4.286F},
                    {-4.286F, 2.14413333F, 2.14413333F},
                    {-4.286F, 2.14413333F, 2.14413333F}
                };
                var result = covariance.GetData2D<float>();
                for (var i = 0; i < result.GetLength(0); i++)
                {
                    for (var j = 0; j < result.GetLength(1); j++)
                    {
                        Assert.AreEqual(expected[i, j], result[i, j], Delta);
                    }
                }
            }
        }

        [Test]
        public void TestCovarianceBiased()
        {
            float[,] tss = {{-2.1F, -1, 4.3F}, {3, 1.1F, 0.12F}, {3, 1.1F, 0.12F}};
            using (KhivaArray arr = KhivaArray.Create(tss), covariance = Statistics.CovarianceStatistics(arr, false))
            {
                float[,] expected =
                {
                    {7.80666667F, -2.85733333F, -2.85733333F},
                    {-2.85733333F, 1.42942222F, 1.42942222F},
                    {-2.85733333F, 1.42942222F, 1.42942222F}
                };
                var result = covariance.GetData2D<float>();
                for (var i = 0; i < result.GetLength(0); i++)
                {
                    for (var j = 0; j < result.GetLength(1); j++)
                    {
                        Assert.AreEqual(expected[i, j], result[i, j], Delta);
                    }
                }
            }
        }

        [Test]
        public void TestKurtosis()
        {
            double[,] tss = {{0, 1, 2, 3, 4, 5}, {2, 2, 2, 20, 30, 25}};
            using (KhivaArray arr = KhivaArray.Create(tss), kurtosis = Statistics.KurtosisStatistics(arr))
            {
                double[] expected = {-1.2, -2.66226722};
                var result = kurtosis.GetData2D<double>();
                for (var i = 0; i < result.GetLength(0); i++)
                {
                    Assert.AreEqual(expected[i], result[i, 0], 1e-2);
                }
            }
        }

        [Test]
        public void TestLjungBox()
        {
            double[,] tss = {{0, 1, 2, 3}, {4, 5, 6, 7}};
            using (KhivaArray arr = KhivaArray.Create(tss), ljungBox = Statistics.LjungBox(arr, 3))
            {
                double[] expected = {6.4400, 6.4400};
                var result = ljungBox.GetData2D<double>();
                for (var i = 0; i < result.GetLength(0); i++)
                {
                    Assert.AreEqual(expected[i], result[i, 0], Delta);
                }
            }
        }

        [Test]
        public void TestMoment()
        {
            double[,] tss = {{0, 1, 2, 3, 4, 5}, {0, 1, 2, 3, 4, 5}};
            using (KhivaArray arr = KhivaArray.Create(tss),
                moment2 = Statistics.MomentStatistics(arr, 2),
                moment4 = Statistics.MomentStatistics(arr, 4))
            {
                double[] expected2 = {9.166666666, 9.166666666};
                double[] expected4 = {163.1666666666, 163.1666666666};
                var result2 = moment2.GetData2D<double>();
                var result4 = moment4.GetData2D<double>();
                for (var i = 0; i < result2.GetLength(0); i++)
                {
                    Assert.AreEqual(expected2[i], result2[i, 0], Delta);
                    Assert.AreEqual(expected4[i], result4[i, 0], 1e-2);
                }
            }
        }

        [Test]
        public void TestQuantile()
        {
            double[,] tss = {{0, 1, 2, 3, 4, 5}, {6, 7, 8, 9, 10, 11}};
            double[] tss2 = {0.1, 0.2};
            using (KhivaArray arr = KhivaArray.Create(tss),
                arr2 = KhivaArray.Create(tss2),
                quantile = Statistics.QuantileStatistics(arr, arr2))
            {
                double[,] expected = {{0.5, 1.0}, {6.5, 7.0}};
                var result = quantile.GetData2D<double>();
                for (var i = 0; i < result.GetLength(0); i++)
                {
                    for (var j = 0; j < result.GetLength(1); j++)
                    {
                        Assert.AreEqual(expected[i, j], result[i, j], 1e-2);
                    }
                }
            }
        }

        [Test]
        public void TestQuantileCut2()
        {
            float[,] tss = {{0, 1, 2, 3, 4, 5}, {6, 7, 8, 9, 10, 11}};
            using (var arr = KhivaArray.Create(tss))
            {
                var quantile = Statistics.QuantilesCutStatistics(arr, 2);
                float[,,] expected =
                {
                    {{-0.00000001F, 2.5F}, {-0.00000001F, 2.5F}}, {{-0.00000001F, 2.5F}, {2.5F, 5.0F}},
                    {{2.5F, 5.0F}, {2.5F, 5.0F}}, {{6.0F, 8.5F}, {6.0F, 8.5F}}, {{6.0F, 8.5F}, {8.5F, 11.0F}},
                    {{8.5F, 11.0F}, {8.5F, 11.0F}}
                };
                quantile = quantile.Transpose();
                using (quantile)
                {
                    var result = quantile.GetData3D<float>();
                    Assert.AreEqual(expected, result);
                }
            }
        }

        [Test]
        public void TestQuantileCut3()
        {
            double[,] tss = {{0, 1, 2, 3, 4, 5}, {6, 7, 8, 9, 10, 11}};
            using (var arr = KhivaArray.Create(tss))
            {
                var quantile = Statistics.QuantilesCutStatistics(arr, 3);
                double[,,] expected =
                {
                    {{-0.00000001, 1.66666667}, {-0.00000001, 1.6666667}},
                    {
                        {1.6666667, 3.3333333},
                        {1.6666667, 3.3333333}
                    },
                    {{3.3333333, 5.0}, {3.3333333, 5.0}},
                    {{5.9999999, 7.66666667}, {5.9999999, 7.6666667}},
                    {
                        {7.6666667, 9.3333333},
                        {7.6666667, 9.3333333}
                    },
                    {{9.3333333, 11.0}, {9.3333333, 11.0}}
                };
                quantile = quantile.Transpose();
                using (quantile)
                {
                    var result = quantile.GetData3D<double>();
                    for (var i = 0; i < result.GetLength(0); i++)
                    {
                        for (var j = 0; j < result.GetLength(1); j++)
                        {
                            for (var k = 0; k < result.GetLength(2); k++)
                            {
                                Assert.AreEqual(expected[i, j, k], result[i, j, k], Delta);
                            }
                        }
                    }
                }
            }
        }

        [Test]
        public void TestQuantileCut7()
        {
            double[,] tss = {{0, 1, 2, 3, 4, 5}, {6, 7, 8, 9, 10, 11}};
            using (var arr = KhivaArray.Create(tss))
            {
                var quantile = Statistics.QuantilesCutStatistics(arr, 7);
                double[,,] expected =
                {
                    {{0, 0.7142857}, {0.7142857, 1.4285715}},
                    {
                        {1.4285715, 2.1428573},
                        {2.8571429, 3.5714288}
                    },
                    {{3.5714288, 4.2857146}, {4.2857146, 5}},
                    {{5.9999999, 6.7142857}, {6.7142857, 7.4285715}},
                    {
                        {7.4285715, 8.1428573},
                        {8.8571429, 9.5714288}
                    },
                    {{9.5714288, 10.2857146}, {10.2857146, 11}}
                };
                quantile = quantile.Transpose();
                using (quantile)
                {
                    var result = quantile.GetData3D<double>();
                    for (var i = 0; i < result.GetLength(0); i++)
                    {
                        for (var j = 0; j < result.GetLength(1); j++)
                        {
                            for (var k = 0; k < result.GetLength(2); k++)
                            {
                                Assert.AreEqual(expected[i, j, k], result[i, j, k], Delta);
                            }
                        }
                    }
                }
            }
        }

        [Test]
        public void TestStdev()
        {
            double[,] tss = {{0, 1, 2, 3, 4, 5}, {2, 2, 2, 20, 30, 25}};
            using (KhivaArray arr = KhivaArray.Create(tss), stdev = Statistics.SampleStdevStatistics(arr))
            {
                double[] expected = {1.870828693, 12.988456413};
                var result = stdev.GetData2D<double>();
                for (var i = 0; i < result.GetLength(0); i++)
                {
                    Assert.AreEqual(expected[i], result[i, 0], 1e-2);
                }
            }
        }

        [Test]
        public void TestSkewness()
        {
            double[,] tss = {{0, 1, 2, 3, 4, 5}, {2, 2, 2, 20, 30, 25}};
            using (KhivaArray arr = KhivaArray.Create(tss), skewness = Statistics.SkewnessStatistics(arr))
            {
                double[] expected = {0.0, 0.236177069879499};
                var result = skewness.GetData2D<double>();
                for (var i = 0; i < result.GetLength(0); i++)
                {
                    Assert.AreEqual(expected[i], result[i, 0], 1e-2);
                }
            }
        }
    }
}