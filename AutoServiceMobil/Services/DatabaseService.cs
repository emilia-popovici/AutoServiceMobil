using AutoServiceMobil.Models;
using Microsoft.Maui.Storage;
using SQLite;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;

namespace AutoServiceMobil.Services;

public class DatabaseService
{
    private readonly SQLiteAsyncConnection _db;

    public ObservableCollection<Serviciu> ListaServicii { get; set; } = new();
    public ObservableCollection<Mecanic> ListaMecanici { get; set; } = new();
    public ObservableCollection<Programare> ListaProgramari { get; set; } = new();
    public ObservableCollection<Review> ListaReviewuri { get; set; } = new();

    public DatabaseService()
    {
        var path = Path.Combine(FileSystem.AppDataDirectory, "autoservice.db");
        _db = new SQLiteAsyncConnection(path);

        _db.CreateTableAsync<User>().Wait();
        _db.CreateTableAsync<Serviciu>().Wait();
        _db.CreateTableAsync<Mecanic>().Wait();
        _db.CreateTableAsync<Programare>().Wait();
        _db.CreateTableAsync<Review>().Wait();

        CreateDefaultAdmin();
        LoadAllData().Wait();
    }

    private async Task LoadAllData()
    {
        ListaServicii = new ObservableCollection<Serviciu>(await _db.Table<Serviciu>().ToListAsync());
        ListaMecanici = new ObservableCollection<Mecanic>(await _db.Table<Mecanic>().ToListAsync());
        ListaProgramari = new ObservableCollection<Programare>(await _db.Table<Programare>().ToListAsync());
        ListaReviewuri = new ObservableCollection<Review>(await _db.Table<Review>().ToListAsync());
    }

    private void CreateDefaultAdmin()
    {
        var admin = _db.Table<User>()
                       .Where(u => u.Rol == "admin")
                       .FirstOrDefaultAsync()
                       .Result;

        if (admin == null)
        {
            _db.InsertAsync(new User
            {
                Nume = "Admin",
                Prenume = "Principal",
                Email = "admin@autoservice.ro",
                Telefon = "0000000000",
                Parola = "admin123",
                Rol = "admin"
            }).Wait();
        }
    }

    public Task<User> GetUserByEmailAsync(string email)
        => _db.Table<User>().Where(u => u.Email == email).FirstOrDefaultAsync();

    public Task<int> AddUserAsync(User user)
        => _db.InsertAsync(user);

    public Task<User> GetUserByIdAsync(int id)
        => _db.Table<User>().Where(u => u.Id == id).FirstOrDefaultAsync();

    public Task<List<User>> GetAllUsersAsync()
        => _db.Table<User>().ToListAsync();

    public Task<int> SaveUserAsync(User user)
        => user.Id != 0 ? _db.UpdateAsync(user) : _db.InsertAsync(user);

    public async Task AddServiciuAsync(Serviciu s)
    {
        await _db.InsertAsync(s);
        ListaServicii.Add(s);
    }

    public async Task UpdateServiciuAsync(Serviciu s)
    {
        await _db.UpdateAsync(s);
    }

    public async Task DeleteServiciuAsync(Serviciu s)
    {
        await _db.DeleteAsync(s);
        ListaServicii.Remove(s);
    }

    public async Task AdaugaMecanicAsync(Mecanic m)
    {
        await _db.InsertAsync(m);
        ListaMecanici.Add(m);
    }

    public Task UpdateMecanicAsync(Mecanic m)
        => _db.UpdateAsync(m);

    public async Task StergeMecanicAsync(int id)
    {
        await _db.DeleteAsync<Mecanic>(id);
        var item = ListaMecanici.FirstOrDefault(x => x.Id == id);
        if (item != null)
            ListaMecanici.Remove(item);
    }

    public async Task AdaugaProgramareAsync(Programare p)
    {
        await _db.InsertAsync(p);
        ListaProgramari.Add(p);
    }

    public Task UpdateProgramareAsync(Programare p)
        => _db.UpdateAsync(p);

    public async Task StergeProgramareAsync(int id)
    {
        await _db.DeleteAsync<Programare>(id);
        var item = ListaProgramari.FirstOrDefault(x => x.Id == id);
        if (item != null)
            ListaProgramari.Remove(item);
    }

    public async Task AdaugaReviewAsync(Review review)
    {
        await _db.InsertAsync(review);
        ListaReviewuri.Add(review);
    }
}