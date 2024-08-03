using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Bookstore.Migrations
{
    /// <inheritdoc />
    public partial class bookstoreinitialseed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Author_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Author_Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Biography = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created_By = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Updated_By = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Author_Id);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Genre_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Genre_Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created_By = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Updated_By = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Genre_Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created_By = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Updated_By = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Author_Id = table.Column<int>(type: "int", nullable: false),
                    Genre_Id = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Publication_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created_By = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Updated_By = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Authors_Author_Id",
                        column: x => x.Author_Id,
                        principalTable: "Authors",
                        principalColumn: "Author_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Books_Genres_Genre_Id",
                        column: x => x.Genre_Id,
                        principalTable: "Genres",
                        principalColumn: "Genre_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Author_Id", "Author_Name", "Biography", "Created_At", "Created_By", "Updated_At", "Updated_By" },
                values: new object[,]
                {
                    { 1, "Nancy Bond", "Nancy Bond is a prolific fantasy writer known for her richly imagined worlds, complex characters, and epic narratives that captivate readers of all ages. Her ability to blend elements of magic and adventure has made her a favorite among fantasy enthusiasts worldwide.", new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2523), "admin", new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2524), "admin" },
                    { 2, "AK Gandhi", "AK Gandhi born in Meerut took retirement from the Indian Air Force in 1995 at a young age and engaged himself as full-time freelance writer and translator.", new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2529), "admin", new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2530), "admin" },
                    { 3, "John Smith", "John Smith is a prolific fantasy writer known for his richly imagined worlds, complex characters, and epic narratives that captivate readers of all ages. His ability to blend elements of magic and adventure has made him a favorite among fantasy enthusiasts worldwide.", new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2532), "admin", new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2532), "admin" },
                    { 4, "Sarah Johnson", "Sarah Johnson writes poignant historical fiction that vividly portrays the past through meticulously researched details and emotionally resonant storytelling. Her novels transport readers to different eras, offering insights into human nature and societal change.,", new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2600), "admin", new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2601), "admin" },
                    { 5, "Michael Andrews", "Michael Andrews explores the intersection of science, technology, and humanity in his thought-provoking science fiction works. Known for his innovative ideas and compelling narratives, Andrews pushes the boundaries of imagination while addressing profound questions about the future.", new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2603), "admin", new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2603), "admin" },
                    { 6, "Emily Chen", "Emily Chens thrillers are known for their gripping plots, complex characters, and unpredictable twists that keep readers on the edge of their seats until the final page. Her ability to create suspenseful narratives filled with psychological depth has garnered critical acclaim in the thriller genre.", new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2605), "admin", new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2605), "admin" },
                    { 7, "David Rodriguez", "David Rodriguez's novels delve into philosophical themes with lyrical prose and introspective storytelling that challenges readers to contemplate life's complexities. His explorations of human emotions and existential questions resonate deeply, making his literary fiction a compelling read for those seeking profound insights.", new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2607), "admin", new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2607), "admin" },
                    { 8, "Alice Thompson", "Alice Thompson writes enchanting tales of magic and adventure that transport readers to fantastical realms filled with wonder and danger. Her ability to weave intricate plots and create vivid settings has earned her a dedicated following among fans of fantasy literature", new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2609), "admin", new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2609), "admin" },
                    { 9, "Mark Davis", "Mark Davis is a master of suspense and thriller, crafting stories that blend heart-pounding action with intricate plot twists. His ability to create tension and suspenseful atmospheres has made him a favorite among readers who enjoy thrillers that keep them guessing until the very end.", new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2611), "admin", new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2612), "admin" },
                    { 10, "Sophie White", "Sophie White's historical novels bring to life forgotten tales from the past, illuminating lesser-known events and characters with compassion and historical accuracy. Her meticulous research and evocative prose make her novels a captivating journey into different epochs", new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2613), "admin", new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2614), "admin" },
                    { 11, "Thomas Brown", "Thomas Brown's science fiction explores complex themes of identity, technology, and the nature of reality with a blend of intellectual rigor and imaginative storytelling. His speculative visions of future societies and technological advancements provoke thought and reflection", new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2615), "admin", new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2615), "admin" },
                    { 12, "Emma Wilson", "Emma Wilson's stories are filled with hope and resilience, portraying characters who face adversity with courage and grace. Her uplifting narratives and heartfelt storytelling resonate with readers seeking stories of human strength and the power of optimism.", new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2617), "admin", new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2617), "admin" },
                    { 13, "Matthew Lee", "Matthew Lee's horror novels keep readers on the edge of their seats with chilling suspense, supernatural phenomena, and spine-tingling plot twists. His atmospheric settings and vivid descriptions create an immersive reading experience for fans of horror and suspense fiction.", new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2619), "admin", new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2619), "admin" },
                    { 14, "Olivia Green", "Olivia Green writes gripping mysteries set in exotic locations, blending suspenseful plots with rich cultural details and compelling characters. Her ability to create atmospheric settings and intricate puzzles has earned her a reputation as a master of the mystery genre.", new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2621), "admin", new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2621), "admin" },
                    { 15, "Daniel Moore", "Daniel Moore's novels blend adventure with profound explorations of human nature and the mysteries of existence. His gripping narratives and multifaceted characters take readers on thrilling journeys that delve into the depths of the human psyche.", new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2622), "admin", new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2623), "admin" },
                    { 16, "Lucy Hall", "Lucy Hall's fantasy worlds are intricate and imaginative, filled with magic, mythical creatures, and quests that challenge characters to discover their true strengths. Her ability to create enchanting realms and compelling characters makes her a beloved author in the fantasy genre.", new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2625), "admin", new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2625), "admin" },
                    { 17, "James Taylor", "James Taylor's historical fiction transports readers to pivotal moments in history, weaving together personal stories with larger historical events to create a vivid tapestry of the past. His meticulous attention to historical detail and evocative storytelling bring history to life for readers.", new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2627), "admin", new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2627), "admin" },
                    { 18, "Sophia Clark", "Sophia Clark explores the mysteries of quantum physics and the implications of scientific discovery on humanity's understanding of reality.Her speculative fiction challenges readers to ponder the philosophical and ethical implications of technological advancements.", new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2629), "admin", new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2629), "admin" }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Genre_Id", "Created_At", "Created_By", "Genre_Name", "Updated_At", "Updated_By" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2678), "admin", "Thriller", new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2680), "admin" },
                    { 2, new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2684), "admin", "Fantasy", new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2685), "admin" },
                    { 3, new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2686), "admin", "History", new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2687), "admin" },
                    { 4, new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2688), "admin", "Horror", new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2688), "admin" },
                    { 5, new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2690), "admin", "Science Fiction", new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2690), "admin" },
                    { 6, new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2692), "admin", "Biography", new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2692), "admin" },
                    { 7, new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2694), "admin", "Self-Help", new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2694), "admin" },
                    { 8, new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2695), "admin", "Spirituality", new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2696), "admin" },
                    { 9, new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2697), "admin", "Politics", new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2698), "admin" },
                    { 10, new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2699), "admin", "Law", new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2700), "admin" },
                    { 11, new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2701), "admin", "Literacy", new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2701), "admin" },
                    { 12, new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2703), "admin", "Travel and Tourism", new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2703), "admin" },
                    { 13, new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2704), "admin", "Sports", new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2705), "admin" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Created_At", "Created_By", "Email", "FirstName", "LastName", "Password", "Updated_At", "Updated_By", "Username" },
                values: new object[] { 1, new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2755), "admin", "Pavikannan@gmail.com", "Pavithra", "G", "Pass@123", new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2756), "admin", "admin" });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author_Id", "Created_At", "Created_By", "Description", "Genre_Id", "Price", "Publication_Date", "Title", "Updated_At", "Updated_By" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2217), "admin", "xyz", 2, 649.0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(1888), "A Little Princess", new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2225), "admin" },
                    { 2, 2, new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2232), "admin", "xyz", 6, 150.0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(1992), "Ratan Tata - A Complete Biography", new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2233), "admin" },
                    { 3, 3, new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2235), "admin", "xyz", 2, 750.0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(2003), "The Hidden Realm", new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2236), "admin" },
                    { 4, 4, new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2238), "admin", "xyz", 3, 1750.0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(1983), "Echoes of Yesterday", new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2239), "admin" },
                    { 5, 5, new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2241), "admin", "xyz", 5, 1750.0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(2012), "Quantum Leap", new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2241), "admin" },
                    { 6, 6, new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2243), "admin", "xyz", 1, 1750.0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(1997), "Shadows in the Mist", new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2243), "admin" },
                    { 7, 7, new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2245), "admin", "xyz", 2, 1750.0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(2009), "Beyond the Horizon", new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2246), "admin" },
                    { 8, 8, new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2248), "admin", "xyz", 2, 1750.0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(2017), "Whispers in the Wind", new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2249), "admin" },
                    { 9, 9, new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2251), "admin", "xyz", 1, 1750.0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(1999), "The Last Stand", new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2252), "admin" },
                    { 10, 10, new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2255), "admin", "xyz", 3, 1750.0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(1982), "The Forgotten Key", new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2255), "admin" },
                    { 11, 11, new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2257), "admin", "xyz", 5, 1750.0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(2015), "Infinite Worlds", new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2258), "admin" },
                    { 12, 12, new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2260), "admin", "xyz", 11, 1750.0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(1997), "A Glimmer of Hope", new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2260), "admin" },
                    { 13, 13, new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2262), "admin", "xyz", 4, 1750.0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(1985), "Darkness Falls", new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2262), "admin" },
                    { 14, 14, new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2264), "admin", "xyz", 1, 1750.0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(2001), "The Golden Hour", new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2265), "admin" },
                    { 15, 15, new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2267), "admin", "xyz", 2, 1750.0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(2003), "Into the Abyss", new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2267), "admin" },
                    { 16, 16, new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2269), "admin", "xyz", 2, 1750.0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(2008), "Through the Looking Glass", new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2270), "admin" },
                    { 17, 17, new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2272), "admin", "xyz", 3, 1750.0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(1999), "The Sands of Time", new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2272), "admin" },
                    { 18, 18, new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2274), "admin", "xyz", 5, 1750.0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(1998), "The Quantum Paradox", new DateTime(2024, 8, 3, 14, 1, 31, 400, DateTimeKind.Utc).AddTicks(2275), "admin" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Authors_Author_Name_Biography",
                table: "Authors",
                columns: new[] { "Author_Name", "Biography" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_Author_Id",
                table: "Books",
                column: "Author_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Books_Genre_Id",
                table: "Books",
                column: "Genre_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Books_Title_Publication_Date",
                table: "Books",
                columns: new[] { "Title", "Publication_Date" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Genres_Genre_Name",
                table: "Genres",
                column: "Genre_Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Genres");
        }
    }
}
