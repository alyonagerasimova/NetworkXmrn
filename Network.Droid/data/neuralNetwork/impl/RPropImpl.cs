using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NueralNetrwork.Network.impl
{
    class RPropImpl
    {

        public double[] getHiddenValues()
        {
            return hiddenValues;
        }

        private double[] hiddenValues; // значение скрытого нейрона
        private double[] outputValues; // выходы
        private double[] hiddenValuesPrev; // значение скрытого нейрона
        private double[] outputValuesPrev; // выходы

        public void setWeightsPrev(double[][,] weightsPrev)
        {
            this.weightsPrev = weightsPrev;
        }

        private double[][,] weightsPrev; // вес от предыдущего к следующему
        private int numberHiddenNeurons;

        public void setPixelsValues(double[] pixelsValues)
        {
            this.pixelsValues = pixelsValues;
        }

        private double[] pixelsValues;
        private String[] percent = new String[27];

        public void setAnswers(int[] answers)
        {
            this.answers = answers;
        }

        private int[] answers = new int[Const.NUMBER_OUTPUT_NEURONS];

        public void setBias(double[,] bias)
        {
            this.bias = bias;
        }

        private double[,] bias; // байасы скрытых нейронов

        public void setInputValues(double[] inputValues)
        {
            this.inputValues = inputValues;
        }

        private double[] inputValues; // входы

        public void setLearningRateFactor(double learningRateFactor)
        {
            this.learningRateFactor = learningRateFactor;
        }

        private double learningRateFactor;
        private int numberCycles = 0;
        private double error = 0.0;
        private double globalNetworkError;
        private String answer;
        private double[][,] delta;
        private double[][,] deltaWeightPrev;
        private double[][,] deltaWeight;
        private double[][,] proizvodnayaPrev;
        private double[][,] proizvodnaya;
        private double[][,] weights;

        public RPropImpl(int numberHiddenNeurons, double numberLearningRate)
        {
            this.numberHiddenNeurons = numberHiddenNeurons;
            this.learningRateFactor = numberLearningRate;
            init();
        }

        public void setPatterns(List<double[]> readedPatterns)
        {
            this.patterns = readedPatterns;
        }

        private List<double[]> patterns;

        public void setNumberHiddenNeurons(Int16 numberHiddenNeurons)
        {
            this.numberHiddenNeurons = numberHiddenNeurons;
        }

        public void setNumberCycles(int numberCycles)
        {
            this.numberCycles = numberCycles;
        }

        public void setError(Double error)
        {
            this.error = error;
        }
        public void init()
        {
            delta = new double[2][,];
            weights = new double[2][,];
            deltaWeight = new double[2][,];
            deltaWeightPrev = new double[2][,];
            proizvodnayaPrev = new double[2][,];
            proizvodnaya = new double[2][,];
            outputValues = new double[Const.NUMBER_OUTPUT_NEURONS];
            hiddenValues = new double[numberHiddenNeurons];
            outputValuesPrev = new double[Const.NUMBER_OUTPUT_NEURONS];
            hiddenValuesPrev = new double[numberHiddenNeurons];
            delta[0] = new double[Const.NUMBER_INPUT_NEURONS, numberHiddenNeurons];
            delta[1] = new double[numberHiddenNeurons, Const.NUMBER_OUTPUT_NEURONS];
            deltaWeight[0] = new double[Const.NUMBER_INPUT_NEURONS, numberHiddenNeurons];
            deltaWeight[1] = new double[numberHiddenNeurons, Const.NUMBER_OUTPUT_NEURONS];
            deltaWeightPrev[0] = new double[Const.NUMBER_INPUT_NEURONS, numberHiddenNeurons];
            deltaWeightPrev[1] = new double[numberHiddenNeurons, Const.NUMBER_OUTPUT_NEURONS];
            proizvodnayaPrev[0] = new double[Const.NUMBER_INPUT_NEURONS, numberHiddenNeurons];
            proizvodnayaPrev[1] = new double[numberHiddenNeurons, Const.NUMBER_OUTPUT_NEURONS];
            proizvodnaya[0] = new double[Const.NUMBER_INPUT_NEURONS, numberHiddenNeurons];
            proizvodnaya[1] = new double[numberHiddenNeurons, Const.NUMBER_OUTPUT_NEURONS];
            weights[0] = new double[Const.NUMBER_INPUT_NEURONS, numberHiddenNeurons];
            weights[1] = new double[numberHiddenNeurons, Const.NUMBER_OUTPUT_NEURONS];
        }
        public void train()
        {
            if (patterns.Count() != 0)
            {
                if (numberCycles != 0)
                {
                    for (int i = 0; i < numberCycles; i++)
                    {
                        study();
                    }
                }
                else if (error != 0.0)
                {
                    int i = 1;
                    for (int j = 0; j < 2; j++)
                    {
                        while (globalNetworkError == 0 || globalNetworkError > error)
                        {
                            i++;
                            study();
                        }
                        globalNetworkError = 0;
                    }
                }
                double chance = 0;
                double[] chancesPerNuml = new double[patterns.Count()];
                int found = 0;

                for (int p = 0; p < Const.NUMBER_OUTPUT_NEURONS; p++)
                {
                    inputValues = patterns[p];
                    countValues();
                    for (int i = 1; i < outputValues.Length; i++)
                    {
                        if (outputValues[i] > outputValues[found])
                        {
                            found = i;
                        }
                    }
                    if (found == answers[p])
                    {
                        chancesPerNuml[answers[p]]++;
                        chance++;
                    }
                }

            }

        }

    public void countValues()
        {
            for (int i = 0; i < hiddenValues.Length; i++)
            {
                hiddenValues[i] = bias[0,i];
                for (int j = 0; j < inputValues.Length; j++)
                {
                    hiddenValues[i] += inputValues[j] * weights[0][j,i];
                }
                hiddenValues[i] = 1d / (1 + Math.Exp(-hiddenValues[i]));
            }
            for (int j = 0; j < outputValues.Length; j++)
            {
                outputValues[j] = bias[1,j];
                for (int i = 0; i < hiddenValues.Length; i++)
                {
                    outputValues[j] += hiddenValues[i] * weights[1][i,j];
                }
                outputValues[j] = 1d / (1 + Math.Exp(-outputValues[j]));
            }
        }
    public void countWeights(int index, double[] left, double[] right, double[] error)
        {
            for (int n = 0; n < right.Length; n++)
            {
                for (int m = 0; m < left.Length; m++)
                {
                    proizvodnaya[index][m,n] = left[m] * error[n];
                    deltaWeight[index][m,n] = -learningRateFactor * proizvodnaya[index][m,n];
                    if (proizvodnayaPrev[index][m,n] * proizvodnaya[index][m,n] > 0)
                    {
                        delta[index][m,n] *= Const.A;
                        if (proizvodnaya[index][m,n] > 0)
                        {
                            deltaWeight[index][m,n] -= delta[index][m,n];
                        }
                        else if (proizvodnaya[index][m,n] < 0)
                        {
                            deltaWeight[index][m,n] += delta[index][m,n];
                        }
                        else if (proizvodnaya[index][m,n] == 0)
                        {
                            deltaWeight[index][m,n] = 0;
                        }
                    }
                    else if (proizvodnayaPrev[index][m,n] * proizvodnaya[index][m,n] < 0)
                    {
                        delta[index][m,n] *= Const.B;
                        if (proizvodnaya[index][m,n] > 0)
                        {
                            deltaWeight[index][m,n] -= delta[index][m,n];
                        }
                        else if (proizvodnaya[index][m,n] < 0)
                        {
                            deltaWeight[index][m,n] += delta[index][m,n];
                        }
                        else if (proizvodnaya[index][m,n] == 0)
                        {
                            deltaWeight[index][m,n] = 0;
                        }
                    }
                    weights[index][m,n] = weightsPrev[index][m,n] + deltaWeight[index][m,n];
                }
            }
        }
    public void countDerivativePrevAndDeltaWeightPrev(int index, double[] left, double[] right, double[] error)
        {
            for (int n = 0; n < right.Length; n++)
            {
                for (int m = 0; m < left.Length; m++)
                {
                    if (left == hiddenValues)
                    {
                        proizvodnayaPrev[index][m,n] = left[m] * error[n];
                    }
                    else proizvodnayaPrev[index][m,n] = error[n];
                    deltaWeightPrev[index][m,n] = -learningRateFactor * proizvodnayaPrev[index][m,n];
                }
            }
        }

    public void countBias(int index, double[] layer, double[] error)
        {
            for (int y = 0; y < layer.Length; y++)
            {
                bias[index,y] += -learningRateFactor * error[y];
            }
        }

    public void study()
        {
            double[] err = new double[hiddenValues.Length];
            globalNetworkError = 0.0;
            int h = 0;
            double sum = 0;
            int k = 1;
            double[] lErr = new double[outputValues.Length]; // ошибка выходных нейронов
            while (h != patterns.Count())
            {
                for (int p = 0; p < Const.NUMBER_OUTPUT_NEURONS; p++)
                {
                    if (k == 2)
                    {
                        h--;
                        inputValues = patterns[h];
                        p--;
                        k++;
                    }
                    else
                    {
                        inputValues = patterns[h];
                    }
                    countValues();
                    for (int n = 0; n < outputValues.Length; n++)
                    {
                        if (n == answers[p])
                        {
                            lErr[n] = (outputValues[n] - 1) * (1 - outputValues[n]);
                        }
                        else lErr[n] = (outputValues[n] - 0) * (1 - outputValues[n]);
                        globalNetworkError += Math.Abs(lErr[n]);
                    }
                    for (int j = 0; j < hiddenValues.Length; j++)
                    {
                        sum = 0;
                        for (int n = 0; n < outputValues.Length; n++)
                        {
                            sum += lErr[n] * weightsPrev[1][j,n];
                        }
                        err[j] = sum * hiddenValues[j] * (1 - hiddenValues[j]);
                    }
                    if (k == 1)
                    {
                        countDerivativePrevAndDeltaWeightPrev(1, hiddenValues, outputValues, lErr);
                        countDerivativePrevAndDeltaWeightPrev(0, inputValues, hiddenValues, err);
                        k++;
                    }
                    else
                    {
                        countWeights(1, hiddenValues, outputValues, lErr);
                        countWeights(0, inputValues, hiddenValues, err);
                        proizvodnayaPrev = proizvodnaya;
                        weightsPrev = weights;
                    }
                    countBias(1, outputValues, lErr);
                    countBias(0, hiddenValues, err);
                    h++;
                }
            }
        }

    public void Answer()
        {
            int found;
            for (int p = 0; p < Const.NUMBER_OUTPUT_NEURONS; p++)
            {
                inputValues = pixelsValues;
                countValues();
                found = 0;
                for (int i = 1; i < outputValues.Length; i++)
                {
                    if (outputValues[i] > outputValues[found])
                    {
                        found = i;
                    }
                }
                if (found == answers[p])
                {
                    answer = Const.ARRAY_LETTERS[answers[p]];
                    Network.setAnswer(answer);
                }
            }
        }
    }
}