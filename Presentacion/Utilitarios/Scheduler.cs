using Quartz;
using Quartz.Impl;
using System;
using static Quartz.MisfireInstruction;

namespace Presentacion.Utilitarios
{
    public class Scheduler
    {
        ////CsBoletosWeb _CsBoletosWeb = new CsBoletosWeb();
       
        public void Start()
        {
            //trigger SE ESTA EJECUTANDO CADA 59 MINUTOS, PERO NO EJECUTA AL INICIAR
            //APLICACION (NO SE PUDO CONSEGUIR ESO)
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();
            IJobDetail job = JobBuilder.Create<Job>().Build();

            ITrigger trigger1 = TriggerBuilder.Create()
               //.WithIdentity("IDGJob", "IDG")
               //.StartAt(date)            
               ////.WithCronSchedule("0 1 * * * ?")//NO FUNCIONA
               .StartNow()
               .WithPriority(1)
               .WithDailyTimeIntervalSchedule
                    (s =>
                        s.WithIntervalInMinutes(59)// SE EJECUTA CADA 59 MINUTOS
                    .OnEveryDay()
                    )
               ////«Segundos» «Minutos» «Horas» «Día del mes» «Mes» «Día de la semana» «Año»
               ////.WithCronSchedule("0 30 6 1/1 * ? *")
               ////.WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(18, 13))              
               .Build();

            scheduler.ScheduleJob(job, trigger1);
        }

        //*** FUNCIONA
        public void Start1()
        {
            IJobDetail job = JobBuilder.Create<Job>().StoreDurably().WithIdentity("J_Email", "J_Mailing").Build();
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            //Agregue el IJob dado al Programador, sin ITrigger asociado
            scheduler.AddJob(job, true /* bool replace */ );


            ITrigger trigger1 = TriggerBuilder.Create()
                                .WithIdentity("MailTrigger1", "T_Mail1")
                                .StartNow()
                                .WithSimpleSchedule(x => x.WithMisfireHandlingInstructionIgnoreMisfires()
                                    .WithIntervalInHours(1)
                                    .RepeatForever())
                                //.WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(08, 00))
                                //.WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(12, 49))
                                .ForJob(job)
                                .Build();


            scheduler.ScheduleJob(trigger1);
            scheduler.Start();
        }

    }
}
