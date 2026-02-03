using APIPizzeria.DataBase;
using APIPizzeria.Models;

namespace APIPizzeria.Services
{
    public class PizzaService
    {
        private readonly PizzaData _pizzaData;

        public PizzaService()
        {
            _pizzaData = new PizzaData();
        }

        public List<Pizza> ObtenerPizzas()
        {
           //Veo el catalogo completo, tengo que poner validaciones
            return _pizzaData.VerCatalogo();
        }

        public Pizza ObtenerPizzaPorID(int ID) 
        {
            return _pizzaData.VerPizzaPorID(ID).FirstOrDefault();
        }
        public void CrearPizza(Pizza pizza)
        {
            if (String.IsNullOrEmpty(pizza.Nombre)) { throw new Exception("El nombre de la pizza es obligatorio"); }
            if (String.IsNullOrEmpty(pizza.Descripcion)) { throw new Exception("Por favor indique una breve descripción del producto"); }
            if (String.IsNullOrEmpty(pizza.Nombre)) { throw new Exception("El nombre de la pizza es obligatorio"); }
            _pizzaData.Insertar(pizza);
        }

        public void ActualizarPizza(Pizza pizza, int id) 
        {
            if (String.IsNullOrEmpty(pizza.Nombre)) { throw new Exception("El nombre de la pizza es obligatorio"); }
            if (String.IsNullOrEmpty(pizza.Descripcion)) { throw new Exception("Por favor indique una breve descripción del producto"); }
            if (String.IsNullOrEmpty(pizza.Nombre)) { throw new Exception("El nombre de la pizza es obligatorio"); }
            _pizzaData.Actualizar(pizza, id);
        }
        public void EliminarPizza(int ID) 
        {
            _pizzaData.Eliminar(ID);
        }
    }
}
