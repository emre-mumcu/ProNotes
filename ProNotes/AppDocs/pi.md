# 2022.10.26
dotnet add package Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation
dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson
dotnet add package SixLabors.ImageSharp
dotnet add package SixLabors.ImageSharp.Drawing --prerelease
dotnet add package Microsoft.IdentityModel.Tokens
dotnet add package System.IdentityModel.Tokens.Jwt
dotnet add package Markdig


        private static void PopulateSampleData(AppDbContext context, IWebHostEnvironment environment)
        {
            // -------
            // ULKELER
            // -------

            string countries = "Afghanistan,Albania,Algeria,Andorra,Angola,Antigua and Barbuda,Argentina,Armenia,Australia,Austria,Azerbaijan,Bahamas,Bahrain,Bangladesh,Barbados,Belarus,Belgium,Belize,Benin,Bhutan,Bolivia,Bosnia and Herzegovina,Botswana,Brazil,Brunei,Bulgaria,Burkina Faso,Burundi,Côte d'Ivoire,Cabo Verde,Cambodia,Cameroon,Canada,Central African Republic,Chad,Chile,China,Colombia,Comoros,Congo (Congo-Brazzaville),Costa Rica,Croatia,Cuba,Cyprus,Czechia (Czech Republic),Democratic Republic of the Congo,Denmark,Djibouti,Dominica,Dominican Republic,Ecuador,Egypt,El Salvador,Equatorial Guinea,Eritrea,Estonia,Eswatini,Ethiopia,Fiji,Finland,France,Gabon,Gambia,Georgia,Germany,Ghana,Greece,Grenada,Guatemala,Guinea,Guinea-Bissau,Guyana,Haiti,Holy See,Honduras,Hungary,Iceland,India,Indonesia,Iran,Iraq,Ireland,Israel,Italy,Jamaica,Japan,Jordan,Kazakhstan,Kenya,Kiribati,Kuwait,Kyrgyzstan,Laos,Latvia,Lebanon,Lesotho,Liberia,Libya,Liechtenstein,Lithuania,Luxembourg,Madagascar,Malawi,Malaysia,Maldives,Mali,Malta,Marshall Islands,Mauritania,Mauritius,Mexico,Micronesia,Moldova,Monaco,Mongolia,Montenegro,Morocco,Mozambique,Myanmar (formerly Burma),Namibia,Nauru,Nepal,Netherlands,New Zealand,Nicaragua,Niger,Nigeria,North Korea,North Macedonia,Norway,Oman,Pakistan,Palau,Palestine State,Panama,Papua New Guinea,Paraguay,Peru,Philippines,Poland,Portugal,Qatar,Romania,Russia,Rwanda,Saint Kitts and Nevis,Saint Lucia,Saint Vincent and the Grenadines,Samoa,San Marino,Sao Tome and Principe,Saudi Arabia,Senegal,Serbia,Seychelles,Sierra Leone,Singapore,Slovakia,Slovenia,Solomon Islands,Somalia,South Africa,South Korea,South Sudan,Spain,Sri Lanka,Sudan,Suriname,Sweden,Switzerland,Syria,Tajikistan,Tanzania,Thailand,Timor-Leste,Togo,Tonga,Trinidad and Tobago,Tunisia,Turkey,Turkmenistan,Tuvalu,Uganda,Ukraine,United Arab Emirates,United Kingdom,United States of America,Uruguay,Uzbekistan,Vanuatu,Venezuela,Vietnam,Yemen,Zambia,Zimbabwe";

            //countries.Split(',').ToList().ForEach(i => context.Countries.Add(new CountryEntity() { Name = i }));
            //context.SaveChanges();

            // -------------------
            // IL-ILCE_SBB-MAHALLE           
            // -------------------

            /// Step1: Read json files as string data
            string ilData = File.ReadAllText(Path.Combine(environment.WebRootPath, "static", "iller.json"));
            string ilceData = File.ReadAllText(Path.Combine(environment.WebRootPath, "static", "ilceler.json"));
            string sbbData = File.ReadAllText(Path.Combine(environment.WebRootPath, "static", "sbbler.json"));
            string mahalleData = File.ReadAllText(Path.Combine(environment.WebRootPath, "static", "mahalleler.json"));

            /// Step2: Create corresponding anonymous array types for json data
            var ilTemplate = new[] { new { IlKod = "", Il = "" } };
            var ilceTemplate = new[] { new { IlKod = "", Il = "", IlceKod = "", Ilce = "" } };
            var sbbTemplate = new[] { new { IlKod = "", Il = "", IlceKod = "", Ilce = "", SbbKod = "", SemtBucakBelde = "" } };
            var mahalleTemplate = new[] { new { IlKod = "", Il = "", IlceKod = "", Ilce = "", SbbKod = "", SemtBucakBelde = "", MahalleKod = "", Mahalle = "", PostaKod = "" } };

            /// Step3: Read json data (array type NOT object type) into anonymous array types
            var illerFromJson = JsonConvert.DeserializeAnonymousType(ilData, ilTemplate);
            var ilcelerFromJson = JsonConvert.DeserializeAnonymousType(ilceData, ilceTemplate);
            var sbbFromJson = JsonConvert.DeserializeAnonymousType(sbbData, sbbTemplate);
            var mahalleFromJson = JsonConvert.DeserializeAnonymousType(mahalleData, mahalleTemplate);

            /// Step4: Transfer data to database
            //illerFromJson?.ToList().ForEach(i => context.Iller.Add(new IlEntity() { IlKodu = i.IlKod, IlAdi = i.Il }));
            //context.SaveChanges();

            //ilcelerFromJson?.ToList().ForEach(i=> context.Ilceler.Add(new IlceEntity() { IlceKodu = i.IlceKod, IlceAdi = i.Ilce, IlKodu = i.IlKod }));
            //context.SaveChanges();

            //sbbFromJson?.ToList().ForEach(i => context.Sbbler.Add(new SbbEntity() { SbbKodu = i.SbbKod, SemtBucakBeldeAdi = i.SemtBucakBelde, IlceKodu = i.IlceKod }));
            //context.SaveChanges();

            //mahalleFromJson?.ToList().ForEach(i => context.Mahalleler.Add(new MahalleEntity() { MahalleKodu = i.MahalleKod, MahalleAdi = i.Mahalle, SbbKodu = i.SbbKod }));
            //context.SaveChanges();

            // -------
            // DBVIEWS
            // -------

            //DbViewEntity view = new DbViewEntity();
            //view.Location = "/Page1";
            //view.Content = @"@{ Layout =""_LayoutMazer""; }<p>There is no route in the endpoints, registered for this view. The UseDbFileProviderMiddleware middleware will bring this view.</p>@System.DateTime.Now";
            //view.LastModified = DateTime.MinValue;
            //view.LastRequested = DateTime.MinValue;
            //context.DbViews.Add(view);

            //DbViewEntity view2 = new DbViewEntity();
            //view2.Location = "/Page2";
            //view2.Content = @"@{ Layout =""_LayoutMazer""; }<p>There is no route in the endpoints, registered for this view. The UseDbFileProviderMiddleware middleware will bring this view.</p>@System.DateTime.Now";
            //view2.LastModified = DateTime.MinValue;
            //view2.LastRequested = DateTime.MinValue;
            //context.DbViews.Add(view2);

            //DbViewEntity view3 = new DbViewEntity();
            //view3.Location = "/Content/Page1";
            //view3.Content = @"@{ Layout =""_LayoutMazer""; }<p>There is no route in the endpoints, registered for this view. The UseDbFileProviderMiddleware middleware will bring this view.</p>@System.DateTime.Now";
            //view3.LastModified = DateTime.MinValue;
            //view3.LastRequested = DateTime.MinValue;
            //context.DbViews.Add(view3);

            //DbViewEntity view4 = new DbViewEntity();
            //view4.Location = "/Content/Page2";
            //view4.Content = @"@{ Layout =""_LayoutMazer""; }<p>There is no route in the endpoints, registered for this view. The UseDbFileProviderMiddleware middleware will bring this view.</p>@System.DateTime.Now";
            //view4.LastModified = DateTime.MinValue;
            //view4.LastRequested = DateTime.MinValue;
            //context.DbViews.Add(view4);

            context.SaveChanges();
        }