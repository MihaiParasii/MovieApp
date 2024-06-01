using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChineseNetflix.Models;

public class Customer(
    int customerId,
    string name,
    string surname,
    string email,
    // GenresRates genresRates,
    string password,
    DateTime birthDate)
    : Person(name, surname, birthDate)
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public required int CustomerId { get; init; } = customerId;

    [Required, MaxLength(40)] public string Email { get; set; } = email;

    [Required, MaxLength(64)] public string Password { get; set; } = password;

    // private GenresRates _genresRates = genresRates;
    private readonly List<Movie> _watchedMovies = [];
    private readonly List<Movie> _ratedMovies = [];


    // public void RateGenres(float dramaRate, float actionRate, float comedyRate) =>
    // _genresRates = new GenresRates(dramaRate, comedyRate, actionRate);

    public void WatchMovie(Movie movie) => _watchedMovies.Add(movie);

    public void RateMovie(Movie movie, float rate)
    {
        movie.RateMovie(rate);
        _ratedMovies.Add(movie);
    }

    // public List<Movie> GetMoviesOfClosestCustomer(List<Customer> customers) =>
    // GetClosestCustomer(customers)._ratedMovies;


    // private Customer GetClosestCustomer(List<Customer> customers)
    // {
    // float minDistance = GetDistance(customers[0]);
    // Customer closestCustomer = customers[0];

    // foreach (Customer customer in customers.Where(customer => customer.CustomerId != CustomerId)
    // .Where(customer => GetDistance(customer) < minDistance))
    // {
    // closestCustomer = customer;
    // minDistance = GetDistance(customer);
    // }

    // return closestCustomer;
    // }

    // private float GetDistance(Customer customer)
    // {
    // float deltaComedyRate = _genresRates.ComedyRate - customer._genresRates.ComedyRate;
    // float deltaDramaRate = _genresRates.DramaRate - customer._genresRates.DramaRate;
    // float deltaActionRate = _genresRates.ActionRate - customer._genresRates.ActionRate;

    // float distance = float.Sqrt(float.Pow(deltaActionRate, 2) +
    // float.Pow(deltaDramaRate, 2) +
    // float.Pow(deltaComedyRate, 2));

    // return distance;
    // }
}
