﻿// Copyright (c) 2019 Shapelets.io
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace khiva.features
{
    public class Features
    {

        /**
         * @brief Calculates the sum over the square values of the timeseries
         *
         * @param array Expects an input array whose dimension zero is the length of the time
         * series (all the same) and dimension one indicates the number of
         * time series.
         * @return result An array with the same dimensions as array, whose values (time series in dimension 0)
         * contains the sum of the squares values in the time series.
         */
        public static array.Array AbsEnergy(array.Array array)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.abs_energy(ref reference, out IntPtr result);
            return (new array.Array(result));
        }

        /**
         * @brief Calculates the sum over the absolute value of consecutive changes in the time series.
         *
         * @param array Expects an input array whose dimension zero is the length of the time
         * series (all the same) and dimension one indicates the number of
         * time series.
         * @return result An array with the same dimensions as array, whose values (time series in dimension 0)
         * contains absolute value of consecutive changes in the time series.
         */
        public static array.Array AbsoluteSumOfChanges(array.Array array)
        {

            IntPtr reference = array.Reference;
            interop.DLLFeatures.absolute_sum_of_changes( ref reference, out IntPtr result);
            return (new array.Array(result));
        }

        /**
         * @brief Calculates the value of an aggregation function f_agg (e.g. var or mean) of the autocorrelation
         * (Compare to http://en.wikipedia.org/wiki/Autocorrelation#Estimation), taken over different all possible
         * lags (1 to length of x).
         *
         * @param array Expects an input array whose dimension zero is the length of the time
         * series (all the same) and dimension one indicates the number of time series.
         * @param aggregation_function Function to be used in the aggregation. It receives an integer which indicates the
         * function to be applied:
         *          {
         *              0 : mean,
         *              1 : median
         *              2 : min,
         *              3 : max,
         *              4 : stdev,
         *              5 : var,
         *              default : mean
         *          }
         * @return result An array whose values contains the aggregated correaltion for each time series.
         */
        public static array.Array AggregatedAutocorrelation(array.Array array, int aggregationFunction)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.aggregated_autocorrelation(ref reference, ref aggregationFunction, out IntPtr result);
            return (new array.Array(result));
        }

        /**
         * @brief Calculates a linear least-squares regression for values of the time series that were aggregated
         * over chunks versus the sequence from 0 up to the number of chunks minus one.
         *
         * @param array The time series to calculate the features of
         * @param chunkSize The chunk size used to aggregate the data.
         * @param aggregation_function Function to be used in the aggregation. It receives an integer which indicates the
         * function to be applied:
         *          {
         *              0 : mean,
         *              1 : median
         *              2 : min,
         *              3 : max,
         *              4 : stdev,
         *              default : mean
         *          }
         * @return slope Slope of the regression line.
         * @return intercept Intercept of the regression line.
         * @return rvalue Correlation coefficient.
         * @return pvalue Two-sided p-value for a hypothesis test whose null hypothesis is that the slope is zero,
         * using Wald Test with t-distribution of the test statistic.
         * @return stderrest Standard error of the estimated gradient.
         */
        public static (array.Array, array.Array, array.Array, array.Array, array.Array) AggregatedLinearTrend(array.Array array, long chunkSize, int aggregationFunction)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.aggregated_linear_trend(ref reference,
                                                        ref chunkSize,
                                                        ref aggregationFunction,
                                                        out IntPtr slope, out IntPtr intercept, out IntPtr rvalue, out IntPtr pvalue, out IntPtr stderrest);
            var tuple = (slopeArr: new array.Array(slope),
                    interceptArr: new array.Array(intercept),
                    rvalueArr: new array.Array(rvalue),
                    pvalueArr: new array.Array(pvalue),
                    stderrestArr: new array.Array(stderrest));
            return tuple;
        }

        /**
         * @brief Calculates a vectorized Approximate entropy algorithm.
         * https://en.wikipedia.org/wiki/Approximate_entropy
         * For short time-series this method is highly dependent on the parameters, but should be stable for N > 2000,
         * see: Yentes et al. (2012) - The Appropriate Use of Approximate Entropy and Sample Entropy with Short Data Sets
         * Other shortcomings and alternatives discussed in:
         * Richman & Moorman (2000) - Physiological time-series analysis using approximate entropy and sample entropy.
         *
         * @param array Expects an input array whose dimension zero is the length of the time
         * series (all the same) and dimension one indicates the number of
         * time series.
         * @param m Length of compared run of data.
         * @param r Filtering level, must be positive.
         * @return result The vectorized approximate entropy for all the input time series in array.
         */
        public static array.Array ApproximateEntropy(array.Array array, int m, float r)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.approximate_entropy(ref reference, ref m, ref r,  out IntPtr result);
            return (new array.Array(result));
        }

        /**
         * @brief Calculates the cross-covariance of the given time series.
         *
         * @param xss Expects an input array whose dimension zero is the length of the
         * time series (all the same) and dimension one indicates the number of time
         * series.
         * @param yss Expects an input array whose dimension zero is the length of the
         * time series (all the same) and dimension one indicates the number of time
         * series.
         * @param unbiased Determines whether it divides by n - lag (if true) or
         * n (if false).
         * @return result The cross-covariance value for the given time series.
         */
        public static array.Array CrossCovariance(array.Array xss, array.Array yss, bool unbiased)
        {
            IntPtr referenceXss = xss.Reference;
            IntPtr referenceYss = yss.Reference;
            interop.DLLFeatures.cross_covariance(ref referenceXss, ref referenceYss, ref unbiased, out IntPtr result);
            return (new array.Array(result));
        }

        /**
         * @brief Calculates the auto-covariance the given time series.
         *
         * @param array Expects an input array whose dimension zero is the length of the
         * time series (all the same) and dimension one indicates the number of time
         * series.
         * @param unbiased Determines whether it divides by n - lag (if true) or
         * n (if false).
         * @return result The auto-covariance value for the given time series.
         */
        public static array.Array AutoCovariance(array.Array array, bool unbiased = false)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.auto_covariance(ref reference, ref unbiased, out IntPtr result);
            return (new array.Array(result));
        }

        /**
         * @brief Calculates the cross-correlation of the given time series.
         *
         * @param xss Expects an input array whose dimension zero is the length of the
         * time series (all the same) and dimension one indicates the number of time
         * series.
         * @param yss Expects an input array whose dimension zero is the length of the
         * time series (all the same) and dimension one indicates the number of time
         * series.
         * @param unbiased Determines whether it divides by n - lag (if true) or
         * n (if false).
         * @return result The cross-correlation value for the given time series.
         */
        public static array.Array CrossCorrelation(array.Array xss, array.Array yss, bool unbiased)
        {
            IntPtr referenceXss = xss.Reference;
            IntPtr referenceYss = yss.Reference;
            interop.DLLFeatures.cross_correlation(ref referenceXss, ref referenceYss, ref unbiased, out IntPtr result);
            return (new array.Array(result));
        }

        /**
         * @brief Calculates the autocorrelation of the specified lag for the given time.
         * series.
         *
         * @param array Expects an input array whose dimension zero is the length of the
         * time series (all the same) and dimension one indicates the number of time
         * series.
         * @param max_lag The maximum lag to compute.
         * @param unbiased Determines whether it divides by n - lag (if true) or n ( if false)
         * @return result The autocorrelation value for the given time series.
         */
        public static array.Array AutoCorrelation(array.Array array, long max_lag, bool unbiased)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.auto_correlation(ref reference, ref max_lag, ref unbiased, out IntPtr result);
            return (new array.Array(result));
        }

        /**
         * @brief Calculates the binned entropy for the given time series and number of bins.
         *
         * @param array Expects an input array whose dimension zero is the length of the
         * time series (all the same) and dimension one indicates the number of time
         * series.
         * @param max_bins The number of bins.
         * @return result The binned entropy value for the given time series.
         */
        public static array.Array BinnedEntropy(array.Array array, int max_bins)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.binned_entropy(ref reference, ref max_bins, out IntPtr result);
            return (new array.Array(result));
        }

        /**
         * @brief Calculates the Schreiber, T. and Schmitz, A. (1997) measure of non-linearity
         * for the given time series.
         *
         * @param array Expects an input array whose dimension zero is the length of the
         * time series (all the same) and dimension one indicates the number of time
         * series.
         * @param lag The lag
         * @return result The non-linearity value for the given time series.
         */
        public static array.Array C3(array.Array array, long lag)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.c3(ref reference, ref lag, out IntPtr result);
            return (new array.Array(result));

        }

        /**
         * @brief Calculates an estimate for the time series complexity defined by
         * Batista, Gustavo EAPA, et al (2014). (A more complex time series has more peaks,
         * valleys, etc.).
         *
         * @param array Expects an input array whose dimension zero is the length of the
         * time series (all the same) and dimension one indicates the number of time
         * series.
         * @param zNormalize Controls whether the time series should be z-normalized or not.
         * @return result The complexity value for the given time series.
         */
        public static array.Array CidCe(array.Array array, bool zNormalize)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.cid_ce(ref reference, ref zNormalize, out IntPtr result);
            return (new array.Array(result));
        }

        /**
         * @brief Calculates the number of values in the time series that are higher than
         * the mean.
         *
         * @param array Expects an input array whose dimension zero is the length of the
         * time series (all the same) and dimension one indicates the number of time
         * series.
         * @return result The number of values in the time series that are higher
         * than the mean.
         */
        public static array.Array CountAboveMean(array.Array array)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.count_above_mean(ref reference, out IntPtr result);
            return (new array.Array(result));
        }

        /**
        * @brief Calculates the number of values in the time series that are lower than
        * the mean.
        *
        * @param array Expects an input array whose dimension zero is the length of the
        * time series (all the same) and dimension one indicates the number of time
        * series.
        * @return result The number of values in the time series that are lower
        * than the mean.
        */
        public static array.Array CountBelowMean(array.Array array)
        {
            IntPtr reference = array.Reference;
            interop.DLLFeatures.count_below_mean(ref reference, out IntPtr result);
            return (new array.Array(result));
        }
        /*
       public static array.Array cwt_coefficients(array.Array array, IntPtr width, int coeff, int w){}
        
       public static array.Array energy_ratio_by_chunks(array.Array array, long num_segments, long segment_focus){}
        
       public static array.Array fft_aggregated(array.Array array){}
        
       public static array.Array fft_coefficient(array.Array array, long coefficient, IntPtr real, IntPtr imag,
                              IntPtr absolute, IntPtr angle){}
        
       public static array.Array first_location_of_maximum(array.Array array){}
        
       public static array.Array first_location_of_minimum(array.Array array){}
        
       public static array.Array friedrich_coefficients(array.Array array, int m, float r){}
        
       public static array.Array has_duplicates(array.Array array){}
        
       public static array.Array has_duplicate_max(array.Array array){}
        
       public static array.Array has_duplicate_min(array.Array array){}
        
       public static array.Array index_mass_quantile(array.Array array, float q){}
        
       public static array.Array kurtosis(array.Array array){}
        
       public static array.Array large_standard_deviation(array.Array array, float r){}
        
       public static array.Array last_location_of_maximum(array.Array array){}
        
       public static array.Array last_location_of_minimum(array.Array array){}
        
       public static array.Array length(array.Array array){}
        
       public static array.Array linear_trend(array.Array array, IntPtr pvalue, IntPtr rvalue, IntPtr intercept,
                           IntPtr slope, IntPtr stdrr){}
        
       public static array.Array local_maximals(array.Array array){}
        
       public static array.Array longest_strike_above_mean(array.Array array){}
        
       public static array.Array longest_strike_below_mean(array.Array array){}
        
       public static array.Array max_langevin_fixed_point(array.Array array, int m, float r){}
        
       public static array.Array maximum(array.Array array){}
        
       public static array.Array mean(array.Array array){}
        
       public static array.Array mean_absolute_change(array.Array array){}
        
       public static array.Array mean_change(array.Array array){}
        
       public static array.Array mean_second_derivative_central(array.Array array){}
        
       public static array.Array median(array.Array array){}
        
       public static array.Array minimum(array.Array array){}
        
       public static array.Array number_crossing_m(array.Array array, int m){}
        
       public static array.Array number_cwt_peaks(array.Array array, int max_w){}
        
       public static array.Array number_peaks(array.Array array, int n){}
        
       public static array.Array partial_autocorrelation(array.Array array, IntPtr lags){}
        
       public static array.Array percentage_of_reoccurring_datapoints_to_all_datapoints(array.Array array, bool is_sorted,
                                                                     IntPtr result){}
        
       public static array.Array percentage_of_reoccurring_values_to_all_values(array.Array array, bool is_sorted){}
        
       public static array.Array quantile(array.Array array, IntPtr q, float precision){}
        
       public static array.Array range_count(array.Array array, float min, float max){}
        
       public static array.Array ratio_beyond_r_sigma(array.Array array, float r){}
        
       public static array.Array ratio_value_number_to_time_series_length(array.Array array){}
        
       public static array.Array sample_entropy(array.Array array){}
        
       public static array.Array skewness(array.Array array){}
        
       public static array.Array spkt_welch_density(array.Array array, int coeff){}
        
       public static array.Array standard_deviation(array.Array array){}
        
       public static array.Array sum_of_reoccurring_datapoints(array.Array array, bool is_sorted){}
        
       public static array.Array sum_of_reoccurring_values(array.Array array, bool is_sorted){}
        
       public static array.Array sum_values(array.Array array){}
        
       public static array.Array symmetry_looking(array.Array array, float r){}
        
       public static array.Array time_reversal_asymmetry_statistic(array.Array array, int lag){}
        
       public static array.Array value_count(array.Array array, float v){}
        
       public static array.Array variance(array.Array array){}
        
       public static array.Array variance_larger_than_standard_deviation(array.Array array){}
       */
    }
}
