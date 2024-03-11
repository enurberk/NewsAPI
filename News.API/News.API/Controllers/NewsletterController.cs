using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using News.API.Model.Domain;
using News.API.Model.DTO;
using News.API.Repositories.Interface;

namespace News.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsletterController : ControllerBase
    {
        private readonly INewsletterRepository newsletterRepository;
        private readonly ICategoryRepository categoryRepository;

        public NewsletterController(INewsletterRepository newsletterRepository,
            ICategoryRepository categoryRepository)
        {
            this.newsletterRepository = newsletterRepository;
            this.categoryRepository = categoryRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewsletter([FromBody] CreateNewsletterRequestDto reqest)
        {
            var newsletter = new Newsletter
            {
                Title = reqest.Title,
                ShortDescription = reqest.ShortDescription,
                Content = reqest.Content,
                UrlHandle = reqest.UrlHandle,
                PublishedDate = reqest.PublishedDate,
                Author = reqest.Author,
                IsVisible = reqest.IsVisible,
                Categories = new List<Category>()
            };

            foreach (var categoryGuid in reqest.Categories)
            {
                var existingCategory = await categoryRepository.GetByIdAsync(categoryGuid);
                if (existingCategory != null)
                {
                    newsletter.Categories.Add(existingCategory);
                }
            }

            newsletter = await newsletterRepository.CreateAsync(newsletter);

            var response = new NewsletterDto
            {
                Id = newsletter.Id,
                Title = newsletter.Title,
                ShortDescription = newsletter.ShortDescription,
                Content = newsletter.Content,
                UrlHandle = newsletter.UrlHandle,
                PublishedDate = newsletter.PublishedDate,
                Author = newsletter.Author,
                IsVisible = newsletter.IsVisible,
                Categories = newsletter.Categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle
                }).ToList()
            };

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNewsletter()
        {
            var newsletters = await newsletterRepository.GetAllAsync();

            var response = new List<NewsletterDto>();
            foreach (var newsletter in newsletters)
            {
                response.Add(new NewsletterDto { 
                    Id = newsletter.Id, 
                    Title = newsletter.Title, 
                    ShortDescription = newsletter.ShortDescription, 
                    Content = newsletter.Content,
                    UrlHandle = newsletter.UrlHandle,
                    PublishedDate = newsletter.PublishedDate,
                    Author = newsletter.Author,
                    IsVisible = newsletter.IsVisible,
                    Categories = newsletter.Categories.Select(x => new CategoryDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        UrlHandle = x.UrlHandle
                    }).ToList()
                });
            }

            return Ok(response);
        }


    }
}
