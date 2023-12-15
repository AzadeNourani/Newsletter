using Microsoft.EntityFrameworkCore;
using NewsletterAPI.Data.Models;
using System;

public interface ISendNewsToPersonnelListService
{
    Task ExecuteAsync();
    //Task<List<...DTO>> ExecuteAsync();
}
