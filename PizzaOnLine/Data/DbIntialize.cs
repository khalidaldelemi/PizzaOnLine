﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PizzaOnLine.Models;

namespace PizzaOnLine.Data
{
    public static class DbIntialize
    {
        public static void Initialize(UserManager<ApplicationUser> userManager, ApplicationDbContext context, RoleManager<IdentityRole> roleManager)
        {
            var aUser = new ApplicationUser
            {
                UserName = "Test@test.com",
                Email = "Test@test.com"
            };
            var userResult = userManager.CreateAsync(aUser, "Pa$$w0rd").Result;

            var adminRole = new IdentityRole { Name = "Admin" };
            var roleResult = roleManager.CreateAsync(adminRole).Result;

            var adminUser = new ApplicationUser
            {
                UserName = "admin@test.com",
                Email = "admin@test.com"
            };
            var adminUserResult = userManager.CreateAsync(adminUser, "Pa$$w0rd").Result;

            var roleAddedResult = userManager.AddToRoleAsync(adminUser, "Admin").Result;

            if (!context.Dishes.Any())
            {
                var pizza = new Category { Name = "Pizza" };
                var pasta = new Category { Name = "Pasta" };
                var salad = new Category { Name = "Salad" };

                
                var cheese = new Ingredient { Name = "Cheese" ,Price= 5};
                var tomato = new Ingredient { Name = "Tomato" ,Price=5};
                var ham = new Ingredient { Name = "Ham" ,Price=10};
                var bannan = new Ingredient { Name = "Bannan", Price = 10 };
                var mashrom = new Ingredient { Name = "Mashroom", Price = 10 };
        
                var capricciosa = new Dish { Name = "Capricciosa", Price = 80 };
                var margarita = new Dish { Name = "Margarita", Price = 70 };
                var hawaii = new Dish { Name = "Hawaii", Price = 100 };

                var margaritaCheese = new DishIngredient { Dish = margarita, Ingredient = cheese ,Enabel=true };
                var margaritaTomato= new DishIngredient { Dish = margarita, Ingredient = tomato, Enabel = true };

                var hawaiiHam = new DishIngredient { Dish = hawaii, Ingredient = ham, Enabel = true };
                var hawaiiBannan = new DishIngredient { Dish = hawaii, Ingredient = bannan, Enabel = true };
                var hawaiiTomato = new DishIngredient { Dish = hawaii, Ingredient = tomato, Enabel = true };
                var hawaiiCheese = new DishIngredient { Dish = hawaii, Ingredient = cheese, Enabel = true };

                var capricciosaCheese = new DishIngredient { Dish = capricciosa, Ingredient = cheese, Enabel = true };
                var capricciosaHam = new DishIngredient { Dish = capricciosa, Ingredient = ham, Enabel = true };
                var capricciosaTomato = new DishIngredient { Dish = capricciosa, Ingredient = tomato, Enabel = true };
                pizza.dishes = new List<Dish>
                 {
                     capricciosa,margarita,hawaii
                 };

                capricciosa.DishIngredient = new List<DishIngredient> {capricciosaCheese, capricciosaTomato,capricciosaHam};


                margarita.DishIngredient = new List<DishIngredient>{ margaritaCheese,margaritaTomato};
                hawaii.DishIngredient = new List<DishIngredient> { hawaiiCheese, hawaiiHam, hawaiiBannan, hawaiiTomato };

                context.AddRange(tomato, ham, cheese);

                context.Categories.AddRange(pizza, pasta, salad);

                
                context.DishIngredients.AddRange(margaritaCheese, margaritaTomato,
                                                  hawaiiBannan, hawaiiTomato, hawaiiCheese, hawaiiHam,
                                                 capricciosaHam, capricciosaCheese, capricciosaTomato);
                context.AddRange(capricciosa, margarita, hawaii);
                context.SaveChanges();
            }
        }
        
    }
}
