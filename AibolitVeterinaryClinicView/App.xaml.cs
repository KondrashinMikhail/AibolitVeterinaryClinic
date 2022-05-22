using AibolitVeterinaryClinicBusinessLogic.BusinessLogics;
using AibolitVeterinaryClinicBusinessLogic.OfficePackage;
using AibolitVeterinaryClinicBusinessLogic.OfficePackage.Implements;
using AibolitVeterinaryClinicContracts.BindingModels;
using AibolitVeterinaryClinicContracts.BusinessLogicsContracts;
using AibolitVeterinaryClinicContracts.StoragesContracts;
using AibolitVeterinaryClinicDatabaseImplement.Implements;
using System.Windows;
using System.Configuration;
using Unity;
using Unity.Lifetime;
using System;

namespace AibolitVeterinaryClinicView
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static IUnityContainer container = null;
        public static IUnityContainer Container
        {
            get
            {
                if (container == null) container = BuildUnityContainer();
                return container;
            }
        }
        protected void App_StartUp(object sender, StartupEventArgs e) 
        {
            var mailSender = Container.Resolve<MailLogic>();
            mailSender.MailConfig(new MailConfigBindingModel
            {
                SmtpClientHost = ConfigurationManager.AppSettings["SmtpClientHost"],
                SmtpClientPort = Convert.ToInt32(ConfigurationManager.AppSettings["SmtpClientPort"]),
                MailLogin = ConfigurationManager.AppSettings["MailLogin"],
                MailPassword = ConfigurationManager.AppSettings["MailPassword"],
                MailName = ConfigurationManager.AppSettings["MailName"]
            });
            Container.Resolve<MainWindow>().Show();
        }
        private static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<IAnimalStorage, AnimalStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IClientStorage, ClientStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IDoctorStorage, DoctorStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMedicineStorage, MedicineStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IServiceStorage, ServiceStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IVaccinationStorage, VaccinationStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IVisitStorage, VisitStorage>(new HierarchicalLifetimeManager());
           
            currentContainer.RegisterType<IAnimalLogic, AnimalLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IClientLogic, ClientLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IDoctorLogic, DoctorLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMedicineLogic, MedicineLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IServiceLogic, ServiceLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IVaccinationLogic, VaccinationLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IVisitLogic, VisitLogic>(new HierarchicalLifetimeManager());

            currentContainer.RegisterType<IReportLogic, ReportLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<AbstractSaveToWord, SaveToWord>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<AbstractSaveToExcel, SaveToExcel>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<AbstractSaveToPdf, SaveToPdf>(new HierarchicalLifetimeManager());

            currentContainer.RegisterType<MailLogic>(new SingletonLifetimeManager());
            return currentContainer;
        }
    }
}
