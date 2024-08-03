using Bookstore.Entities;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Bookstore.DbContexts
{
    public class BookstoreContext : DbContext
    {
        public DbSet<Books> Books { get; set; }
        public DbSet<Authors> Authors { get; set; }
        public DbSet<Genres> Genres { get; set; }
        public DbSet <Users> Users { get; set; }

        public BookstoreContext(DbContextOptions<BookstoreContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Books>().HasData(
                new Books
                {
                    Id = 1,
                    Title = "A Little Princess",
                    Price = 649.00,
                    Publication_Date = new DateTime(1905,08, 09),
                    Created_At = DateTime.UtcNow,
                    Updated_At = DateTime.UtcNow,
                    Created_By = "admin",
                    Updated_By = "admin",
                    Author_Id = 1,
                    Genre_Id = 2,
                    Description = "xyz"
                },
                new Books
                {
                    Id = 2,
                    Title = "Ratan Tata - A Complete Biography",
                    Price = 150.00,
                    Publication_Date = new DateTime(2021 , 11 , 18),
                    Created_At = DateTime.UtcNow,
                    Updated_At = DateTime.UtcNow,
                    Author_Id = 2,
                    Genre_Id = 6,
                    Created_By = "admin",
                    Updated_By = "admin",
                    Description = "xyz"
                },
                new Books
                {
                    Id = 3,
                    Title = "The Hidden Realm",
                    Price = 750,
                    Publication_Date = new DateTime(2023 , 05 , 15),
                    Created_At = DateTime.UtcNow,
                    Updated_At = DateTime.UtcNow,
                    Author_Id = 3,
                    Genre_Id = 2,
                    Created_By = "admin",
                    Updated_By = "admin",
                    Description = "xyz"
                },
                new Books
                {
                    Id = 4,
                    Title = "Echoes of Yesterday",
                    Price = 1750,
                    Publication_Date = new DateTime(2022 , 11 , 28),
                    Created_At = DateTime.UtcNow,
                    Updated_At = DateTime.UtcNow,
                    Author_Id = 4,
                    Genre_Id = 3,
                    Created_By = "admin",
                    Updated_By = "admin",
                    Description = "xyz"
                },
                new Books
                {
                    Id = 5,
                    Title = "Quantum Leap",
                    Price = 1750.00,
                    Publication_Date = new DateTime(2024 , 02 , 10),
                    Created_At = DateTime.UtcNow,
                    Updated_At = DateTime.UtcNow,
                    Author_Id = 5,
                    Genre_Id = 5,
                    Created_By = "admin",
                    Updated_By = "admin",
                    Description = "xyz"
                },
                new Books
                {
                    Id = 6,
                    Title = "Shadows in the Mist",
                    Price = 1750.00,
                    Publication_Date = new DateTime(2023 , 07 , 19),
                    Created_At = DateTime.UtcNow,
                    Updated_At = DateTime.UtcNow,
                    Author_Id = 6,
                    Genre_Id = 1,
                    Created_By = "admin",
                    Updated_By = "admin",
                    Description = "xyz"
                },
                new Books
                {
                    Id = 7,
                    Title = "Beyond the Horizon",
                    Price = 1750.00,
                    Publication_Date = new DateTime(2023 , 09 , 05),
                    Created_At = DateTime.UtcNow,
                    Updated_At = DateTime.UtcNow,
                    Author_Id = 7,
                    Genre_Id = 2,
                    Created_By = "admin",
                    Updated_By = "admin",
                    Description = "xyz"
                },
                new Books
                {
                    Id = 8,
                    Title = "Whispers in the Wind",
                    Price = 1750.00,
                    Publication_Date = new DateTime(2023 , 04 , 02),
                    Created_At = DateTime.UtcNow,
                    Updated_At = DateTime.UtcNow,
                    Author_Id = 8,
                    Genre_Id = 2,
                    Created_By = "admin",
                    Updated_By = "admin",
                    Description = "xyz"
                },
                new Books
                {
                    Id = 9,
                    Title = "The Last Stand",
                    Price = 1750.00,
                    Publication_Date = new DateTime(2022 , 08 , 15),
                    Created_At = DateTime.UtcNow,
                    Updated_At = DateTime.UtcNow,
                    Author_Id = 9,
                    Genre_Id = 1,
                    Created_By = "admin",
                    Updated_By = "admin",
                    Description = "xyz"
                },
                new Books
                {
                    Id = 10,
                    Title = "The Forgotten Key",
                    Price = 1750.00,
                    Publication_Date = new DateTime(2023 , 11 , 30),
                    Created_At = DateTime.UtcNow,
                    Updated_At = DateTime.UtcNow,
                    Author_Id = 10,
                    Genre_Id = 3,
                    Created_By = "admin",
                    Updated_By = "admin",
                    Description = "xyz"
                },
                new Books
                {
                    Id = 11,
                    Title = "Infinite Worlds",
                    Price = 1750.00,
                    Publication_Date = new DateTime(2024 , 01 , 08),
                    Created_At = DateTime.UtcNow,
                    Updated_At = DateTime.UtcNow,
                    Author_Id = 11,
                    Genre_Id = 5,
                    Created_By = "admin",
                    Updated_By = "admin",
                    Description = "xyz"
                },
                new Books
                {
                    Id = 12,
                    Title = "A Glimmer of Hope",
                    Price = 1750.00,
                    Publication_Date = new DateTime(2023 , 06 , 20),
                    Created_At = DateTime.UtcNow,
                    Updated_At = DateTime.UtcNow,
                    Author_Id = 12,
                    Genre_Id = 11,
                    Created_By = "admin",
                    Updated_By = "admin",
                    Description = "xyz"
                },
                new Books
                {
                    Id = 13,
                    Title = "Darkness Falls",
                    Price = 1750.00,
                    Publication_Date = new DateTime(2022 , 12 , 25),
                    Created_At = DateTime.UtcNow,
                    Updated_At = DateTime.UtcNow,
                    Author_Id = 13,
                    Genre_Id = 4,
                    Created_By = "admin",
                    Updated_By = "admin",
                    Description = "xyz"
                },
                new Books
                {
                    Id = 14,
                    Title = "The Golden Hour",
                    Price = 1750.00,
                    Publication_Date = new DateTime(2023 , 10 , 12),
                    Created_At = DateTime.UtcNow,
                    Updated_At = DateTime.UtcNow,
                    Author_Id = 14,
                    Genre_Id = 1,
                    Created_By = "admin",
                    Updated_By = "admin",
                    Description = "xyz"
                },
                new Books
                {
                    Id=15,
                    Title = "Into the Abyss",
                    Price = 1750.00,
                    Publication_Date = new DateTime(2024 , 03 , 18),
                    Created_At = DateTime.UtcNow,
                    Updated_At = DateTime.UtcNow,
                    Author_Id = 15,
                    Genre_Id = 2,
                    Created_By = "admin",
                    Updated_By = "admin",
                    Description = "xyz"
                },
                new Books
                {
                    Id = 16,
                    Title = "Through the Looking Glass",
                    Price = 1750.00,
                    Publication_Date = new DateTime(2023 , 08 , 07),
                    Created_At = DateTime.UtcNow,
                    Updated_At = DateTime.UtcNow,
                    Author_Id = 16,
                    Genre_Id = 2,
                    Created_By = "admin",
                    Updated_By = "admin",
                    Description = "xyz"
                },
                new Books
                {
                    Id = 17,
                    Title = "The Sands of Time",
                    Price = 1750.00,
                    Publication_Date = new DateTime(2022 , 09 , 14),
                    Created_At = DateTime.UtcNow,
                    Updated_At = DateTime.UtcNow,
                    Author_Id = 17,
                    Genre_Id = 3,
                    Created_By = "admin",
                    Updated_By = "admin",
                    Description = "xyz"
                },
                new Books
                {
                    Id = 18,
                    Title = "The Quantum Paradox",
                    Price = 1750.00,
                    Publication_Date = new DateTime(2024 , 04 , 22),
                    Created_At = DateTime.UtcNow,
                    Updated_At = DateTime.UtcNow,
                    Author_Id = 18,
                    Genre_Id = 5,
                    Created_By = "admin",
                    Updated_By = "admin",
                    Description = "xyz"
                }
            );

            modelBuilder.Entity<Authors>().HasData(
                new Authors
                {
                    Author_Id =1,
                    Author_Name = "Nancy Bond",
                    Biography = "Nancy Bond is a prolific fantasy writer known for her richly imagined worlds, complex characters, and epic narratives that captivate readers of all ages. Her ability to blend elements of magic and adventure has made her a favorite among fantasy enthusiasts worldwide.",
                    Created_At = DateTime.UtcNow,
                    Updated_At = DateTime.UtcNow,
                    Created_By = "admin",
                    Updated_By = "admin",
                },
                new Authors
                {
                    Author_Id =2,
                    Author_Name = "AK Gandhi",
                    Biography = "AK Gandhi born in Meerut took retirement from the Indian Air Force in 1995 at a young age and engaged himself as full-time freelance writer and translator.",
                    Created_At = DateTime.UtcNow,
                    Updated_At = DateTime.UtcNow,
                    Created_By = "admin",
                    Updated_By = "admin",
                },
                new Authors
                {
                    Author_Id =3,
                    Author_Name = "John Smith",
                    Biography = "John Smith is a prolific fantasy writer known for his richly imagined worlds, complex characters, and epic narratives that captivate readers of all ages. His ability to blend elements of magic and adventure has made him a favorite among fantasy enthusiasts worldwide.",
                    Created_At = DateTime.UtcNow,
                    Updated_At = DateTime.UtcNow,
                    Created_By = "admin",
                    Updated_By = "admin",
                },
                new Authors
                {
                    Author_Id =4,
                    Author_Name = "Sarah Johnson",
                    Biography = "Sarah Johnson writes poignant historical fiction that vividly portrays the past through meticulously researched details and emotionally resonant storytelling. Her novels transport readers to different eras, offering insights into human nature and societal change.,",
                    Created_At = DateTime.UtcNow,
                    Updated_At = DateTime.UtcNow,
                    Created_By = "admin",
                    Updated_By = "admin",
                },
                new Authors
                {
                    Author_Id =5,
                    Author_Name = "Michael Andrews",
                    Biography = "Michael Andrews explores the intersection of science, technology, and humanity in his thought-provoking science fiction works. Known for his innovative ideas and compelling narratives, Andrews pushes the boundaries of imagination while addressing profound questions about the future.",
                    Created_At = DateTime.UtcNow,
                    Updated_At = DateTime.UtcNow,
                    Created_By = "admin",
                    Updated_By = "admin",
                },
                new Authors
                {
                    Author_Id =6,
                    Author_Name = "Emily Chen",
                    Biography = "Emily Chens thrillers are known for their gripping plots, complex characters, and unpredictable twists that keep readers on the edge of their seats until the final page. Her ability to create suspenseful narratives filled with psychological depth has garnered critical acclaim in the thriller genre.",
                    Created_At = DateTime.UtcNow,
                    Updated_At = DateTime.UtcNow,
                    Created_By = "admin",
                    Updated_By = "admin",
                },
                new Authors
                {
                    Author_Id =7,
                    Author_Name = "David Rodriguez",
                    Biography = "David Rodriguez's novels delve into philosophical themes with lyrical prose and introspective storytelling that challenges readers to contemplate life's complexities. His explorations of human emotions and existential questions resonate deeply, making his literary fiction a compelling read for those seeking profound insights.",
                    Created_At = DateTime.UtcNow,
                    Updated_At = DateTime.UtcNow,
                    Created_By = "admin",
                    Updated_By = "admin",
                },
                new Authors
                {
                    Author_Id = 8,
                    Author_Name = "Alice Thompson",
                    Biography = "Alice Thompson writes enchanting tales of magic and adventure that transport readers to fantastical realms filled with wonder and danger. Her ability to weave intricate plots and create vivid settings has earned her a dedicated following among fans of fantasy literature",
                    Created_At = DateTime.UtcNow,
                    Updated_At = DateTime.UtcNow,
                    Created_By = "admin",
                    Updated_By = "admin",
                },
                new Authors
                {
                    Author_Id =9,
                    Author_Name = "Mark Davis",
                    Biography = "Mark Davis is a master of suspense and thriller, crafting stories that blend heart-pounding action with intricate plot twists. His ability to create tension and suspenseful atmospheres has made him a favorite among readers who enjoy thrillers that keep them guessing until the very end.",
                    Created_At = DateTime.UtcNow,
                    Updated_At = DateTime.UtcNow,
                    Created_By = "admin",
                    Updated_By = "admin",
                },
                new Authors
                {
                    Author_Id =10,
                    Author_Name = "Sophie White",
                    Biography = "Sophie White's historical novels bring to life forgotten tales from the past, illuminating lesser-known events and characters with compassion and historical accuracy. Her meticulous research and evocative prose make her novels a captivating journey into different epochs",
                    Created_At = DateTime.UtcNow,
                    Updated_At = DateTime.UtcNow,
                    Created_By = "admin",
                    Updated_By = "admin",
                },
                new Authors
                {
                    Author_Id =11,
                    Author_Name = "Thomas Brown",
                    Biography = "Thomas Brown's science fiction explores complex themes of identity, technology, and the nature of reality with a blend of intellectual rigor and imaginative storytelling. His speculative visions of future societies and technological advancements provoke thought and reflection",
                    Created_At = DateTime.UtcNow,
                    Updated_At = DateTime.UtcNow,
                    Created_By = "admin",
                    Updated_By = "admin",
                },
                new Authors
                {
                    Author_Id =12,
                    Author_Name = "Emma Wilson",
                    Biography = "Emma Wilson's stories are filled with hope and resilience, portraying characters who face adversity with courage and grace. Her uplifting narratives and heartfelt storytelling resonate with readers seeking stories of human strength and the power of optimism.",
                    Created_At = DateTime.UtcNow,
                    Updated_At = DateTime.UtcNow,
                    Created_By = "admin",
                    Updated_By = "admin",
                },
                new Authors
                {
                    Author_Id =13,
                    Author_Name = "Matthew Lee",
                    Biography = "Matthew Lee's horror novels keep readers on the edge of their seats with chilling suspense, supernatural phenomena, and spine-tingling plot twists. His atmospheric settings and vivid descriptions create an immersive reading experience for fans of horror and suspense fiction.",
                    Created_At = DateTime.UtcNow,
                    Updated_At = DateTime.UtcNow,
                    Created_By = "admin",
                    Updated_By = "admin",
                },
                new Authors
                {
                    Author_Id =14,
                    Author_Name = "Olivia Green",
                    Biography = "Olivia Green writes gripping mysteries set in exotic locations, blending suspenseful plots with rich cultural details and compelling characters. Her ability to create atmospheric settings and intricate puzzles has earned her a reputation as a master of the mystery genre.",
                    Created_At = DateTime.UtcNow,
                    Updated_At = DateTime.UtcNow,
                    Created_By = "admin",
                    Updated_By = "admin",
                },
                new Authors
                {
                    Author_Id =15,
                    Author_Name = "Daniel Moore",
                    Biography = "Daniel Moore's novels blend adventure with profound explorations of human nature and the mysteries of existence. His gripping narratives and multifaceted characters take readers on thrilling journeys that delve into the depths of the human psyche.",
                    Created_At = DateTime.UtcNow,
                    Updated_At = DateTime.UtcNow,
                    Created_By = "admin",
                    Updated_By = "admin",
                },
                new Authors
                {
                    Author_Id =16,
                    Author_Name = "Lucy Hall",
                    Biography = "Lucy Hall's fantasy worlds are intricate and imaginative, filled with magic, mythical creatures, and quests that challenge characters to discover their true strengths. Her ability to create enchanting realms and compelling characters makes her a beloved author in the fantasy genre.",
                    Created_At = DateTime.UtcNow,
                    Updated_At = DateTime.UtcNow,
                    Created_By = "admin",
                    Updated_By = "admin",
                },
                new Authors
                {
                    Author_Id =17,
                    Author_Name = "James Taylor",
                    Biography = "James Taylor's historical fiction transports readers to pivotal moments in history, weaving together personal stories with larger historical events to create a vivid tapestry of the past. His meticulous attention to historical detail and evocative storytelling bring history to life for readers.",
                    Created_At = DateTime.UtcNow,
                    Updated_At = DateTime.UtcNow,
                    Created_By = "admin",
                    Updated_By = "admin",
                },
                new Authors
                {
                    Author_Id =18,
                    Author_Name = "Sophia Clark",
                    Biography = "Sophia Clark explores the mysteries of quantum physics and the implications of scientific discovery on humanity's understanding of reality.Her speculative fiction challenges readers to ponder the philosophical and ethical implications of technological advancements.",
                    Created_At = DateTime.UtcNow,
                    Updated_At = DateTime.UtcNow,
                    Created_By = "admin",
                    Updated_By = "admin",
                }
                );

            modelBuilder.Entity<Genres>().HasData(
            new Genres
            {
                Genre_Id = 1,
                Genre_Name = "Thriller",
                Created_At = DateTime.UtcNow,
                Updated_At = DateTime.UtcNow,
                Created_By = "admin",
                Updated_By = "admin"
            },
            new Genres
            {
                Genre_Id =2,
                Genre_Name = "Fantasy",
                Created_At = DateTime.UtcNow,
                Updated_At = DateTime.UtcNow,
                Created_By = "admin",
                Updated_By = "admin"
            },
            new Genres
            {
                Genre_Id =3,
                Genre_Name = "History",
                Created_At = DateTime.UtcNow,
                Updated_At = DateTime.UtcNow,
                Created_By = "admin",
                Updated_By = "admin"
            },
            new Genres
            {
                Genre_Id =4,
                Genre_Name = "Horror",
                Created_At = DateTime.UtcNow,
                Updated_At = DateTime.UtcNow,
                Created_By = "admin",
                Updated_By = "admin"
            },
            new Genres
            {
                Genre_Id =5,
                Genre_Name = "Science Fiction",
                Created_At = DateTime.UtcNow,
                Updated_At = DateTime.UtcNow,
                Created_By = "admin",
                Updated_By = "admin"
            },
            new Genres
            {
                Genre_Id =6,
                Genre_Name = "Biography",
                Created_At = DateTime.UtcNow,
                Updated_At = DateTime.UtcNow,
                Created_By = "admin",
                Updated_By = "admin"
            },
            new Genres
            {
                Genre_Id =7,
                Genre_Name = "Self-Help",
                Created_At = DateTime.UtcNow,
                Updated_At = DateTime.UtcNow,
                Created_By = "admin",
                Updated_By = "admin"
            },
            new Genres
            {
                Genre_Id =8,
                Genre_Name = "Spirituality",
                Created_At = DateTime.UtcNow,
                Updated_At = DateTime.UtcNow,
                Created_By = "admin",
                Updated_By = "admin"
            },
            new Genres
            {
                Genre_Id =9,
                Genre_Name = "Politics",
                Created_At = DateTime.UtcNow,
                Updated_At = DateTime.UtcNow,
                Created_By = "admin",
                Updated_By = "admin"
            },
            new Genres
            {
                Genre_Id =10,
                Genre_Name = "Law",
                Created_At = DateTime.UtcNow,
                Updated_At = DateTime.UtcNow,
                Created_By = "admin",
                Updated_By = "admin"
            },
            new Genres
            {
                Genre_Id =11,
                Genre_Name = "Literacy",
                Created_At = DateTime.UtcNow,
                Updated_At = DateTime.UtcNow,
                Created_By = "admin",
                Updated_By = "admin"
            },
            new Genres
            {
                Genre_Id =12,
                Genre_Name = "Travel and Tourism",
                Created_At = DateTime.UtcNow,
                Updated_At = DateTime.UtcNow,
                Created_By = "admin",
                Updated_By = "admin"
            },
            new Genres
            {
                Genre_Id =13,
                Genre_Name = "Sports",
                Created_At = DateTime.UtcNow,
                Updated_At = DateTime.UtcNow,
                Created_By = "admin",
                Updated_By = "admin"
            }
            );

            modelBuilder.Entity<Users>().HasData(
                new Users
                {
                    Id = 1,
                    FirstName = "Pavithra",
                    LastName = "G",
                    Email = "Pavikannan@gmail.com",
                    Username = "admin",
                    Password = "Pass@123",
                    Created_At = DateTime.UtcNow,
                    Updated_At = DateTime.UtcNow,
                    Created_By = "admin",
                    Updated_By = "admin"
                }
            );

            modelBuilder.Entity<Genres>()
            .HasIndex(t => t.Genre_Name)
            .IsUnique();

            modelBuilder.Entity<Authors>()
            .HasIndex(t => new { t.Author_Name, t.Biography })
            .IsUnique();

            modelBuilder.Entity<Books>()
            .HasIndex(t => new { t.Title, t.Publication_Date })
            .IsUnique();

            modelBuilder.Entity<Users>()
            .HasIndex(t => new { t.Username })
            .IsUnique();

            base.OnModelCreating(modelBuilder);


        }
    }
}
