using MediatRPoC.Application.Models;

namespace MediatRPoC.Interfaces
{
    public class PersonRepository : IRepository<Person>
    {
        private static Dictionary<int, Person> persons = new Dictionary<int, Person>();

        public async Task<IEnumerable<Person>> GetAll()
        {
            return await Task.Run(() => persons.Values.ToList());
        }

        public async Task<Person> Get(int id)
        {
            return await Task.Run(() => persons.GetValueOrDefault(id));
        }

        public async Task<Person> Add(Person person)
        {
            person.Id = persons.Count+1;
            await Task.Run(() => persons.Add(person.Id, person));
            return person;
        }

        public async Task<Person> Update(Person person)
        {
            await Task.Run(() => persons.Remove(person.Id));
            await Task.Run(() => persons.Add(person.Id, person));

            return person;
        }

        public async Task Delete(int id)
        {
            await Task.Run(() => persons.Remove(id));
        }
    }
}
