using AibolitVeterinaryClinicContracts.BindingModels;
using AibolitVeterinaryClinicContracts.StoragesContracts;
using AibolitVeterinaryClinicContracts.ViewModels;
using AibolitVeterinaryClinicDatabaseImplement.Models;

namespace AibolitVeterinaryClinicDatabaseImplement.Implements
{
    public class ClientStorage : IClientStorage
    {
        public List<ClientViewModel> GetFullList()
        {
            using var context = new AibolitVeterinaryClinicDatabase();
            return context.Clients.Select(CreateModel).ToList();
        }
        public List<ClientViewModel> GetFilteredList(ClientBindingModel model)
        {
            if (model == null) return null;
            using var context = new AibolitVeterinaryClinicDatabase();
            return context.Clients.Where(rec => rec.ClientName.Contains(model.ClientName)).Select(CreateModel).ToList();
        }
        public ClientViewModel GetElement(ClientBindingModel model)
        {
            if (model == null) return null;
            using var context = new AibolitVeterinaryClinicDatabase();
            var client = context.Clients.FirstOrDefault(rec => rec.ClientName == model.ClientName || rec.Id == model.Id);
            return client != null ? CreateModel(client) : null;
        }
        public void Insert(ClientBindingModel model)
        {
            using var context = new AibolitVeterinaryClinicDatabase();
            context.Clients.Add(CreateModel(model, new Client(), context));
            context.SaveChanges();
        }
        public void Update(ClientBindingModel model)
        {
            using var context = new AibolitVeterinaryClinicDatabase();
            var element = context.Clients.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null) throw new Exception("Клиент не найден");
            CreateModel(model, element, context);
            context.SaveChanges();
        }
        public void Delete(ClientBindingModel model)
        {
            using var context = new AibolitVeterinaryClinicDatabase();
            var element = context.Clients.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null) throw new Exception("Клиент не найден");
            context.Clients.Remove(element);
            context.SaveChanges();
        }
        private static Client CreateModel(ClientBindingModel model, Client client, AibolitVeterinaryClinicDatabase context) 
        {
            client.ClientLogin = model.ClientLogin;
            client.ClientName = model.ClientName;
            client.ClientPhoneNumber = model.ClientPhoneNumber;
            foreach (var animal in model.ClientAnimals) 
            {
                client.Animals.Add(new Animal
                {
                    ClientId = client.Id,
                    AnimalName = context.Animals.FirstOrDefault(rec => rec.Id == animal).AnimalName,
                    AnimalBreed = context.Animals.FirstOrDefault(rec => rec.Id == animal).AnimalBreed
                });
                context.SaveChanges();
            }
            return client;
        }
        private static ClientViewModel CreateModel(Client client) 
        {
            using var context = new AibolitVeterinaryClinicDatabase();
            List<int> list = new List<int>();
            foreach (var animal in client.Animals) if (animal.ClientId == client.Id) list.Add(animal.Id);
            return new ClientViewModel
            {
                Id = client.Id,
                ClientLogin = client.ClientLogin,
                ClientName = client.ClientName,
                ClientPhoneNumber = client.ClientPhoneNumber,
                ClientAnimals = list
            };
        }
    }
}
