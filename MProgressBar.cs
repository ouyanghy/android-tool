using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace android_tool
{
    public class MProgressBar
    {
        private static SynchronizationContext mSyn;
        private Thread mThreadShowProgress;
        private Thread mThreadDismissProgress;
        private static long STEP = 440 * 1024;// 4200bytes/s
        private  ProgressBar progressBarCur;
        private  ProgressBar progressBarTotal;


        public MProgressBar(SynchronizationContext syn, ProgressBar cur, ProgressBar total)
        {
            mSyn = syn;
            progressBarCur = cur;
            progressBarTotal = total;
            Console.WriteLine("cur:" + cur);
            Console.WriteLine("total:" + total);
        }

        public void setProgressBar(ProgressBar cur, ProgressBar total)
        {
            progressBarCur = cur;
            progressBarTotal = total;
        }

        public void showProgress(CmdOption option)
        {
            mThreadShowProgress = new Thread(new ParameterizedThreadStart(showProgressThread));
            mThreadShowProgress.Start(option);
        }

        //show progress
        private void showProgressThread(Object obj)
        {
            CmdOption option = (CmdOption)obj;

            long size = 0;
            int percent = 0;
            int totalPercent = 0;
            long totalValue = 0;
            long value = 0;
            int state = Enums.WorkState.RUNNING;

            for (int i = 0; i < option.length; i++)
            {
                size = option.size[i];
                value = 0;
                percent = 0;

                if (size <= 0)
                    continue;
                state = Enums.WorkState.RUNNING;
                while (value < size || state == Enums.WorkState.RUNNING)
                {
                    percent = (int)(((float)value / (float)(size)) * 100);
                    percent = (percent > 100) ? 100 : percent;

                    if (state == Enums.WorkState.SUCC)
                    {
                        value = size;
                        percent = 100;
                        break;

                    }
                    else if (state == Enums.WorkState.ERROR)
                    {
                        value = size;
                        percent = 100;                    
                        break;

                    }
                    else
                    {
                        percent = (percent >= 100) ? 99 : percent;
                        if (percent < 99)
                        {
                            value += STEP;
                            totalValue += STEP;
                            totalPercent = (int)(((float)totalValue / (float)option.sizeTotal) * 100);
                            totalPercent = totalPercent >= 99 ? 99 : totalPercent;
                            Console.WriteLine("toatlval:" + totalValue + " size:" + option.sizeTotal + " pecent:" + totalPercent);
                            mSyn.Post(__showProgressTotal, totalPercent);
                        }

                        else
                            value = size - 1;
                    }
                    //   Console.WriteLine("cur percent:" + percent + " size:" + size);
                    mSyn.Post(__showProgressCur, percent);

                    Thread.Sleep(100);
                    state = option.state[i];

                }
                mSyn.Post(__showProgressCur, 100);
                //    Console.WriteLine("size:" + option.size[i] + " total:" + option.sizeTotal);


            }
            mSyn.Post(__showProgressTotal, 100);
        }

        private void __showProgressCur(Object get)
        {
            progressBarCur.Value = (int)get;
            progressBarCur.Update();
        }

        public void __showProgressTotal(Object get)
        {
            progressBarTotal.Value = (int)get;
            progressBarTotal.Update();
        }


        public void dismissProgress(int bar)
        {
            mThreadDismissProgress = new Thread(new ParameterizedThreadStart(dismissProgressThread));
            mThreadDismissProgress.Start(bar);
        }

        private void dismissProgressThread(Object obj)
        {
            mSyn.Post(__dismissProgress, 0);

        }

        private void __dismissProgress(Object obj)
        {
            for (int i = 100; i >= 0; i -= 10)
            {
                progressBarCur.Value = i;
                progressBarTotal.Value = i;
            
                progressBarCur.Update();
                Thread.Sleep(20);
            }

        }
    }
}
