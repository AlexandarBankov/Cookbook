using Cookbook.Controllers;
using Cookbook.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Cookbook.Views
{
    public class Display
    {
        private const string closeOperationCommand = "7";

        private RecipeTagController recipeTagController;
        private TagController tagController;
        private RecipeController recipeController;
        private MealTypeController mealTypeController;

        public Display()
        {
            recipeTagController = new RecipeTagController();
            tagController = new TagController();
            recipeController = new RecipeController();
            mealTypeController = new MealTypeController();
            HandleInput();
        }

        private void ShowCommands()
        {
            Console.WriteLine(new string('_',40));
            Console.WriteLine("List of the general commands:");
            Console.WriteLine("1.List all type of commands.");
            Console.WriteLine("2.Search/List by type of commands.");
            Console.WriteLine("3.Get by id type of commands.");
            Console.WriteLine("4.Add type of commands.");
            Console.WriteLine("5.Remove type of commands.");
            Console.WriteLine("6.Update type of commands.");
            Console.WriteLine("7.Exit.");
            Console.WriteLine(new string('_', 40));
        }

        private void HandleInput()
        {
            string input;
            do
            {
                ShowCommands();
                input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        ListAllCommands();
                        break;
                    case "2":
                        GetByCommands();
                        break;
                    case "3":
                        GetByIdCOmmands();
                        break;
                    case "4":
                        AddCommands();
                        break;
                    case "5":
                        RemoveCommands();
                        break;
                    case "6":
                        UpdateCommands();
                        break;
                    default:
                        break;
                }
            } while (input!=closeOperationCommand);
        }

        private void ListAllRecipes()
        {
            List<Recipe> recipes = recipeController.GetAllRecipes();
            foreach (Recipe recipe in recipes)
            {
                PrintRecipe(recipe);
            }
        }

        private void PrintRecipe(Recipe recipe)
        {
            Console.WriteLine("ID: " + recipe.Id);
            Console.WriteLine("Name: " + recipe.Name);
            Console.WriteLine("Ingredients: " + recipe.Ingredients);
            Console.WriteLine("Preparation: " + recipe.Preparation);
            Console.WriteLine("Meal type: " + recipe.MealType.Name);
            Console.WriteLine("Tags: ");
            foreach (RecipeTag recipeTag in recipe.RecipeTags)
            {
                Console.Write(recipeTag.Tag.Name + " ");
            }
            Console.WriteLine();
            Console.WriteLine(new string('_', 40));
        }

        private void ListAllMealTypes()
        {
            List<MealType> mealTypes = mealTypeController.GetAllMealTypes();
            foreach (MealType mealType in mealTypes)
            {
                Console.WriteLine("ID: " + mealType.Id);
                Console.WriteLine("Name: " + mealType.Name);
                Console.WriteLine(new string('_',40));
            }
        }

        private void ListAllTags()
        {
            List<Tag> tags = tagController.GetAllTags();
            foreach (Tag tag in tags)
            {
                Console.WriteLine("ID: " + tag.Id);
                Console.WriteLine("Name: " + tag.Name);
                Console.WriteLine(new string('_',40));
            }
        }

        private void ListAllCommands()
        {
            string input;
            Console.WriteLine("1.List all recipes.");
            Console.WriteLine("2.List all tags.");
            Console.WriteLine("3.List all meal types.");
            input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    ListAllRecipes();
                    break;
                case "2":
                    ListAllTags();
                    break;
                case "3":
                    ListAllMealTypes();
                    break;
                default:
                    break;
            }
        }

        private void GetByCommands()
        {
            Console.WriteLine("1.List all recipes with a given meal type.");
            Console.WriteLine("2.List all recipes with a given tag.");
            Console.WriteLine("3.Search a recipe by name.");
            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    GetAllRecipesWithMealType();
                    break;
                case "2":
                    GetAllRecipesWithTag();
                    break;
                case "3":
                    GetRecipeByName();
                    break;
                default:
                    break;
            }
        }

        private void GetAllRecipesWithMealType()
        {
            Console.WriteLine("Enter meal type name:");
            
            string name = Console.ReadLine();
            MealType mealType = mealTypeController.GetAllMealTypes().FirstOrDefault(mt => mt.Name == name);
            if (mealType == null)
            {
                Console.WriteLine("No tags with that name were found.");
            }
            else
            {
                List<Recipe> recipes = mealType.Recipes.ToList();
                foreach (Recipe recipe in recipes)
                {
                    PrintRecipe(recipe);
                }
            }
            
        }

        private void GetAllRecipesWithTag()
        {
            Console.WriteLine("Enter tag name:");
            string name = Console.ReadLine();
            Tag tag = tagController.GetAllTags().FirstOrDefault(t => t.Name == name);
            if (tag == null)
            {
                Console.WriteLine("No tags with that name were found.");
            }
            else
            {
                List<RecipeTag> recipesTags = tag.RecipeTags.ToList();
                foreach (RecipeTag recipeTag in recipesTags)
                {
                    PrintRecipe(recipeTag.Recipe);
                }
            }
        }

        private void GetRecipeByName()
        {
            Console.WriteLine("Enter recipe name:");
            string name = Console.ReadLine();
            List<Recipe> recipes = recipeController.GetRecipeByName(name);
            if (recipes.Count==0)
            {
                Console.WriteLine("No recipes with that name were found.");
            }
            else
            {
                foreach (Recipe recipe in recipes)
                {
                    PrintRecipe(recipe);
                }
            }
        }

        private void GetByIdCOmmands()
        {
            Console.WriteLine("1.Get recipe by id.");
            Console.WriteLine("2.Get meal type by id.");
            Console.WriteLine("3.Get tag by id.");
            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    GetRecipeById();
                    break;
                case "2":
                    GetMealTypeById();
                    break;
                case "3":
                    GetTagById();
                    break;
                default:
                    break;
            }
        }

        private void GetMealTypeById()
        {
            Console.WriteLine("Enter meal type id:");
            int id;
            try
            {
                id = int.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("No such id.");
                return;
            }
            MealType mealType = mealTypeController.GetMealType(id);
            if (mealType == null)
            {
                Console.WriteLine("No such id.");
            }
            else
            {
                Console.WriteLine("Name: " + mealType.Name);
            }
        }

        private void GetRecipeById()
        {
            Console.WriteLine("Enter recipe id:");
            int id;
            try
            {
                id = int.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("No such id.");
                return;
            }

            Recipe recipe = recipeController.GetRecipe(id);

            if (recipe==null)
            {
                Console.WriteLine("No such id.");
            }
            else
            {
                PrintRecipe(recipe);
            }
        }

        private void GetTagById()
        {
            Console.WriteLine("Enter tag id:");
            int id;
            try
            {
                id = int.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("No such id.");
                return;
            }
            Tag tag = tagController.GetTag(id);
            if (tag == null)
            {
                Console.WriteLine("No such id.");
            }
            else
            {
                Console.WriteLine("Name: " + tag.Name);
            }
        }

        private void AddCommands()
        {
            Console.WriteLine("1.Add a recipe.");
            Console.WriteLine("2.Add a tag.");
            Console.WriteLine("3.Add a meal type.");
            Console.WriteLine("4.Add a tag to a recipe.");
            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    AddRecipe();
                    break;
                case "2":
                    AddTag();
                    break;
                case "3":
                    AddMealType();
                    break;
                case "4":
                    AddTagToRecipe();
                    break;
                default:
                    break;
            }
        }

        private void AddMealType()
        {
            Console.WriteLine("Enter meal type name:");
            string input = Console.ReadLine();
            MealType mealType = mealTypeController.GetAllMealTypes().FirstOrDefault(mt => mt.Name == input);
            if (mealType!=null)
            {
                Console.WriteLine("Meal type already exists and has id:" + mealType.Id);
            }
            else
            {
                mealTypeController.AddMealType(new MealType() { Name = input });
            }
        }

        private void AddTag()
        {
            Console.WriteLine("Enter tag name:");
            string input = Console.ReadLine();
            Tag tag = tagController.GetAllTags().FirstOrDefault(t => t.Name == input);
            if (tag != null)
            {
                Console.WriteLine("Tag already exists and has id:" + tag.Id);
            }
            else
            {
                tagController.AddTag(new Tag() { Name = input });
            }
        }

        private void AddRecipe()
        {
            Recipe recipe = new Recipe();
            Console.WriteLine("Enter recipe name:");
            recipe.Name = Console.ReadLine();
            Console.WriteLine("Enter ingredients:");
            recipe.Ingredients = Console.ReadLine();
            Console.WriteLine("Enter preparation:");
            recipe.Preparation = Console.ReadLine();
            Console.WriteLine("Enter meal type id:");
            recipe.MealTypeId = int.Parse(Console.ReadLine());
            recipe.RecipeTags = new List<RecipeTag>();
            recipeController.AddRecipe(recipe);
        }

        private void AddTagToRecipe()
        {
            Console.WriteLine("Enter recipe id:");
            int recipeId = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter tag id:");
            int tagId = int.Parse(Console.ReadLine());
            recipeTagController.AddRecipeTag(new RecipeTag() { RecipeId = recipeId, TagId = tagId });
        }

        private void RemoveCommands()
        {
            Console.WriteLine("1.Remove recipe.");
            Console.WriteLine("2.Remove tag.");
            Console.WriteLine("3.!Remove meal type!.");
            Console.WriteLine("4.Remove a tag from a recipe.");
            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    RemoveRecipe();
                    break;
                case "2":
                    RemoveTag();
                    break;
                case "3":
                    RemoveMealType();
                    break;
                case "4":
                    RemoveTagFromRecipe();
                    break;
                default:
                    break;
            }
        }

        private void RemoveTagFromRecipe()
        {
            Console.WriteLine("Enter recipe id:");
            int recipeId = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter tag id:");
            int tagId = int.Parse(Console.ReadLine());
            recipeTagController.DeleteRecipeTag(recipeId,tagId);
        }

        private void RemoveMealType()
        {
            Console.WriteLine("!You cannot remove a meal type if it is still used in a recipe without deleting the recipe!");
            Console.WriteLine("Enter meal type id:");
            int id = int.Parse(Console.ReadLine());
            mealTypeController.DeleteMealType(id);
        }

        private void RemoveTag()
        {
            Console.WriteLine("Enter tag id:");
            int id = int.Parse(Console.ReadLine());
            tagController.DeleteTag(id);
        }

        private void RemoveRecipe()
        {
            Console.WriteLine("Enter recipe id:");
            int id = int.Parse(Console.ReadLine());
            recipeController.DeleteRecipe(id);
        }

        private void UpdateCommands()
        {
            Console.WriteLine("1.Update tag.");
            Console.WriteLine("2.Update meal type.");
            Console.WriteLine("3.Update recipe.");
            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    UpdateTag();
                    break;
                case "2":
                    UpdateMealType();
                    break;
                case "3":
                    UpdateRecipe();
                    break;
                default:
                    break;
            }
        }

        private void UpdateRecipe()
        {
            Recipe recipe = new Recipe();
            Console.WriteLine("Enter recipe id:");
            int id = int.Parse(Console.ReadLine());
            recipe.Id = id;
            Console.WriteLine("Enter recipe name:");
            recipe.Name = Console.ReadLine();
            Console.WriteLine("Enter ingredients:");
            recipe.Ingredients = Console.ReadLine();
            Console.WriteLine("Enter preparation:");
            recipe.Preparation = Console.ReadLine();
            Console.WriteLine("Enter meal type id:");
            recipe.MealTypeId = int.Parse(Console.ReadLine());
            recipe.RecipeTags = new List<RecipeTag>();
            recipeController.UpdateRecipe(recipe);

        }

        private void UpdateMealType()
        {
            MealType mealType = new MealType();
            Console.WriteLine("Enter meal type id:");
            int id = int.Parse(Console.ReadLine());
            mealType.Id = id;
            Console.WriteLine("Enter new name:");
            string name = Console.ReadLine();
            mealType.Name = name;
            mealTypeController.UpdateMealType(mealType);
        }

        private void UpdateTag()
        {
            Tag tag = new Tag();
            Console.WriteLine("Enter tag id:");
            int id = int.Parse(Console.ReadLine());
            tag.Id = id;
            Console.WriteLine("Enter new name:");
            string name = Console.ReadLine();
            tag.Name = name;
            tagController.UpdateTag(tag);
        }
    }
}
