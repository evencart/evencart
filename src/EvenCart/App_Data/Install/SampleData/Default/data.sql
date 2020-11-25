EXEC sp_MSforeachtable "ALTER TABLE ? NOCHECK CONSTRAINT all"

SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([Name], [Summary], [Description], [IsShippable], [IsDownloadable], [IsFeatured], [IsVisibleIndividually], [TrackInventory], [CanOrderWhenOutOfStock], [ComparePrice], [Price], [Sku], [Gtin], [Mpn], [MinimumPurchaseQuantity], [MaximumPurchaseQuantity], [CreatedOn], [UpdatedOn], [ParentProductId], [DisplayOrder], [ChargeTaxes], [Published], [Deleted], [ManufacturerId], [TaxId], [HasVariants], [ReviewsDisabled], [PopularityIndex], [PackageWeight], [PackageWeightUnit], [PackageWidth], [PackageWidthUnit], [PackageHeight], [PackageHeightUnit], [PackageLength], [PackageLengthUnit], [AdditionalShippingCharge], [IndividuallyShipped], [AllowReturns], [DaysForReturn], [Id], [ProductType], [ProductSaleType], [SubscriptionCycle], [CycleCount], [TrialDays], [RequireLoginToPurchase], [RequireLoginToViewPrice], [RestrictedToRoles], [DisableSale]) VALUES (N'HP 15 Core i3 7th gen 15.6-inch Laptop', N'<p><span style="font-size: 14.4px;">Designed for long-lasting performance, this stylishly designed HP laptop has a long-lasting battery that keeps you connected, entertained, and productive all day. Speed through tasks, or sit back and socialize - with the latest processors and a rich Full HD display. Do it all, all day.</span></p>', N'<p><span style="font-size: 14.4px;">Processor: 7th Gen Intel Core i3-7100U processor, 2.4GHz base processor speed, 2 cores, 3MB cache</span></p><p><span style="font-size: 14.4px;">Operating System: Pre-loaded Windows 10 Home with lifetime validity</span></p><p><span style="font-size: 14.4px;">Display: 15.6-inch Full HD (1920x1080) WLED display, Display Features: Diagonal FHD SVA Anti-Glare WLED-backlit Display</span></p><p></p><ul><li><span style="font-size: 14.4px;">Memory &amp; Storage: 4GB DDR4 RAM Intel HD Graphics 620 | Storage: 1TB HDD, HDD Speed(RPM): 5400 RPM</span></li><li>Design &amp; battery: Multi-touch gesture support | Thin and light design | Laptop weight: 2.2 kg | Average battery life = 7 hours, HP Fast Charge battery, Battery: 3 Cell, Li-Ion, Power Supply: 41 W AC Adapter W</li><li>Warranty: This genuine HP laptop comes with a 1-year domestic warranty from HP covering manufacturing defects and not covering physical damage. For more details, see Warranty section below</li><li>Preinstalled Software: Windows 10 Home | In the Box: Laptop with included battery and charger Ports &amp; CD drive: 1 HDMI, 2 USB 3.0, 1 USB 2.0, 1 Audio-output | With CD drive Other features: Anti Glare Display</li></ul><p></p>', 1, 0, 0, 0, 0, 0, CAST(650.00000 AS Numeric(18, 5)), CAST(449.00000 AS Numeric(18, 5)), N'15-DA0326TU', NULL, NULL, 1, 1, CAST(N'2019-07-02T12:06:46.667' AS DateTime), CAST(N'2019-11-19T11:53:52.597' AS DateTime), NULL, 0, 0, 1, 0, 1, NULL, 1, 0, 0, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 0, 0, 0, 2, 0, 0, 0, 0, NULL, 0, 0, 0, 0)

INSERT [dbo].[Product] ([Name], [Summary], [Description], [IsShippable], [IsDownloadable], [IsFeatured], [IsVisibleIndividually], [TrackInventory], [CanOrderWhenOutOfStock], [ComparePrice], [Price], [Sku], [Gtin], [Mpn], [MinimumPurchaseQuantity], [MaximumPurchaseQuantity], [CreatedOn], [UpdatedOn], [ParentProductId], [DisplayOrder], [ChargeTaxes], [Published], [Deleted], [ManufacturerId], [TaxId], [HasVariants], [ReviewsDisabled], [PopularityIndex], [PackageWeight], [PackageWeightUnit], [PackageWidth], [PackageWidthUnit], [PackageHeight], [PackageHeightUnit], [PackageLength], [PackageLengthUnit], [AdditionalShippingCharge], [IndividuallyShipped], [AllowReturns], [DaysForReturn], [Id], [ProductType], [ProductSaleType], [SubscriptionCycle], [CycleCount], [TrialDays], [RequireLoginToPurchase], [RequireLoginToViewPrice], [RestrictedToRoles], [DisableSale]) VALUES (N'Microsoft Surface Pro 6 1796 2019 12.3-inch Laptop', N'<p><span style="font-size: 14.4px;">Unplug. Pack light. Get productive your way, all day, with new Surface Pro 6 – now faster than ever with the latest 8th Generation Intel Core processor. Wherever you are, new Surface Pro 6 makes it easy to work and play virtually anywhere, with laptop-to-tablet versatility that adapts to you.</span></p>', N'<p style="letter-spacing: 0.32px;"><span style="font-weight: bolder;">Your Laptop, your Way</span></p><p style="letter-spacing: 0.32px;">Wherever you are, new Surface Pro 6 makes it easy to work and play virtually anywhere, with laptop-to-tablet versatility that adapts to you.</p><span style="letter-spacing: 0.32px;"><br></span><p style="letter-spacing: 0.32px;"><span style="font-weight: bolder;">More Power for your Ideas</span></p><p style="letter-spacing: 0.32px;">Professional. Student. Creator. Whatever you do, let next-generation Surface Pro 6, featuring the latest 8th Generation Intel Core processor and all-day battery life, help you take your ideas to the next level.</p><span style="letter-spacing: 0.32px;"><br></span><p style="letter-spacing: 0.32px;"><span style="font-weight: bolder;">Mix, Match, Make it your own Personalize</span></p><p style="letter-spacing: 0.32px;">Personalize Surface Pro 6 to suit your style with a choice of Surface Accessories. Make it a full laptop with our Signature Type Cover*, Surface Pen*, and Surface Arc Mouse*.</p><span style="letter-spacing: 0.32px;"><br></span><p style="letter-spacing: 0.32px;"><span style="font-weight: bolder;">Do More with the Windows you Know</span></p><p style="letter-spacing: 0.32px;">With Windows 10 Home, enjoy familiar features like password-free Windows Hello sign-in and Cortana* intelligent assistant - and create your best work with Office 365* on Windows.</p>', 1, 0, 0, 0, 1, 0, NULL, CAST(1566.00000 AS Numeric(18, 5)), N'Pro1796', NULL, NULL, 1, 0, CAST(N'2019-07-03T11:45:03.197' AS DateTime), CAST(N'2019-07-03T12:32:55.583' AS DateTime), NULL, 0, 0, 1, 0, 2, NULL, 1, 0, 0, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 1, 0, 0, 3, 0, 0, 0, 0, NULL, 0, 0, 0, 0)

INSERT [dbo].[Product] ([Name], [Summary], [Description], [IsShippable], [IsDownloadable], [IsFeatured], [IsVisibleIndividually], [TrackInventory], [CanOrderWhenOutOfStock], [ComparePrice], [Price], [Sku], [Gtin], [Mpn], [MinimumPurchaseQuantity], [MaximumPurchaseQuantity], [CreatedOn], [UpdatedOn], [ParentProductId], [DisplayOrder], [ChargeTaxes], [Published], [Deleted], [ManufacturerId], [TaxId], [HasVariants], [ReviewsDisabled], [PopularityIndex], [PackageWeight], [PackageWeightUnit], [PackageWidth], [PackageWidthUnit], [PackageHeight], [PackageHeightUnit], [PackageLength], [PackageLengthUnit], [AdditionalShippingCharge], [IndividuallyShipped], [AllowReturns], [DaysForReturn], [Id], [ProductType], [ProductSaleType], [SubscriptionCycle], [CycleCount], [TrialDays], [RequireLoginToPurchase], [RequireLoginToViewPrice], [RestrictedToRoles], [DisableSale]) VALUES (N'Apple iPhone X', N'<p><span style="font-size: 14.4px;">Meet the iPhone X - the device that''s so smart that it responds to a tap, your voice, and even a glance. Elegantly designed with a large 14.73 cm (5.8) Super Retina screen and a durable front-and-back glass, this smartphone is designed to impress. What''s more, you can charge this iPhone wirelessly.</span></p>', N'<p><span style="font-size: 14.4px;"><strong>14.73 cm Super Retina Screen</strong></span></p><p><span style="font-size: 14.4px;">Movies or games - with its Super Retina screen, you can enjoy an immersive-viewing experience that dazzles the eyes.</span></p><p style=""><span style="font-size: 14.4px;"><strong>Innovative Display Technology</strong></span></p><p style=""><span style="font-size: 14.4px;">The display, with new techniques and technology, follows the curves and its elegantly rounded corners.</span></p><p style=""><span style="font-size: 14.4px;"><strong>OLED Screen</strong></span></p><p style=""><span style="font-size: 14.4px;">Everything on the screen looks vibrant and beautiful, with true blacks, stunning colors, high brightness, and a 1,000,000 to 1 contrast ratio.</span></p><p style=""><span style="font-size: 14.4px;"><strong>IP67 Rating</strong></span></p><p style=""><span style="font-size: 14.4px;">Crafted using durable glass on both the sides, this phone, with surgical-grade stainless steel, is water- and dust-resistant.</span></p><p style=""><span style="font-size: 14.4px;"><strong>Intuitive Gestures</strong></span></p><p style=""><span style="font-size: 14.4px;">Navigating your phone using familiar gestures will be intuitive and natural. All it takes is a simple swipe to take you to your home screen from anywhere.</span></p><p style=""><span style="font-size: 14.4px;"><strong>Your Face is Your Password</strong></span></p><p style=""><span style="font-size: 14.4px;">Experience secure authentication with its Face ID; it projects and analyses more than 30,000 invisible dots on your face to create a depth map. What''s more, enabled by the TrueDepth camera and equipped with an adaptive recognition, the Face ID adapts to your face''s physical changes over time.</span></p><p style=""><span style="font-size: 14.4px;"><strong>Portrait Mode Selfies and Portrait Lighting</strong></span></p><p style=""><span style="font-size: 14.4px;">Click beautiful selfies with sharp foregrounds, blurred backgrounds and impressive studio-quality lighting effects.</span></p>', 1, 0, 0, 0, 0, 1, NULL, CAST(1029.00000 AS Numeric(18, 5)), NULL, NULL, NULL, 1, 0, CAST(N'2019-07-03T14:05:01.890' AS DateTime), CAST(N'2020-02-18T09:05:08.497' AS DateTime), NULL, 0, 0, 1, 0, 3, NULL, 1, 0, 0, CAST(450.00000 AS Numeric(18, 5)), 1, CAST(50.00000 AS Numeric(18, 5)), 1, CAST(50.00000 AS Numeric(18, 5)), 1, CAST(50.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 0, 0, 0, 4, 0, 0, 1, 0, NULL, 0, 0, 0, 0)

INSERT [dbo].[Product] ([Name], [Summary], [Description], [IsShippable], [IsDownloadable], [IsFeatured], [IsVisibleIndividually], [TrackInventory], [CanOrderWhenOutOfStock], [ComparePrice], [Price], [Sku], [Gtin], [Mpn], [MinimumPurchaseQuantity], [MaximumPurchaseQuantity], [CreatedOn], [UpdatedOn], [ParentProductId], [DisplayOrder], [ChargeTaxes], [Published], [Deleted], [ManufacturerId], [TaxId], [HasVariants], [ReviewsDisabled], [PopularityIndex], [PackageWeight], [PackageWeightUnit], [PackageWidth], [PackageWidthUnit], [PackageHeight], [PackageHeightUnit], [PackageLength], [PackageLengthUnit], [AdditionalShippingCharge], [IndividuallyShipped], [AllowReturns], [DaysForReturn], [Id], [ProductType], [ProductSaleType], [SubscriptionCycle], [CycleCount], [TrialDays], [RequireLoginToPurchase], [RequireLoginToViewPrice], [RestrictedToRoles], [DisableSale]) VALUES (N'Canon Pixma G3000 All-in-One Wireless Ink Tank Color Printer', N'<p><span style="font-size: 14.4px;">Canon''s first refillable ink tank system All-In-One wireless printer is designed for high volume printing at low running cost.</span></p>', N'<p><span style="font-size: 14.4px;"><strong>High Page Yield Ink Bottles</strong></span></p><p><span style="font-size: 14.4px;">With high page yield ink bottles up to 7000 pages, users can enjoy printing without having to worry about cost of ink, or ink supplies running low.</span></p><p style=""><span style="font-size: 14.4px;"><strong>Integrated Ink Tank System</strong></span></p><p style=""><span style="font-size: 14.4px;">Built-in integrated ink tanks create a compact printer body. Users can also view remaining ink levels easily at a glance.</span></p><p style=""><span style="font-size: 14.4px;"><strong>Quality Photo and Document Printing</strong></span></p><p style=""><span style="font-size: 14.4px;">Borderless photos can be printed up to A4 size, and Canon’s Hybrid ink system is equally adept at producing crisp black text documents and stunning photos.</span></p><p style=""><span style="font-size: 14.4px;"><strong>Wireless LAN</strong></span></p><p style=""><span style="font-size: 14.4px;">Built-in wireless LAN connectivity allows users to print wirelessly from PCs, laptops, mobile phones and tablet computers.</span></p><p style=""><span style="font-size: 14.4px;"><strong>Access Point Mode</strong></span></p><p style=""><span style="font-size: 14.4px;">The G3000 can behave as an access point in the absence of a wireless router, allowing a direct connection to be established to mobile phones and tablet PCs.</span></p><p style=""><span style="font-size: 14.4px;"><strong>Mobile and Cloud Printing</strong></span></p><p style=""><span style="font-size: 14.4px;">The new Canon PRINT Inkjet/SELPHY app supports easy, guided printing from mobile phones and tablet PCs. Also supports printing from cloud services such as Facebook and Dropbox via PIXMA Cloud Link.</span></p>', 1, 0, 0, 0, 1, 0, NULL, CAST(169.00000 AS Numeric(18, 5)), NULL, NULL, NULL, 1, 1, CAST(N'2019-07-04T05:39:33.700' AS DateTime), CAST(N'2019-07-04T10:56:44.030' AS DateTime), NULL, 0, 0, 1, 0, 4, NULL, 0, 0, 0, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 0, 0, 0, 5, 0, 0, 0, 0, NULL, 0, 0, 0, 0)

INSERT [dbo].[Product] ([Name], [Summary], [Description], [IsShippable], [IsDownloadable], [IsFeatured], [IsVisibleIndividually], [TrackInventory], [CanOrderWhenOutOfStock], [ComparePrice], [Price], [Sku], [Gtin], [Mpn], [MinimumPurchaseQuantity], [MaximumPurchaseQuantity], [CreatedOn], [UpdatedOn], [ParentProductId], [DisplayOrder], [ChargeTaxes], [Published], [Deleted], [ManufacturerId], [TaxId], [HasVariants], [ReviewsDisabled], [PopularityIndex], [PackageWeight], [PackageWeightUnit], [PackageWidth], [PackageWidthUnit], [PackageHeight], [PackageHeightUnit], [PackageLength], [PackageLengthUnit], [AdditionalShippingCharge], [IndividuallyShipped], [AllowReturns], [DaysForReturn], [Id], [ProductType], [ProductSaleType], [SubscriptionCycle], [CycleCount], [TrialDays], [RequireLoginToPurchase], [RequireLoginToViewPrice], [RestrictedToRoles], [DisableSale]) VALUES (N'HP 310 All-in-One Ink Tank Color Printer', NULL, N'<p><span style="font-size: 14.4px;">Get up to 8,000 color or 6,000 black pages at an extremely low cost-per page. Print thousands of pages with high-capacity ink tank system. Restore ink levels with resealable bottles and our spill-free refill system. Easily refill your ink tank system with spill-free, resealable bottles. Print darker, crisper text and get borderless, fade-resistant photos and documents that last up to 22 times longer. Count on darker, crisper text, time after time. Functions: Print, copy, scan Color, A4: Up to 5 ppm, Black &amp; White, A4: Up to 8 ppm Orderable Supplies: HP GT52 Cyan Original Ink Bottle, HP GT52 Magenta Original Ink Bottle, HP GT52 Yellow Original Ink Bottle, HP GT51 Black Original Ink Bottle.</span></p>', 1, 0, 0, 0, 1, 0, CAST(199.00000 AS Numeric(18, 5)), CAST(149.00000 AS Numeric(18, 5)), N'Z6Z11A', NULL, NULL, 1, 0, CAST(N'2019-07-04T11:01:53.777' AS DateTime), CAST(N'2019-07-04T11:04:47.537' AS DateTime), NULL, 0, 0, 1, 0, 1, NULL, 0, 0, 0, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 1, 0, 0, 6, 0, 0, 0, 0, NULL, 0, 0, 0, 0)

INSERT [dbo].[Product] ([Name], [Summary], [Description], [IsShippable], [IsDownloadable], [IsFeatured], [IsVisibleIndividually], [TrackInventory], [CanOrderWhenOutOfStock], [ComparePrice], [Price], [Sku], [Gtin], [Mpn], [MinimumPurchaseQuantity], [MaximumPurchaseQuantity], [CreatedOn], [UpdatedOn], [ParentProductId], [DisplayOrder], [ChargeTaxes], [Published], [Deleted], [ManufacturerId], [TaxId], [HasVariants], [ReviewsDisabled], [PopularityIndex], [PackageWeight], [PackageWeightUnit], [PackageWidth], [PackageWidthUnit], [PackageHeight], [PackageHeightUnit], [PackageLength], [PackageLengthUnit], [AdditionalShippingCharge], [IndividuallyShipped], [AllowReturns], [DaysForReturn], [Id], [ProductType], [ProductSaleType], [SubscriptionCycle], [CycleCount], [TrialDays], [RequireLoginToPurchase], [RequireLoginToViewPrice], [RestrictedToRoles], [DisableSale]) VALUES (N'Canon PG-47 Ink Cartridge', NULL, N'<p><span style="font-size: 14.4px;"><strong>Smooth Performance</strong></span></p><p><span style="font-size: 14.4px;">Running out of Ink for your Canon Inkjet printer? Get the Canon original PG-47 Ink Cartridge for flawless prints. This black ink cartridge offers smear free prints that lasts for a long time. Be it home or office, it is easy to install and not messy at all. It is engineered for high performance with smudge free, no smears and rich prints. With a high yield capacity of 400 pages, this cartridge is meant for superior performance. The durable and sturdy cartridge makes sure your Canon printer works smoothly without any glitches. Make sure you use original cartridge for a flawless printing experience.</span></p><span style="font-size: 14.4px;"><strong><br></strong></span><p><span style="font-size: 14.4px;"><strong>Compatibility and Design</strong></span></p><p><span style="font-size: 14.4px;">Built for inkjet printing technology, this Canon Cartridge is compatible with Canon E400 printers. Get prominent and superior prints at your office or at home with this cartridge. Your kid’s homework print or that urgent print at office can happen in no time with perfection. It has a dimension of 4.5 x 3 x 4.5 cm that fits perfectly for Canon’s Inkjet technology. It is light weight with just 32 g. You can easily carry it along with your office stationary. Grab the Canon PG-47 Ink Cartridge (Black) for improved productivity.</span></p>', 1, 0, 0, 0, 1, 0, CAST(11.00000 AS Numeric(18, 5)), CAST(10.00000 AS Numeric(18, 5)), NULL, NULL, NULL, 1, 0, CAST(N'2019-07-04T11:45:43.337' AS DateTime), CAST(N'2019-07-04T11:46:40.013' AS DateTime), NULL, 0, 0, 1, 0, 4, NULL, 1, 0, 0, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 0, 0, 0, 7, 0, 0, 0, 0, NULL, 0, 0, 0, 0)

INSERT [dbo].[Product] ([Name], [Summary], [Description], [IsShippable], [IsDownloadable], [IsFeatured], [IsVisibleIndividually], [TrackInventory], [CanOrderWhenOutOfStock], [ComparePrice], [Price], [Sku], [Gtin], [Mpn], [MinimumPurchaseQuantity], [MaximumPurchaseQuantity], [CreatedOn], [UpdatedOn], [ParentProductId], [DisplayOrder], [ChargeTaxes], [Published], [Deleted], [ManufacturerId], [TaxId], [HasVariants], [ReviewsDisabled], [PopularityIndex], [PackageWeight], [PackageWeightUnit], [PackageWidth], [PackageWidthUnit], [PackageHeight], [PackageHeightUnit], [PackageLength], [PackageLengthUnit], [AdditionalShippingCharge], [IndividuallyShipped], [AllowReturns], [DaysForReturn], [Id], [ProductType], [ProductSaleType], [SubscriptionCycle], [CycleCount], [TrialDays], [RequireLoginToPurchase], [RequireLoginToViewPrice], [RestrictedToRoles], [DisableSale]) VALUES (N'Apple Watch Series 4', N'<p><span style="font-size: 14.4px;">Apple Watch Series 4. Fundamentally redesigned and re‑engineered to help you be even more active, healthy, and connected.</span></p>', N'<p><span style="font-size: 14.4px;"><strong>Part guardian. Part guru.</strong></span></p><p><span style="font-size: 14.4px;">ECG on your wrist. Notifications for low and high heart rate, and irregular rhythm. Fall detection and Emergency SOS. Breathe watch faces. It’s designed to improve your health every day and powerful enough to help protect it.</span></p><p style=""><span style="font-size: 14.4px;"><strong>Workouts that work harder.</strong></span></p><p style=""><span style="font-size: 14.4px;">Automatic workout detection. Yoga and hiking workouts. Advanced features for runners like cadence and pace alerts. See up to five metrics at a glance as you precisely track all your favorite ways to train.</span></p><p style=""><span style="font-size: 14.4px;"><strong>Made to motivate.</strong></span></p><p style=""><span style="font-size: 14.4px;">Head-to-head competitions. Activity sharing with friends. Personalized coaching. Monthly challenges. Achievement awards. Get all the motivation you need to close your Activity rings every day.</span></p><p><span style="font-size: 14.4px;"><strong>The freedom of cellular.</strong></span></p><p><span style="font-size: 14.4px;">Walkie-Talkie, phone calls, and messages. Stream Apple Music and Apple Podcasts.* More ways to use Siri. Built-in cellular lets you do it all on your watch — even while you’re away from your phone.</span></p>', 1, 0, 0, 0, 0, 0, NULL, CAST(399.00000 AS Numeric(18, 5)), NULL, NULL, NULL, 1, 0, CAST(N'2019-07-04T12:20:17.103' AS DateTime), CAST(N'2020-01-02T09:45:03.400' AS DateTime), NULL, 0, 0, 1, 0, 3, NULL, 1, 0, 0, CAST(500.00000 AS Numeric(18, 5)), 1, CAST(50.00000 AS Numeric(18, 5)), 1, CAST(20.00000 AS Numeric(18, 5)), 1, CAST(50.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 0, 0, 0, 8, 0, 0, 1, 0, NULL, 0, 0, 0, 0)

INSERT [dbo].[Product] ([Name], [Summary], [Description], [IsShippable], [IsDownloadable], [IsFeatured], [IsVisibleIndividually], [TrackInventory], [CanOrderWhenOutOfStock], [ComparePrice], [Price], [Sku], [Gtin], [Mpn], [MinimumPurchaseQuantity], [MaximumPurchaseQuantity], [CreatedOn], [UpdatedOn], [ParentProductId], [DisplayOrder], [ChargeTaxes], [Published], [Deleted], [ManufacturerId], [TaxId], [HasVariants], [ReviewsDisabled], [PopularityIndex], [PackageWeight], [PackageWeightUnit], [PackageWidth], [PackageWidthUnit], [PackageHeight], [PackageHeightUnit], [PackageLength], [PackageLengthUnit], [AdditionalShippingCharge], [IndividuallyShipped], [AllowReturns], [DaysForReturn], [Id], [ProductType], [ProductSaleType], [SubscriptionCycle], [CycleCount], [TrialDays], [RequireLoginToPurchase], [RequireLoginToViewPrice], [RestrictedToRoles], [DisableSale]) VALUES (N'WD Elements Portable', N'<p><span style="font-size: 14.4px;">WD Elements™ portable hard drives with USB 3.0 offer reliable, high-capacity storage to go, fast data transfer rates, universal connectivity and massive capacity for value-conscious consumers.</span></p>', N'<p><span style="font-size: 14.4px;"><strong>MASSIVE CAPACITY IN A SMALL ENCLOSURE</strong></span></p><p><span style="font-size: 14.4px;">The small, lightweight design offers up to 4TB capacity, making WD Elements portable hard drives the ideal companion for consumers on the go.</span></p><p style=""><span style="font-size: 14.4px;"><strong>WD QUALITY INSIDE AND OUT</strong></span></p><p style=""><span style="font-size: 14.4px;">We know your data is important to you. So we build the drive inside to our demanding requirements for durability, shock tolerance, and long-term reliability. Then we protect the drive with a durable enclosure designed for style and protection.</span></p><p style=""><span style="font-size: 14.4px;"><strong>TECHNICAL SPECIFICATIONS</strong></span></p><p style=""><span style="font-size: 14.4px;">Formatted NTFS for Windows® 10, Windows 8.1, Windows 7. Reformatting may be required for other operating systems. Compatibility may vary depending on user’s hardware configuration and operating system.</span></p><br>', 1, 1, 0, 0, 0, 0, CAST(69.99000 AS Numeric(18, 5)), CAST(54.99000 AS Numeric(18, 5)), NULL, NULL, NULL, 1, 0, CAST(N'2019-07-04T12:44:12.007' AS DateTime), CAST(N'2020-02-01T13:22:05.163' AS DateTime), NULL, 0, 0, 1, 0, 5, NULL, 1, 0, 0, CAST(500.00000 AS Numeric(18, 5)), 1, CAST(6.00000 AS Numeric(18, 5)), 4, CAST(6.00000 AS Numeric(18, 5)), 4, CAST(6.00000 AS Numeric(18, 5)), 4, CAST(0.00000 AS Numeric(18, 5)), 0, 0, 0, 9, 0, 0, 1, 0, NULL, 0, 0, 0, 0)

INSERT [dbo].[Product] ([Name], [Summary], [Description], [IsShippable], [IsDownloadable], [IsFeatured], [IsVisibleIndividually], [TrackInventory], [CanOrderWhenOutOfStock], [ComparePrice], [Price], [Sku], [Gtin], [Mpn], [MinimumPurchaseQuantity], [MaximumPurchaseQuantity], [CreatedOn], [UpdatedOn], [ParentProductId], [DisplayOrder], [ChargeTaxes], [Published], [Deleted], [ManufacturerId], [TaxId], [HasVariants], [ReviewsDisabled], [PopularityIndex], [PackageWeight], [PackageWeightUnit], [PackageWidth], [PackageWidthUnit], [PackageHeight], [PackageHeightUnit], [PackageLength], [PackageLengthUnit], [AdditionalShippingCharge], [IndividuallyShipped], [AllowReturns], [DaysForReturn], [Id], [ProductType], [ProductSaleType], [SubscriptionCycle], [CycleCount], [TrialDays], [RequireLoginToPurchase], [RequireLoginToViewPrice], [RestrictedToRoles], [DisableSale]) VALUES (N'My Passport X', N'<p><span style="font-size: 14.4px;">Connect this portable and powerful drive to immediately add storage capacity and expand your console or PC gaming experience.</span></p>', N'<p><span style="font-size: 14.4px;"><strong>More Zombies to Conquer</strong></span></p><p><span style="font-size: 14.4px;">The My Passport X drive is the perfect way to store and expand your gaming experience.</span></p><p><span style="font-size: 14.4px;"><strong>Play Anywhere</strong></span></p><p><span style="font-size: 14.4px;">Compact design allows you to take your gaming lifestyle with you – and look good doing it.1</span></p><p><span style="font-size: 14.4px;"><strong>Performance Tweaked</strong></span></p><p><span style="font-size: 14.4px;">It’s like giving your gaming avatar super-lifting strength and lightning-quick gaming speed.</span></p>', 1, 0, 0, 0, 0, 0, NULL, CAST(89.99000 AS Numeric(18, 5)), NULL, NULL, NULL, 1, 0, CAST(N'2019-07-04T13:30:02.120' AS DateTime), CAST(N'2020-02-08T07:44:45.620' AS DateTime), NULL, 0, 0, 1, 0, 5, NULL, 1, 0, 0, CAST(10.00000 AS Numeric(18, 5)), 1, CAST(10.00000 AS Numeric(18, 5)), 1, CAST(10.00000 AS Numeric(18, 5)), 1, CAST(10.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 0, 0, 0, 10, 0, 0, 1, 0, NULL, 0, 0, 0, 0)

INSERT [dbo].[Product] ([Name], [Summary], [Description], [IsShippable], [IsDownloadable], [IsFeatured], [IsVisibleIndividually], [TrackInventory], [CanOrderWhenOutOfStock], [ComparePrice], [Price], [Sku], [Gtin], [Mpn], [MinimumPurchaseQuantity], [MaximumPurchaseQuantity], [CreatedOn], [UpdatedOn], [ParentProductId], [DisplayOrder], [ChargeTaxes], [Published], [Deleted], [ManufacturerId], [TaxId], [HasVariants], [ReviewsDisabled], [PopularityIndex], [PackageWeight], [PackageWeightUnit], [PackageWidth], [PackageWidthUnit], [PackageHeight], [PackageHeightUnit], [PackageLength], [PackageLengthUnit], [AdditionalShippingCharge], [IndividuallyShipped], [AllowReturns], [DaysForReturn], [Id], [ProductType], [ProductSaleType], [SubscriptionCycle], [CycleCount], [TrialDays], [RequireLoginToPurchase], [RequireLoginToViewPrice], [RestrictedToRoles], [DisableSale]) VALUES (N'Samsung LED Smart TV', N'<p><span style="font-size: 14.4px;">Make blurry images a thing of the past. Digital Clean View improves your content no matter the quality. See impressive colour with Wide Colour Enhancer. Improve the quality of any image, uncover hidden details and see colours as they were meant to be seen.</span></p>', N'<p><span style="font-size: 14.4px;"><strong>Share the Moment</strong></span></p><p><span style="font-size: 14.4px;">Live stream what you like, share it and entertain everyone. With live cast, you can broadcast your experiences from anywhere at any time, right on to your Samsung Smart TV.</span></p><p><span style="font-size: 14.4px;"><strong>Your Entertainment, on Any Screen</strong></span></p><p><span style="font-size: 14.4px;">Let your smartphone** and Smart TV work together to maximize your entertainment. Play music and videos from your phone to your TV for a big screen experience or carry your TV content to your phone for personal enjoyment.</span></p><p><span style="font-size: 14.4px;"><strong>Single Access for all Your Content</strong></span></p><p><span style="font-size: 14.4px;">The Smart Hub provides a single access to live TV, apps and other sources. You can browse content while watching TV and check out the thumbnail previews before diving in.</span></p><p><span style="font-size: 14.4px;"><strong>Immersive Sound</strong></span></p><p><span style="font-size: 14.4px;">Great sound for great entertainment, and no need for a separate system. Dive into your content with more immersive audio. Beamforming technology and 4Ch 40W sound surrounds with the dynamism of a concert hall.</span></p><p><span style="font-size: 14.4px;"><strong>HDR</strong></span></p><p><span style="font-size: 14.4px;">Watch HDR content with better clarity and detailed colour expression. Samsung HD TV gives you more accurate details in bright and dark scenes.</span></p><p><span style="font-size: 14.4px;"><strong>Ultra Clean View</strong></span></p><p><span style="font-size: 14.4px;">Analyzing original content with an advanced algorithm, ultra clean view gives you higher quality images with less distortion. Enjoy the clear picture.</span></p><p><span style="font-size: 14.4px;"><strong>PurColour</strong></span></p><p><span style="font-size: 14.4px;">Watch your favourite content with natural colours that deliver details as crisp as the real thing. Get a more colourful viewing experience.</span></p><p><span style="font-size: 14.4px;"><strong>Micro Dimming Pro</strong></span></p><p><span style="font-size: 14.4px;">Experience shadow detail and colour. Dividing the screen into zones, micro dimming pro analyzes each one for deeper blacks and purer whites.</span></p><p><span style="font-size: 14.4px;"><strong>SmartThings App, Just One App for all</strong></span></p><p><span style="font-size: 14.4px;">Availability of the feature and Graphic User Interface (GUI) may vary by region. Check before use.The SmartThings app also offers features such as remote control, and mirror screen.</span></p>', 1, 0, 0, 0, 0, 0, CAST(369.00000 AS Numeric(18, 5)), CAST(329.00000 AS Numeric(18, 5)), N'UA32N4310', NULL, NULL, 1, 0, CAST(N'2019-07-04T14:33:48.143' AS DateTime), CAST(N'2020-02-18T10:34:49.110' AS DateTime), NULL, 0, 0, 1, 0, 6, NULL, 0, 0, 0, CAST(10.00000 AS Numeric(18, 5)), 2, CAST(50.00000 AS Numeric(18, 5)), 4, CAST(50.00000 AS Numeric(18, 5)), 1, CAST(20.00000 AS Numeric(18, 5)), 4, CAST(200.00000 AS Numeric(18, 5)), 1, 0, 0, 11, 0, 0, 1, 0, NULL, 0, 0, 0, 0)

INSERT [dbo].[Product] ([Name], [Summary], [Description], [IsShippable], [IsDownloadable], [IsFeatured], [IsVisibleIndividually], [TrackInventory], [CanOrderWhenOutOfStock], [ComparePrice], [Price], [Sku], [Gtin], [Mpn], [MinimumPurchaseQuantity], [MaximumPurchaseQuantity], [CreatedOn], [UpdatedOn], [ParentProductId], [DisplayOrder], [ChargeTaxes], [Published], [Deleted], [ManufacturerId], [TaxId], [HasVariants], [ReviewsDisabled], [PopularityIndex], [PackageWeight], [PackageWeightUnit], [PackageWidth], [PackageWidthUnit], [PackageHeight], [PackageHeightUnit], [PackageLength], [PackageLengthUnit], [AdditionalShippingCharge], [IndividuallyShipped], [AllowReturns], [DaysForReturn], [Id], [ProductType], [ProductSaleType], [SubscriptionCycle], [CycleCount], [TrialDays], [RequireLoginToPurchase], [RequireLoginToViewPrice], [RestrictedToRoles], [DisableSale]) VALUES (N'Sony Bravia Full HD LED Smart TV', N'<p><span style="font-size: 14.4px;">X-Reality PRO picture processing upscales every pixel for exceptional Full HD clarity. As frames are analysed, each scene is matched with our special image database to refine images and reduce noise. See how the architecture in the building is enhanced with extra details.</span></p>', N'<p><span style="font-size: 14.4px;"><strong>Discover thrilling HDR entertainment</strong></span></p><p><span style="font-size: 14.4px;">This TV brings you the excitement of movies and games in vividly detailed HDR. It handles a variety of HDR formats, including HDR10 and Hybrid Log-Gamma.</span></p><p style=""><span style="font-size: 14.4px;"><strong>Access YouTube™ instantly with one click</strong></span></p><p style=""><span style="font-size: 14.4px;">Go straight to YouTube and enjoy all your favourite videos. We have made watching clips ultra-fast on this internet-ready TV with YouTube and included a YouTube button on the remote control for easy browsing</span></p><p style=""><span style="font-size: 14.4px;"><strong>Power your entertainment with deep bass</strong></span></p><p style=""><span style="font-size: 14.4px;">Take your place in the front row. We''ve built a subwoofer into this TV so you can feel right at the heart of the action when watching concerts and movies. Hear deep bass riffs, soaring vocals and powerful soundtracks.</span></p><p style=""><span style="font-size: 14.4px;"><strong>Made to listen</strong></span></p><p style=""><span style="font-size: 14.4px;">Make your listening as lifelike as your viewing. ClearAudio+ fine tunes TV sound offers an immersive, emotionally enriching experience that seems to surround you. Hear music and dialogue with greater clarity and separation, whatever you''re watching.</span></p><p style=""><span style="font-size: 14.4px;"><strong>A smarter way to enjoy your smartphone</strong></span></p><p style=""><span style="font-size: 14.4px;">Take all the things you love on your smartphone or USB drive and enjoy them in beautiful detail on your large-screen TV. Smart Plug and Play lets you easily share videos, photos and music from multiple devices</span></p><p style=""><span style="font-size: 14.4px;"><strong>Performs in the toughest conditions</strong></span></p><p style=""><span style="font-size: 14.4px;">Enjoy your entertainment, without any disruption. This TV with X-Protection PRO is extremely durable and protected from dust, humidity, power surges and even lightning strikes.</span></p>', 1, 1, 0, 0, 1, 0, NULL, CAST(439.00000 AS Numeric(18, 5)), N'KLV-32W672F', NULL, NULL, 1, 1, CAST(N'2019-07-04T14:45:51.763' AS DateTime), CAST(N'2019-12-26T08:56:48.993' AS DateTime), NULL, 0, 0, 1, 0, 7, NULL, 0, 0, 0, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 1, 0, 0, 12, 0, 0, 1, 0, NULL, 1, 1, 0, 0)

INSERT [dbo].[Product] ([Name], [Summary], [Description], [IsShippable], [IsDownloadable], [IsFeatured], [IsVisibleIndividually], [TrackInventory], [CanOrderWhenOutOfStock], [ComparePrice], [Price], [Sku], [Gtin], [Mpn], [MinimumPurchaseQuantity], [MaximumPurchaseQuantity], [CreatedOn], [UpdatedOn], [ParentProductId], [DisplayOrder], [ChargeTaxes], [Published], [Deleted], [ManufacturerId], [TaxId], [HasVariants], [ReviewsDisabled], [PopularityIndex], [PackageWeight], [PackageWeightUnit], [PackageWidth], [PackageWidthUnit], [PackageHeight], [PackageHeightUnit], [PackageLength], [PackageLengthUnit], [AdditionalShippingCharge], [IndividuallyShipped], [AllowReturns], [DaysForReturn], [Id], [ProductType], [ProductSaleType], [SubscriptionCycle], [CycleCount], [TrialDays], [RequireLoginToPurchase], [RequireLoginToViewPrice], [RestrictedToRoles], [DisableSale]) VALUES (N'Kaspersky Internet Security', N'<p><span style="font-size: 14.4px;">When you go online shopping or banking – we protect your money &amp; account details… when you socialize – we safeguard your identity… when you surf – we prevent attacks… when you download or stream – we block infected files.</span></p><span style="font-size: 14.4px;">And it''s awesome to enter here.</span>', N'<p><span style="font-size: 14.4px;">Whatever you do in your digital life – our premium protection is here to help you protect it all.</span></p><p></p><ul><li><span style="font-size: 14.4px;">Protects against attacks, ransomware &amp; more</span></li><li>Protects your privacy &amp; personal information</li><li>Protects money when you bank &amp; shop online</li></ul><p></p>', 1, 1, 0, 0, 0, 0, NULL, CAST(39.99000 AS Numeric(18, 5)), NULL, NULL, NULL, 1, 0, CAST(N'2019-07-05T07:51:04.043' AS DateTime), CAST(N'2020-02-29T10:47:48.050' AS DateTime), NULL, 0, 0, 1, 0, 8, NULL, 1, 0, 0, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 0, 0, 0, 13, 0, 0, 1, 0, NULL, 0, 0, 0, 0)

INSERT [dbo].[Product] ([Name], [Summary], [Description], [IsShippable], [IsDownloadable], [IsFeatured], [IsVisibleIndividually], [TrackInventory], [CanOrderWhenOutOfStock], [ComparePrice], [Price], [Sku], [Gtin], [Mpn], [MinimumPurchaseQuantity], [MaximumPurchaseQuantity], [CreatedOn], [UpdatedOn], [ParentProductId], [DisplayOrder], [ChargeTaxes], [Published], [Deleted], [ManufacturerId], [TaxId], [HasVariants], [ReviewsDisabled], [PopularityIndex], [PackageWeight], [PackageWeightUnit], [PackageWidth], [PackageWidthUnit], [PackageHeight], [PackageHeightUnit], [PackageLength], [PackageLengthUnit], [AdditionalShippingCharge], [IndividuallyShipped], [AllowReturns], [DaysForReturn], [Id], [ProductType], [ProductSaleType], [SubscriptionCycle], [CycleCount], [TrialDays], [RequireLoginToPurchase], [RequireLoginToViewPrice], [RestrictedToRoles], [DisableSale]) VALUES (N'Microsoft Office Home and Business 2016', NULL, N'<p><span style="font-size: 14.4px;">Microsoft Office 2016 helps you to do your best work - anywhere, anytime and with anyone. New, modern versions of the classic desktop applications, Word, Excel, PowerPoint, Outlook, and OneNote, are built for maximum productivity. You''ll quickly produce professional documents with rich authoring features, design controls for pixel-perfect layouts and intuitive tools to help you make the most of your data. You''ll have access to your docs in the cloud whenever you need them. With your documents stored online, it''s easy to get your team on the same page. Share, present and work together on projects with built in team collaboration tools across the suite.&nbsp;</span></p>', 0, 1, 0, 0, 0, 0, NULL, CAST(229.99000 AS Numeric(18, 5)), NULL, NULL, NULL, 1, 0, CAST(N'2019-07-05T08:13:21.567' AS DateTime), CAST(N'2020-02-29T10:26:33.860' AS DateTime), NULL, 0, 0, 1, 0, 2, NULL, 0, 0, 0, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 0, 0, 0, 14, 0, 0, 1, 0, NULL, 0, 0, 0, 0)

INSERT [dbo].[Product] ([Name], [Summary], [Description], [IsShippable], [IsDownloadable], [IsFeatured], [IsVisibleIndividually], [TrackInventory], [CanOrderWhenOutOfStock], [ComparePrice], [Price], [Sku], [Gtin], [Mpn], [MinimumPurchaseQuantity], [MaximumPurchaseQuantity], [CreatedOn], [UpdatedOn], [ParentProductId], [DisplayOrder], [ChargeTaxes], [Published], [Deleted], [ManufacturerId], [TaxId], [HasVariants], [ReviewsDisabled], [PopularityIndex], [PackageWeight], [PackageWeightUnit], [PackageWidth], [PackageWidthUnit], [PackageHeight], [PackageHeightUnit], [PackageLength], [PackageLengthUnit], [AdditionalShippingCharge], [IndividuallyShipped], [AllowReturns], [DaysForReturn], [Id], [ProductType], [ProductSaleType], [SubscriptionCycle], [CycleCount], [TrialDays], [RequireLoginToPurchase], [RequireLoginToViewPrice], [RestrictedToRoles], [DisableSale]) VALUES (N'Test Product', NULL, NULL, 0, 0, 0, 0, 1, 0, NULL, CAST(0.00000 AS Numeric(18, 5)), NULL, NULL, NULL, 1, 0, CAST(N'2019-11-16T12:50:18.227' AS DateTime), CAST(N'2019-11-16T12:50:18.227' AS DateTime), NULL, 0, 0, 0, 1, NULL, NULL, 0, 0, 0, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 0, 0, 0, 15, 0, 0, 0, 0, NULL, 0, 0, 0, 0)

INSERT [dbo].[Product] ([Name], [Summary], [Description], [IsShippable], [IsDownloadable], [IsFeatured], [IsVisibleIndividually], [TrackInventory], [CanOrderWhenOutOfStock], [ComparePrice], [Price], [Sku], [Gtin], [Mpn], [MinimumPurchaseQuantity], [MaximumPurchaseQuantity], [CreatedOn], [UpdatedOn], [ParentProductId], [DisplayOrder], [ChargeTaxes], [Published], [Deleted], [ManufacturerId], [TaxId], [HasVariants], [ReviewsDisabled], [PopularityIndex], [PackageWeight], [PackageWeightUnit], [PackageWidth], [PackageWidthUnit], [PackageHeight], [PackageHeightUnit], [PackageLength], [PackageLengthUnit], [AdditionalShippingCharge], [IndividuallyShipped], [AllowReturns], [DaysForReturn], [Id], [ProductType], [ProductSaleType], [SubscriptionCycle], [CycleCount], [TrialDays], [RequireLoginToPurchase], [RequireLoginToViewPrice], [RestrictedToRoles], [DisableSale]) VALUES (N'Subscription Product', NULL, NULL, 1, 1, 0, 0, 0, 0, NULL, CAST(1.00000 AS Numeric(18, 5)), NULL, NULL, NULL, 1, 0, CAST(N'2019-12-04T09:26:15.030' AS DateTime), CAST(N'2020-02-27T08:54:21.053' AS DateTime), NULL, 0, 0, 1, 0, NULL, NULL, 0, 0, 0, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 0, 0, 0, 16, 0, 1, 30, 1, NULL, 0, 0, 0, 0)

SET IDENTITY_INSERT [dbo].[Product] OFF

SET IDENTITY_INSERT [dbo].[ProductVariant] ON 

INSERT [dbo].[ProductVariant] ([ProductId], [Sku], [Gtin], [Mpn], [Price], [ComparePrice], [TrackInventory], [CanOrderWhenOutOfStock], [MediaId], [DisableSale], [Id]) VALUES (2, NULL, NULL, NULL, NULL, NULL, 1, 0, 0, 0, 1)

INSERT [dbo].[ProductVariant] ([ProductId], [Sku], [Gtin], [Mpn], [Price], [ComparePrice], [TrackInventory], [CanOrderWhenOutOfStock], [MediaId], [DisableSale], [Id]) VALUES (2, NULL, NULL, NULL, CAST(520.00000 AS Numeric(18, 5)), NULL, 1, 0, 0, 0, 2)

INSERT [dbo].[ProductVariant] ([ProductId], [Sku], [Gtin], [Mpn], [Price], [ComparePrice], [TrackInventory], [CanOrderWhenOutOfStock], [MediaId], [DisableSale], [Id]) VALUES (3, NULL, NULL, NULL, NULL, NULL, 1, 0, 0, 0, 3)

INSERT [dbo].[ProductVariant] ([ProductId], [Sku], [Gtin], [Mpn], [Price], [ComparePrice], [TrackInventory], [CanOrderWhenOutOfStock], [MediaId], [DisableSale], [Id]) VALUES (3, NULL, NULL, NULL, CAST(1979.00000 AS Numeric(18, 5)), NULL, 1, 0, 0, 0, 4)

INSERT [dbo].[ProductVariant] ([ProductId], [Sku], [Gtin], [Mpn], [Price], [ComparePrice], [TrackInventory], [CanOrderWhenOutOfStock], [MediaId], [DisableSale], [Id]) VALUES (3, NULL, NULL, NULL, CAST(1100.00000 AS Numeric(18, 5)), NULL, 1, 0, 0, 0, 5)

INSERT [dbo].[ProductVariant] ([ProductId], [Sku], [Gtin], [Mpn], [Price], [ComparePrice], [TrackInventory], [CanOrderWhenOutOfStock], [MediaId], [DisableSale], [Id]) VALUES (4, NULL, NULL, NULL, NULL, NULL, 1, 1, 0, 0, 6)

INSERT [dbo].[ProductVariant] ([ProductId], [Sku], [Gtin], [Mpn], [Price], [ComparePrice], [TrackInventory], [CanOrderWhenOutOfStock], [MediaId], [DisableSale], [Id]) VALUES (4, NULL, NULL, NULL, NULL, NULL, 1, 0, 0, 0, 7)

INSERT [dbo].[ProductVariant] ([ProductId], [Sku], [Gtin], [Mpn], [Price], [ComparePrice], [TrackInventory], [CanOrderWhenOutOfStock], [MediaId], [DisableSale], [Id]) VALUES (4, NULL, NULL, NULL, CAST(1095.00000 AS Numeric(18, 5)), NULL, 1, 0, 0, 0, 8)

INSERT [dbo].[ProductVariant] ([ProductId], [Sku], [Gtin], [Mpn], [Price], [ComparePrice], [TrackInventory], [CanOrderWhenOutOfStock], [MediaId], [DisableSale], [Id]) VALUES (4, NULL, NULL, NULL, CAST(1095.00000 AS Numeric(18, 5)), NULL, 1, 0, 0, 0, 9)

INSERT [dbo].[ProductVariant] ([ProductId], [Sku], [Gtin], [Mpn], [Price], [ComparePrice], [TrackInventory], [CanOrderWhenOutOfStock], [MediaId], [DisableSale], [Id]) VALUES (7, NULL, NULL, NULL, CAST(10.00000 AS Numeric(18, 5)), NULL, 1, 0, 0, 0, 10)

INSERT [dbo].[ProductVariant] ([ProductId], [Sku], [Gtin], [Mpn], [Price], [ComparePrice], [TrackInventory], [CanOrderWhenOutOfStock], [MediaId], [DisableSale], [Id]) VALUES (7, NULL, NULL, NULL, NULL, NULL, 1, 0, 0, 0, 11)

INSERT [dbo].[ProductVariant] ([ProductId], [Sku], [Gtin], [Mpn], [Price], [ComparePrice], [TrackInventory], [CanOrderWhenOutOfStock], [MediaId], [DisableSale], [Id]) VALUES (7, NULL, NULL, NULL, CAST(15.00000 AS Numeric(18, 5)), NULL, 1, 0, 0, 0, 12)

INSERT [dbo].[ProductVariant] ([ProductId], [Sku], [Gtin], [Mpn], [Price], [ComparePrice], [TrackInventory], [CanOrderWhenOutOfStock], [MediaId], [DisableSale], [Id]) VALUES (7, NULL, NULL, NULL, CAST(11.00000 AS Numeric(18, 5)), NULL, 1, 0, 0, 0, 13)

INSERT [dbo].[ProductVariant] ([ProductId], [Sku], [Gtin], [Mpn], [Price], [ComparePrice], [TrackInventory], [CanOrderWhenOutOfStock], [MediaId], [DisableSale], [Id]) VALUES (8, NULL, NULL, NULL, CAST(399.00000 AS Numeric(18, 5)), NULL, 1, 0, 0, 0, 14)

INSERT [dbo].[ProductVariant] ([ProductId], [Sku], [Gtin], [Mpn], [Price], [ComparePrice], [TrackInventory], [CanOrderWhenOutOfStock], [MediaId], [DisableSale], [Id]) VALUES (8, NULL, NULL, NULL, CAST(429.00000 AS Numeric(18, 5)), NULL, 1, 0, 0, 0, 15)

INSERT [dbo].[ProductVariant] ([ProductId], [Sku], [Gtin], [Mpn], [Price], [ComparePrice], [TrackInventory], [CanOrderWhenOutOfStock], [MediaId], [DisableSale], [Id]) VALUES (8, NULL, NULL, NULL, CAST(499.00000 AS Numeric(18, 5)), NULL, 1, 0, 0, 0, 16)

INSERT [dbo].[ProductVariant] ([ProductId], [Sku], [Gtin], [Mpn], [Price], [ComparePrice], [TrackInventory], [CanOrderWhenOutOfStock], [MediaId], [DisableSale], [Id]) VALUES (8, NULL, NULL, NULL, CAST(529.00000 AS Numeric(18, 5)), NULL, 1, 0, 0, 0, 17)

INSERT [dbo].[ProductVariant] ([ProductId], [Sku], [Gtin], [Mpn], [Price], [ComparePrice], [TrackInventory], [CanOrderWhenOutOfStock], [MediaId], [DisableSale], [Id]) VALUES (9, NULL, NULL, NULL, NULL, NULL, 1, 0, 0, 0, 18)

INSERT [dbo].[ProductVariant] ([ProductId], [Sku], [Gtin], [Mpn], [Price], [ComparePrice], [TrackInventory], [CanOrderWhenOutOfStock], [MediaId], [DisableSale], [Id]) VALUES (9, NULL, NULL, NULL, CAST(74.99000 AS Numeric(18, 5)), NULL, 1, 0, 0, 0, 19)

INSERT [dbo].[ProductVariant] ([ProductId], [Sku], [Gtin], [Mpn], [Price], [ComparePrice], [TrackInventory], [CanOrderWhenOutOfStock], [MediaId], [DisableSale], [Id]) VALUES (9, NULL, NULL, NULL, CAST(99.99000 AS Numeric(18, 5)), NULL, 1, 0, 0, 0, 20)

INSERT [dbo].[ProductVariant] ([ProductId], [Sku], [Gtin], [Mpn], [Price], [ComparePrice], [TrackInventory], [CanOrderWhenOutOfStock], [MediaId], [DisableSale], [Id]) VALUES (10, N'WDBCRM0020BBK-NESN', NULL, NULL, NULL, NULL, 1, 0, 0, 0, 21)

INSERT [dbo].[ProductVariant] ([ProductId], [Sku], [Gtin], [Mpn], [Price], [ComparePrice], [TrackInventory], [CanOrderWhenOutOfStock], [MediaId], [DisableSale], [Id]) VALUES (10, N'WDBCRM0030BBK-NESN', NULL, NULL, CAST(119.99000 AS Numeric(18, 5)), NULL, 1, 0, 0, 0, 22)

INSERT [dbo].[ProductVariant] ([ProductId], [Sku], [Gtin], [Mpn], [Price], [ComparePrice], [TrackInventory], [CanOrderWhenOutOfStock], [MediaId], [DisableSale], [Id]) VALUES (13, NULL, NULL, NULL, CAST(39.99000 AS Numeric(18, 5)), NULL, 0, 0, 0, 0, 23)

INSERT [dbo].[ProductVariant] ([ProductId], [Sku], [Gtin], [Mpn], [Price], [ComparePrice], [TrackInventory], [CanOrderWhenOutOfStock], [MediaId], [DisableSale], [Id]) VALUES (13, NULL, NULL, NULL, CAST(68.99000 AS Numeric(18, 5)), NULL, 0, 0, 0, 0, 24)

INSERT [dbo].[ProductVariant] ([ProductId], [Sku], [Gtin], [Mpn], [Price], [ComparePrice], [TrackInventory], [CanOrderWhenOutOfStock], [MediaId], [DisableSale], [Id]) VALUES (13, NULL, NULL, NULL, CAST(89.98000 AS Numeric(18, 5)), NULL, 0, 0, 0, 0, 25)

INSERT [dbo].[ProductVariant] ([ProductId], [Sku], [Gtin], [Mpn], [Price], [ComparePrice], [TrackInventory], [CanOrderWhenOutOfStock], [MediaId], [DisableSale], [Id]) VALUES (13, NULL, NULL, NULL, CAST(44.99000 AS Numeric(18, 5)), NULL, 0, 0, 0, 0, 26)

INSERT [dbo].[ProductVariant] ([ProductId], [Sku], [Gtin], [Mpn], [Price], [ComparePrice], [TrackInventory], [CanOrderWhenOutOfStock], [MediaId], [DisableSale], [Id]) VALUES (13, NULL, NULL, NULL, CAST(77.99000 AS Numeric(18, 5)), NULL, 0, 0, 0, 0, 27)

INSERT [dbo].[ProductVariant] ([ProductId], [Sku], [Gtin], [Mpn], [Price], [ComparePrice], [TrackInventory], [CanOrderWhenOutOfStock], [MediaId], [DisableSale], [Id]) VALUES (13, NULL, NULL, NULL, CAST(100.98000 AS Numeric(18, 5)), NULL, 0, 0, 0, 0, 28)

SET IDENTITY_INSERT [dbo].[ProductVariant] OFF

SET IDENTITY_INSERT [dbo].[AvailableAttribute] ON 

INSERT [dbo].[AvailableAttribute] ([Name], [Description], [Id]) VALUES (N'Processor', NULL, 1)

INSERT [dbo].[AvailableAttribute] ([Name], [Description], [Id]) VALUES (N'Screen Size', NULL, 2)

INSERT [dbo].[AvailableAttribute] ([Name], [Description], [Id]) VALUES (N'Screen Resolution', NULL, 3)

INSERT [dbo].[AvailableAttribute] ([Name], [Description], [Id]) VALUES (N'Hard Disk Size', NULL, 4)

INSERT [dbo].[AvailableAttribute] ([Name], [Description], [Id]) VALUES (N'Operating System', NULL, 5)

INSERT [dbo].[AvailableAttribute] ([Name], [Description], [Id]) VALUES (N'Graphics', NULL, 6)

INSERT [dbo].[AvailableAttribute] ([Name], [Description], [Id]) VALUES (N'Memory', NULL, 7)

INSERT [dbo].[AvailableAttribute] ([Name], [Description], [Id]) VALUES (N'Without Type Cover', NULL, 8)

INSERT [dbo].[AvailableAttribute] ([Name], [Description], [Id]) VALUES (N'Color', NULL, 9)

INSERT [dbo].[AvailableAttribute] ([Name], [Description], [Id]) VALUES (N'Storage', NULL, 10)

INSERT [dbo].[AvailableAttribute] ([Name], [Description], [Id]) VALUES (N'Display Type', NULL, 11)

INSERT [dbo].[AvailableAttribute] ([Name], [Description], [Id]) VALUES (N'Brand', NULL, 12)

INSERT [dbo].[AvailableAttribute] ([Name], [Description], [Id]) VALUES (N'Weight', NULL, 13)

INSERT [dbo].[AvailableAttribute] ([Name], [Description], [Id]) VALUES (N'Dimensions', NULL, 14)

INSERT [dbo].[AvailableAttribute] ([Name], [Description], [Id]) VALUES (N'Connectivity Type', NULL, 15)

INSERT [dbo].[AvailableAttribute] ([Name], [Description], [Id]) VALUES (N'Package Contents', NULL, 16)

INSERT [dbo].[AvailableAttribute] ([Name], [Description], [Id]) VALUES (N'Series', NULL, 17)

INSERT [dbo].[AvailableAttribute] ([Name], [Description], [Id]) VALUES (N'Size', NULL, 18)

INSERT [dbo].[AvailableAttribute] ([Name], [Description], [Id]) VALUES (N'Case Size', NULL, 19)

INSERT [dbo].[AvailableAttribute] ([Name], [Description], [Id]) VALUES (N'Width', NULL, 20)

INSERT [dbo].[AvailableAttribute] ([Name], [Description], [Id]) VALUES (N'Depth', NULL, 21)

INSERT [dbo].[AvailableAttribute] ([Name], [Description], [Id]) VALUES (N'Model Year', NULL, 22)

INSERT [dbo].[AvailableAttribute] ([Name], [Description], [Id]) VALUES (N'Number of Devices', NULL, 23)

INSERT [dbo].[AvailableAttribute] ([Name], [Description], [Id]) VALUES (N'License Term', NULL, 24)

INSERT [dbo].[AvailableAttribute] ([Name], [Description], [Id]) VALUES (N'Name', NULL, 25)

SET IDENTITY_INSERT [dbo].[AvailableAttribute] OFF

SET IDENTITY_INSERT [dbo].[ProductAttribute] ON 

INSERT [dbo].[ProductAttribute] ([ProductId], [AvailableAttributeId], [InputFieldType], [Label], [DisplayOrder], [IsRequired], [Id]) VALUES (2, 7, 12, NULL, 0, 1, 1)

INSERT [dbo].[ProductAttribute] ([ProductId], [AvailableAttributeId], [InputFieldType], [Label], [DisplayOrder], [IsRequired], [Id]) VALUES (3, 1, 12, NULL, 0, 1, 2)

INSERT [dbo].[ProductAttribute] ([ProductId], [AvailableAttributeId], [InputFieldType], [Label], [DisplayOrder], [IsRequired], [Id]) VALUES (3, 8, 7, NULL, 1, 0, 3)

INSERT [dbo].[ProductAttribute] ([ProductId], [AvailableAttributeId], [InputFieldType], [Label], [DisplayOrder], [IsRequired], [Id]) VALUES (4, 10, 12, NULL, 0, 1, 4)

INSERT [dbo].[ProductAttribute] ([ProductId], [AvailableAttributeId], [InputFieldType], [Label], [DisplayOrder], [IsRequired], [Id]) VALUES (4, 9, 12, NULL, 0, 1, 5)

INSERT [dbo].[ProductAttribute] ([ProductId], [AvailableAttributeId], [InputFieldType], [Label], [DisplayOrder], [IsRequired], [Id]) VALUES (7, 9, 12, NULL, 0, 1, 6)

INSERT [dbo].[ProductAttribute] ([ProductId], [AvailableAttributeId], [InputFieldType], [Label], [DisplayOrder], [IsRequired], [Id]) VALUES (7, 18, 12, NULL, 0, 1, 7)

INSERT [dbo].[ProductAttribute] ([ProductId], [AvailableAttributeId], [InputFieldType], [Label], [DisplayOrder], [IsRequired], [Id]) VALUES (8, 19, 12, NULL, 0, 1, 8)

INSERT [dbo].[ProductAttribute] ([ProductId], [AvailableAttributeId], [InputFieldType], [Label], [DisplayOrder], [IsRequired], [Id]) VALUES (8, 15, 12, NULL, 0, 1, 9)

INSERT [dbo].[ProductAttribute] ([ProductId], [AvailableAttributeId], [InputFieldType], [Label], [DisplayOrder], [IsRequired], [Id]) VALUES (9, 10, 12, NULL, 0, 1, 10)

INSERT [dbo].[ProductAttribute] ([ProductId], [AvailableAttributeId], [InputFieldType], [Label], [DisplayOrder], [IsRequired], [Id]) VALUES (10, 10, 12, NULL, 0, 1, 11)

INSERT [dbo].[ProductAttribute] ([ProductId], [AvailableAttributeId], [InputFieldType], [Label], [DisplayOrder], [IsRequired], [Id]) VALUES (13, 23, 12, NULL, 0, 1, 12)

INSERT [dbo].[ProductAttribute] ([ProductId], [AvailableAttributeId], [InputFieldType], [Label], [DisplayOrder], [IsRequired], [Id]) VALUES (13, 24, 12, NULL, 0, 1, 13)

INSERT [dbo].[ProductAttribute] ([ProductId], [AvailableAttributeId], [InputFieldType], [Label], [DisplayOrder], [IsRequired], [Id]) VALUES (15, 12, 12, NULL, 0, 1, 14)

INSERT [dbo].[ProductAttribute] ([ProductId], [AvailableAttributeId], [InputFieldType], [Label], [DisplayOrder], [IsRequired], [Id]) VALUES (15, 19, 12, NULL, 1, 0, 15)

INSERT [dbo].[ProductAttribute] ([ProductId], [AvailableAttributeId], [InputFieldType], [Label], [DisplayOrder], [IsRequired], [Id]) VALUES (16, 9, 11, NULL, 0, 1, 17)

SET IDENTITY_INSERT [dbo].[ProductAttribute] OFF

SET IDENTITY_INSERT [dbo].[AvailableAttributeValue] ON 

INSERT [dbo].[AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (1, N'Core i3 7100U', 1)

INSERT [dbo].[AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (2, N'15.6', 2)

INSERT [dbo].[AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (3, N'1920 x 1080 (Full HD)', 3)

INSERT [dbo].[AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (4, N'1 TB', 4)

INSERT [dbo].[AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (5, N'Windows 10 Home', 5)

INSERT [dbo].[AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (6, N'Intel HD Graphics', 6)

INSERT [dbo].[AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (7, N'4 GB', 7)

INSERT [dbo].[AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (7, N'8 GB', 8)

INSERT [dbo].[AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (1, N'8th Gen - Core i3', 9)

INSERT [dbo].[AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (1, N'8th Gen - Core i5', 10)

INSERT [dbo].[AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (1, N'8th Gen - Core i7', 11)

INSERT [dbo].[AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (8, N'Yes', 12)

INSERT [dbo].[AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (4, N'128 GB SSD', 13)

INSERT [dbo].[AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (10, N'64 GB', 14)

INSERT [dbo].[AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (10, N'256 GB', 15)

INSERT [dbo].[AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (9, N'Silver', 16)

INSERT [dbo].[AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (9, N'Space Gray', 17)

INSERT [dbo].[AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (2, N'5.8 Inches', 18)

INSERT [dbo].[AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (3, N'2436 x 1125 Pixels', 19)

INSERT [dbo].[AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (11, N'OLED Multi-touch', 20)

INSERT [dbo].[AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (12, N'Canon', 21)

INSERT [dbo].[AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (9, N'Black', 22)

INSERT [dbo].[AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (13, N'5.8 KG', 23)

INSERT [dbo].[AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (14, N'44.5 x 33 x 16.3 cm', 24)

INSERT [dbo].[AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (15, N'WiFi', 25)

INSERT [dbo].[AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (16, N'Printer with Ink Tank and Cartridge Unit Set;USB Cable, Power Cable, Print Head x2/2N;Manual and Driver, Ink Bottle x6/6N', 26)

INSERT [dbo].[AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (12, N'HP', 27)

INSERT [dbo].[AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (14, N'58.7 x 39.4 x 20.8 cm', 28)

INSERT [dbo].[AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (13, N'6.53 KG', 29)

INSERT [dbo].[AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (17, N'310', 30)

INSERT [dbo].[AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (9, N'Color', 31)

INSERT [dbo].[AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (18, N'Regular', 32)

INSERT [dbo].[AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (18, N'Small', 33)

INSERT [dbo].[AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (19, N'40mm', 34)

INSERT [dbo].[AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (19, N'44mm', 35)

INSERT [dbo].[AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (15, N'GPS', 36)

INSERT [dbo].[AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (15, N'GPS + Cellular', 37)

INSERT [dbo].[AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (16, N'Case, Band, 1m Magnetic Charging Cable, 5W USB Power Adapter', 38)

INSERT [dbo].[AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (15, N'Wi-Fi 802.11b/g/n 2.4GHz, Bluetooth 5.0', 39)

INSERT [dbo].[AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (20, N'34mm', 40)

INSERT [dbo].[AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (21, N'10.7mm', 41)

INSERT [dbo].[AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (10, N'1 TB', 42)

INSERT [dbo].[AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (10, N'2 TB', 43)

INSERT [dbo].[AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (10, N'3 TB', 44)

INSERT [dbo].[AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (10, N'4 TB', 45)

INSERT [dbo].[AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (16, N'Portable hard drive, USB 3.0 cable, Quick install guide', 46)

INSERT [dbo].[AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (22, N'2018', 47)

INSERT [dbo].[AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (14, N'73.7 x 43.8 x 7.4 cm', 48)

INSERT [dbo].[AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (16, N'1 LED TV, 1 Table Top Stand, 1 User Manual, 1 Warranty Card, 1 Remote Control, 1 Power Cable / Power Supply Adopter', 49)

INSERT [dbo].[AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (2, N'32 Inches', 50)

INSERT [dbo].[AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (11, N'LED', 51)

INSERT [dbo].[AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (23, N'3 Devices', 52)

INSERT [dbo].[AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (23, N'5 Devices', 53)

INSERT [dbo].[AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (24, N'1 Year', 54)

INSERT [dbo].[AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (24, N'2 Years', 55)

INSERT [dbo].[AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (24, N'3 Years', 56)

INSERT [dbo].[AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (25, N'A', 57)

INSERT [dbo].[AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (25, N'B', 58)

INSERT [dbo].[AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (25, N'C', 59)

INSERT [dbo].[AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (9, N'Red', 60)

INSERT [dbo].[AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (9, N'Green', 61)

INSERT [dbo].[AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (9, N'Yello', 62)

INSERT [dbo].[AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (9, N'Orange', 63)

INSERT [dbo].[AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (9, N'Purple', 64)

INSERT [dbo].[AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (9, N'Magenta', 65)

SET IDENTITY_INSERT [dbo].[AvailableAttributeValue] OFF

SET IDENTITY_INSERT [dbo].[ProductAttributeValue] ON 

INSERT [dbo].[ProductAttributeValue] ([ProductAttributeId], [AvailableAttributeValueId], [Label], [Id]) VALUES (1, 7, NULL, 1)

INSERT [dbo].[ProductAttributeValue] ([ProductAttributeId], [AvailableAttributeValueId], [Label], [Id]) VALUES (1, 8, NULL, 2)

INSERT [dbo].[ProductAttributeValue] ([ProductAttributeId], [AvailableAttributeValueId], [Label], [Id]) VALUES (2, 10, NULL, 3)

INSERT [dbo].[ProductAttributeValue] ([ProductAttributeId], [AvailableAttributeValueId], [Label], [Id]) VALUES (2, 11, NULL, 4)

INSERT [dbo].[ProductAttributeValue] ([ProductAttributeId], [AvailableAttributeValueId], [Label], [Id]) VALUES (3, 12, NULL, 5)

INSERT [dbo].[ProductAttributeValue] ([ProductAttributeId], [AvailableAttributeValueId], [Label], [Id]) VALUES (4, 14, NULL, 6)

INSERT [dbo].[ProductAttributeValue] ([ProductAttributeId], [AvailableAttributeValueId], [Label], [Id]) VALUES (4, 15, NULL, 7)

INSERT [dbo].[ProductAttributeValue] ([ProductAttributeId], [AvailableAttributeValueId], [Label], [Id]) VALUES (5, 16, NULL, 8)

INSERT [dbo].[ProductAttributeValue] ([ProductAttributeId], [AvailableAttributeValueId], [Label], [Id]) VALUES (5, 17, NULL, 9)

INSERT [dbo].[ProductAttributeValue] ([ProductAttributeId], [AvailableAttributeValueId], [Label], [Id]) VALUES (6, 22, NULL, 10)

INSERT [dbo].[ProductAttributeValue] ([ProductAttributeId], [AvailableAttributeValueId], [Label], [Id]) VALUES (6, 31, NULL, 11)

INSERT [dbo].[ProductAttributeValue] ([ProductAttributeId], [AvailableAttributeValueId], [Label], [Id]) VALUES (7, 32, NULL, 12)

INSERT [dbo].[ProductAttributeValue] ([ProductAttributeId], [AvailableAttributeValueId], [Label], [Id]) VALUES (7, 33, NULL, 13)

INSERT [dbo].[ProductAttributeValue] ([ProductAttributeId], [AvailableAttributeValueId], [Label], [Id]) VALUES (8, 34, NULL, 14)

INSERT [dbo].[ProductAttributeValue] ([ProductAttributeId], [AvailableAttributeValueId], [Label], [Id]) VALUES (8, 35, NULL, 15)

INSERT [dbo].[ProductAttributeValue] ([ProductAttributeId], [AvailableAttributeValueId], [Label], [Id]) VALUES (9, 36, NULL, 16)

INSERT [dbo].[ProductAttributeValue] ([ProductAttributeId], [AvailableAttributeValueId], [Label], [Id]) VALUES (9, 37, NULL, 17)

INSERT [dbo].[ProductAttributeValue] ([ProductAttributeId], [AvailableAttributeValueId], [Label], [Id]) VALUES (10, 42, NULL, 18)

INSERT [dbo].[ProductAttributeValue] ([ProductAttributeId], [AvailableAttributeValueId], [Label], [Id]) VALUES (10, 43, NULL, 19)

INSERT [dbo].[ProductAttributeValue] ([ProductAttributeId], [AvailableAttributeValueId], [Label], [Id]) VALUES (10, 44, NULL, 20)

INSERT [dbo].[ProductAttributeValue] ([ProductAttributeId], [AvailableAttributeValueId], [Label], [Id]) VALUES (10, 45, NULL, 21)

INSERT [dbo].[ProductAttributeValue] ([ProductAttributeId], [AvailableAttributeValueId], [Label], [Id]) VALUES (11, 43, NULL, 22)

INSERT [dbo].[ProductAttributeValue] ([ProductAttributeId], [AvailableAttributeValueId], [Label], [Id]) VALUES (11, 44, NULL, 23)

INSERT [dbo].[ProductAttributeValue] ([ProductAttributeId], [AvailableAttributeValueId], [Label], [Id]) VALUES (12, 52, NULL, 24)

INSERT [dbo].[ProductAttributeValue] ([ProductAttributeId], [AvailableAttributeValueId], [Label], [Id]) VALUES (12, 53, NULL, 25)

INSERT [dbo].[ProductAttributeValue] ([ProductAttributeId], [AvailableAttributeValueId], [Label], [Id]) VALUES (13, 54, NULL, 26)

INSERT [dbo].[ProductAttributeValue] ([ProductAttributeId], [AvailableAttributeValueId], [Label], [Id]) VALUES (13, 55, NULL, 27)

INSERT [dbo].[ProductAttributeValue] ([ProductAttributeId], [AvailableAttributeValueId], [Label], [Id]) VALUES (13, 56, NULL, 28)

INSERT [dbo].[ProductAttributeValue] ([ProductAttributeId], [AvailableAttributeValueId], [Label], [Id]) VALUES (14, 21, NULL, 29)

INSERT [dbo].[ProductAttributeValue] ([ProductAttributeId], [AvailableAttributeValueId], [Label], [Id]) VALUES (14, 27, NULL, 30)

INSERT [dbo].[ProductAttributeValue] ([ProductAttributeId], [AvailableAttributeValueId], [Label], [Id]) VALUES (15, 34, NULL, 31)

INSERT [dbo].[ProductAttributeValue] ([ProductAttributeId], [AvailableAttributeValueId], [Label], [Id]) VALUES (15, 35, NULL, 32)

INSERT [dbo].[ProductAttributeValue] ([ProductAttributeId], [AvailableAttributeValueId], [Label], [Id]) VALUES (17, 22, NULL, 36)

INSERT [dbo].[ProductAttributeValue] ([ProductAttributeId], [AvailableAttributeValueId], [Label], [Id]) VALUES (17, 60, NULL, 37)

INSERT [dbo].[ProductAttributeValue] ([ProductAttributeId], [AvailableAttributeValueId], [Label], [Id]) VALUES (17, 61, NULL, 38)

INSERT [dbo].[ProductAttributeValue] ([ProductAttributeId], [AvailableAttributeValueId], [Label], [Id]) VALUES (17, 63, NULL, 40)

INSERT [dbo].[ProductAttributeValue] ([ProductAttributeId], [AvailableAttributeValueId], [Label], [Id]) VALUES (17, 64, NULL, 41)

INSERT [dbo].[ProductAttributeValue] ([ProductAttributeId], [AvailableAttributeValueId], [Label], [Id]) VALUES (17, 65, NULL, 42)

SET IDENTITY_INSERT [dbo].[ProductAttributeValue] OFF

SET IDENTITY_INSERT [dbo].[ProductVariantAttribute] ON 

INSERT [dbo].[ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (1, 1, 1, 1)

INSERT [dbo].[ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (2, 1, 2, 2)

INSERT [dbo].[ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (3, 2, 3, 3)

INSERT [dbo].[ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (4, 2, 4, 4)

INSERT [dbo].[ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (5, 2, 3, 5)

INSERT [dbo].[ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (5, 3, 5, 6)

INSERT [dbo].[ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (6, 4, 6, 7)

INSERT [dbo].[ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (6, 5, 8, 8)

INSERT [dbo].[ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (7, 4, 6, 9)

INSERT [dbo].[ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (7, 5, 9, 10)

INSERT [dbo].[ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (8, 4, 7, 11)

INSERT [dbo].[ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (8, 5, 8, 12)

INSERT [dbo].[ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (9, 4, 7, 13)

INSERT [dbo].[ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (9, 5, 9, 14)

INSERT [dbo].[ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (10, 6, 10, 15)

INSERT [dbo].[ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (10, 7, 12, 16)

INSERT [dbo].[ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (11, 6, 10, 17)

INSERT [dbo].[ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (11, 7, 13, 18)

INSERT [dbo].[ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (12, 6, 11, 19)

INSERT [dbo].[ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (12, 7, 12, 20)

INSERT [dbo].[ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (13, 6, 11, 21)

INSERT [dbo].[ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (13, 7, 13, 22)

INSERT [dbo].[ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (14, 8, 14, 23)

INSERT [dbo].[ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (14, 9, 16, 24)

INSERT [dbo].[ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (15, 8, 15, 25)

INSERT [dbo].[ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (15, 9, 16, 26)

INSERT [dbo].[ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (16, 8, 14, 27)

INSERT [dbo].[ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (16, 9, 17, 28)

INSERT [dbo].[ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (17, 8, 15, 29)

INSERT [dbo].[ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (17, 9, 17, 30)

INSERT [dbo].[ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (18, 10, 18, 31)

INSERT [dbo].[ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (19, 10, 19, 32)

INSERT [dbo].[ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (20, 10, 20, 33)

INSERT [dbo].[ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (21, 11, 22, 34)

INSERT [dbo].[ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (22, 11, 23, 35)

INSERT [dbo].[ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (23, 12, 24, 36)

INSERT [dbo].[ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (23, 13, 26, 37)

INSERT [dbo].[ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (24, 12, 24, 38)

INSERT [dbo].[ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (24, 13, 27, 39)

INSERT [dbo].[ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (25, 12, 24, 40)

INSERT [dbo].[ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (25, 13, 28, 41)

INSERT [dbo].[ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (26, 12, 25, 42)

INSERT [dbo].[ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (26, 13, 26, 43)

INSERT [dbo].[ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (27, 12, 25, 44)

INSERT [dbo].[ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (27, 13, 27, 45)

INSERT [dbo].[ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (28, 12, 25, 46)

INSERT [dbo].[ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (28, 13, 28, 47)

SET IDENTITY_INSERT [dbo].[ProductVariantAttribute] OFF

SET IDENTITY_INSERT [dbo].[Media] ON 

INSERT [dbo].[Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id], [MetaData]) VALUES (N'Image 1.jpg', N'Image 1.jpg', NULL, N'Image 1.jpg', N'/Content/Uploads/Serves/Image 1_0_0.jpg', N'/Content/Uploads/Serves/Image 1_150_150.jpg', N'image/jpeg', 0, CAST(N'2019-07-03T09:50:41.857' AS DateTime), 0, 1, 1, NULL)

INSERT [dbo].[Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id], [MetaData]) VALUES (N'Image 2.jpg', N'Image 2_WNTEYLJATT.jpg', NULL, N'Image 2.jpg', N'/Content/Uploads/Serves/Image 2_WNTEYLJATT_0_0.jpg', N'/Content/Uploads/Serves/Image 2_WNTEYLJATT_150_150.jpg', N'image/jpeg', 0, CAST(N'2019-07-03T10:00:18.413' AS DateTime), 0, 2, 6, NULL)

INSERT [dbo].[Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id], [MetaData]) VALUES (N'Image 3.jpg', N'Image 3.jpg', NULL, N'Image 3.jpg', N'/Content/Uploads/Serves/Image 3_0_0.jpg', N'/Content/Uploads/Serves/Image 3_150_150.jpg', N'image/jpeg', 0, CAST(N'2019-07-03T10:51:26.930' AS DateTime), 0, 0, 7, NULL)

INSERT [dbo].[Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id], [MetaData]) VALUES (N'Image 1.jpg', N'Image 1_TAODLLZOOB.jpg', NULL, N'Image 1.jpg', N'/Content/Uploads/Serves/Image 1_TAODLLZOOB_0_0.jpg', N'/Content/Uploads/Serves/Image 1_TAODLLZOOB_150_150.jpg', N'image/jpeg', 0, CAST(N'2019-07-03T12:04:41.773' AS DateTime), 0, 0, 8, NULL)

INSERT [dbo].[Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id], [MetaData]) VALUES (N'Image 2.jpg', N'Image 2_UNSJHCQYWE.jpg', NULL, N'Image 2.jpg', N'/Content/Uploads/Serves/Image 2_UNSJHCQYWE_0_0.jpg', N'/Content/Uploads/Serves/Image 2_UNSJHCQYWE_150_150.jpg', N'image/jpeg', 0, CAST(N'2019-07-03T12:04:47.057' AS DateTime), 0, 0, 9, NULL)

INSERT [dbo].[Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id], [MetaData]) VALUES (N'Image 3.jpg', N'Image 3_GUJSNHXUJX.jpg', NULL, N'Image 3.jpg', N'/Content/Uploads/Serves/Image 3_GUJSNHXUJX_0_0.jpg', N'/Content/Uploads/Serves/Image 3_GUJSNHXUJX_150_150.jpg', N'image/jpeg', 0, CAST(N'2019-07-03T12:04:51.373' AS DateTime), 0, 0, 10, NULL)

INSERT [dbo].[Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id], [MetaData]) VALUES (N'Image 4.jpg', N'Image 4.jpg', NULL, N'Image 4.jpg', N'/Content/Uploads/Serves/Image 4_0_0.jpg', N'/Content/Uploads/Serves/Image 4_150_150.jpg', N'image/jpeg', 0, CAST(N'2019-07-03T12:04:56.243' AS DateTime), 0, 0, 11, NULL)

INSERT [dbo].[Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id], [MetaData]) VALUES (N'Image04.jpeg', N'Image04.jpeg', NULL, N'Image04.jpeg', N'/Content/Uploads/Serves/Image04_0_0.jpeg', N'/Content/Uploads/Serves/Image04_150_150.jpeg', N'image/jpeg', 0, CAST(N'2019-07-03T14:07:38.047' AS DateTime), 0, 4, 12, NULL)

INSERT [dbo].[Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id], [MetaData]) VALUES (N'Image05.jpeg', N'Image05.jpeg', NULL, N'Image05.jpeg', N'/Content/Uploads/Serves/Image05_0_0.jpeg', N'/Content/Uploads/Serves/Image05_150_150.jpeg', N'image/jpeg', 0, CAST(N'2019-07-03T14:07:38.050' AS DateTime), 0, 3, 13, NULL)

INSERT [dbo].[Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id], [MetaData]) VALUES (N'Image03.jpeg', N'Image03.jpeg', NULL, N'Image03.jpeg', N'/Content/Uploads/Serves/Image03_0_0.jpeg', N'/Content/Uploads/Serves/Image03_150_150.jpeg', N'image/jpeg', 0, CAST(N'2019-07-03T14:07:38.050' AS DateTime), 0, 2, 14, NULL)

INSERT [dbo].[Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id], [MetaData]) VALUES (N'Image02.jpeg', N'Image02.jpeg', NULL, N'Image02.jpeg', N'/Content/Uploads/Serves/Image02_0_0.jpeg', N'/Content/Uploads/Serves/Image02_150_150.jpeg', N'image/jpeg', 0, CAST(N'2019-07-03T14:07:38.047' AS DateTime), 0, 1, 15, NULL)

INSERT [dbo].[Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id], [MetaData]) VALUES (N'Image01.jpeg', N'Image01.jpeg', NULL, N'Image01.jpeg', N'/Content/Uploads/Serves/Image01_0_0.jpeg', N'/Content/Uploads/Serves/Image01_150_150.jpeg', N'image/jpeg', 0, CAST(N'2019-07-03T14:07:38.050' AS DateTime), 0, 0, 16, NULL)

INSERT [dbo].[Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id], [MetaData]) VALUES (N'Image1.jpg', N'Image1.jpg', NULL, N'Image1.jpg', N'/Content/Uploads/Serves/Image1_0_0.jpg', N'/Content/Uploads/Serves/Image1_150_150.jpg', N'image/jpeg', 0, CAST(N'2019-07-04T07:31:02.417' AS DateTime), 0, 0, 17, NULL)

INSERT [dbo].[Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id], [MetaData]) VALUES (N'Image2.jpg', N'Image2.jpg', NULL, N'Image2.jpg', N'/Content/Uploads/Serves/Image2_0_0.jpg', N'/Content/Uploads/Serves/Image2_150_150.jpg', N'image/jpeg', 0, CAST(N'2019-07-04T07:31:02.417' AS DateTime), 0, 0, 18, NULL)

INSERT [dbo].[Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id], [MetaData]) VALUES (N'Image 5.jpg', N'Image 5.jpg', NULL, N'Image 5.jpg', N'/Content/Uploads/Serves/Image 5_0_0.jpg', N'/Content/Uploads/Serves/Image 5_150_150.jpg', N'image/jpeg', 0, CAST(N'2019-07-04T11:04:44.957' AS DateTime), 0, 2, 19, NULL)

INSERT [dbo].[Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id], [MetaData]) VALUES (N'Image 4.jpg', N'Image 4_CWWUEKKCXD.jpg', NULL, N'Image 4.jpg', N'/Content/Uploads/Serves/Image 4_CWWUEKKCXD_0_0.jpg', N'/Content/Uploads/Serves/Image 4_CWWUEKKCXD_150_150.jpg', N'image/jpeg', 0, CAST(N'2019-07-04T11:04:44.953' AS DateTime), 0, 0, 20, NULL)

INSERT [dbo].[Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id], [MetaData]) VALUES (N'Image 3.jpg', N'Image 3_WLRANZVQMZ.jpg', NULL, N'Image 3.jpg', N'/Content/Uploads/Serves/Image 3_WLRANZVQMZ_0_0.jpg', N'/Content/Uploads/Serves/Image 3_WLRANZVQMZ_150_150.jpg', N'image/jpeg', 0, CAST(N'2019-07-04T11:04:44.957' AS DateTime), 0, 1, 21, NULL)

INSERT [dbo].[Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id], [MetaData]) VALUES (N'Image 1.jpg', N'Image 1_LTWAQZCKAW.jpg', NULL, N'Image 1.jpg', N'/Content/Uploads/Serves/Image 1_LTWAQZCKAW_0_0.jpg', N'/Content/Uploads/Serves/Image 1_LTWAQZCKAW_150_150.jpg', N'image/jpeg', 0, CAST(N'2019-07-04T11:04:44.953' AS DateTime), 0, 3, 22, NULL)

INSERT [dbo].[Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id], [MetaData]) VALUES (N'Image 2.jpg', N'Image 2_DEYNQMKCRV.jpg', NULL, N'Image 2.jpg', N'/Content/Uploads/Serves/Image 2_DEYNQMKCRV_0_0.jpg', N'/Content/Uploads/Serves/Image 2_DEYNQMKCRV_150_150.jpg', N'image/jpeg', 0, CAST(N'2019-07-04T11:04:44.953' AS DateTime), 0, 4, 23, NULL)

INSERT [dbo].[Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id], [MetaData]) VALUES (N'Image 6.jpg', N'Image 6.jpg', NULL, N'Image 6.jpg', N'/Content/Uploads/Serves/Image 6_0_0.jpg', N'/Content/Uploads/Serves/Image 6_150_150.jpg', N'image/jpeg', 0, CAST(N'2019-07-04T11:04:44.957' AS DateTime), 0, 5, 24, NULL)

INSERT [dbo].[Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id], [MetaData]) VALUES (N'Image 1.jpg', N'Image 1_JVYSWCDWSU.jpg', NULL, N'Image 1.jpg', N'/Content/Uploads/Serves/Image 1_JVYSWCDWSU_0_0.jpg', N'/Content/Uploads/Serves/Image 1_JVYSWCDWSU_150_150.jpg', N'image/jpeg', 0, CAST(N'2019-07-04T12:00:56.773' AS DateTime), 0, 0, 25, NULL)

INSERT [dbo].[Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id], [MetaData]) VALUES (N'Image2.jpg', N'Image2_YDYWECTEVO.jpg', NULL, N'Image2.jpg', N'/Content/Uploads/Serves/Image2_YDYWECTEVO_0_0.jpg', N'/Content/Uploads/Serves/Image2_YDYWECTEVO_150_150.jpg', N'image/jpeg', 0, CAST(N'2019-07-04T12:22:26.557' AS DateTime), 0, 1, 26, NULL)

INSERT [dbo].[Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id], [MetaData]) VALUES (N'Image1.jpg', N'Image1_DWABHQZFMK.jpg', NULL, N'Image1.jpg', N'/Content/Uploads/Serves/Image1_DWABHQZFMK_0_0.jpg', N'/Content/Uploads/Serves/Image1_DWABHQZFMK_150_150.jpg', N'image/jpeg', 0, CAST(N'2019-07-04T12:22:26.590' AS DateTime), 0, 0, 27, NULL)

INSERT [dbo].[Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id], [MetaData]) VALUES (N'Image 2.jpg', N'Image 2_DLFJYPAIVH.jpg', NULL, N'Image 2.jpg', N'/Content/Uploads/Serves/Image 2_DLFJYPAIVH_0_0.jpg', N'/Content/Uploads/Serves/Image 2_DLFJYPAIVH_150_150.jpg', N'image/jpeg', 0, CAST(N'2019-07-04T12:49:22.970' AS DateTime), 0, 1, 28, NULL)

INSERT [dbo].[Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id], [MetaData]) VALUES (N'Image 1.jpg', N'Image 1_MWFFVCYQSU.jpg', NULL, N'Image 1.jpg', N'/Content/Uploads/Serves/Image 1_MWFFVCYQSU_0_0.jpg', N'/Content/Uploads/Serves/Image 1_MWFFVCYQSU_150_150.jpg', N'image/jpeg', 0, CAST(N'2019-07-04T12:49:22.973' AS DateTime), 0, 0, 29, NULL)

INSERT [dbo].[Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id], [MetaData]) VALUES (N'Image 1.jpg', N'Image 1_LSFCSKSPAD.jpg', NULL, N'Image 1.jpg', N'/Content/Uploads/Serves/Image 1_LSFCSKSPAD_0_0.jpg', N'/Content/Uploads/Serves/Image 1_LSFCSKSPAD_150_150.jpg', N'image/jpeg', 0, CAST(N'2019-07-04T13:32:18.807' AS DateTime), 0, 1, 30, NULL)

INSERT [dbo].[Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id], [MetaData]) VALUES (N'Image 2.jpg', N'Image 2_YAMQBGYQZM.jpg', NULL, N'Image 2.jpg', N'/Content/Uploads/Serves/Image 2_YAMQBGYQZM_0_0.jpg', N'/Content/Uploads/Serves/Image 2_YAMQBGYQZM_150_150.jpg', N'image/jpeg', 0, CAST(N'2019-07-04T13:32:18.803' AS DateTime), 0, 0, 31, NULL)

INSERT [dbo].[Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id], [MetaData]) VALUES (N'Image 5.jpg', N'Image 5_BHLFRSXROL.jpg', NULL, N'Image 5.jpg', N'/Content/Uploads/Serves/Image 5_BHLFRSXROL_0_0.jpg', N'/Content/Uploads/Serves/Image 5_BHLFRSXROL_150_150.jpg', N'image/jpeg', 0, CAST(N'2019-07-04T14:36:08.053' AS DateTime), 0, 4, 32, NULL)

INSERT [dbo].[Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id], [MetaData]) VALUES (N'Image 6.jpg', N'Image 6_JTGUVSJLPZ.jpg', NULL, N'Image 6.jpg', N'/Content/Uploads/Serves/Image 6_JTGUVSJLPZ_0_0.jpg', N'/Content/Uploads/Serves/Image 6_JTGUVSJLPZ_150_150.jpg', N'image/jpeg', 0, CAST(N'2019-07-04T14:36:08.020' AS DateTime), 0, 5, 33, NULL)

INSERT [dbo].[Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id], [MetaData]) VALUES (N'Image 1.jpg', N'Image 1_ANVRPZEXAF.jpg', NULL, N'Image 1.jpg', N'/Content/Uploads/Serves/Image 1_ANVRPZEXAF_0_0.jpg', N'/Content/Uploads/Serves/Image 1_ANVRPZEXAF_150_150.jpg', N'image/jpeg', 0, CAST(N'2019-07-04T14:36:08.070' AS DateTime), 0, 0, 34, NULL)

INSERT [dbo].[Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id], [MetaData]) VALUES (N'Image 4.jpg', N'Image 4_RBXJYKGZJZ.jpg', NULL, N'Image 4.jpg', N'/Content/Uploads/Serves/Image 4_RBXJYKGZJZ_0_0.jpg', N'/Content/Uploads/Serves/Image 4_RBXJYKGZJZ_150_150.jpg', N'image/jpeg', 0, CAST(N'2019-07-04T14:36:08.107' AS DateTime), 0, 2, 35, NULL)

INSERT [dbo].[Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id], [MetaData]) VALUES (N'Image 2.jpg', N'Image 2_AHTRZQOGGE.jpg', NULL, N'Image 2.jpg', N'/Content/Uploads/Serves/Image 2_AHTRZQOGGE_0_0.jpg', N'/Content/Uploads/Serves/Image 2_AHTRZQOGGE_150_150.jpg', N'image/jpeg', 0, CAST(N'2019-07-04T14:36:08.107' AS DateTime), 0, 3, 36, NULL)

INSERT [dbo].[Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id], [MetaData]) VALUES (N'Image 3.jpg', N'Image 3_PFCHRAQZJK.jpg', NULL, N'Image 3.jpg', N'/Content/Uploads/Serves/Image 3_PFCHRAQZJK_0_0.jpg', N'/Content/Uploads/Serves/Image 3_PFCHRAQZJK_150_150.jpg', N'image/jpeg', 0, CAST(N'2019-07-04T14:36:08.170' AS DateTime), 0, 1, 37, NULL)

INSERT [dbo].[Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id], [MetaData]) VALUES (N'Image 1.jpg', N'Image 1_LNREOZMAVI.jpg', NULL, N'Image 1.jpg', N'/Content/Uploads/Serves/Image 1_LNREOZMAVI_0_0.jpg', N'/Content/Uploads/Serves/Image 1_LNREOZMAVI_150_150.jpg', N'image/jpeg', 0, CAST(N'2019-07-04T14:48:13.127' AS DateTime), 0, 0, 38, NULL)

INSERT [dbo].[Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id], [MetaData]) VALUES (N'Image 2.jpg', N'Image 2_EZHSXFNTUE.jpg', NULL, N'Image 2.jpg', N'/Content/Uploads/Serves/Image 2_EZHSXFNTUE_0_0.jpg', N'/Content/Uploads/Serves/Image 2_EZHSXFNTUE_150_150.jpg', N'image/jpeg', 0, CAST(N'2019-07-04T14:48:13.107' AS DateTime), 0, 1, 39, NULL)

INSERT [dbo].[Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id], [MetaData]) VALUES (N'Image 4.jpg', N'Image 4_GMXPEIMIUM.jpg', NULL, N'Image 4.jpg', N'/Content/Uploads/Serves/Image 4_GMXPEIMIUM_0_0.jpg', N'/Content/Uploads/Serves/Image 4_GMXPEIMIUM_150_150.jpg', N'image/jpeg', 0, CAST(N'2019-07-04T14:48:13.113' AS DateTime), 0, 3, 40, NULL)

INSERT [dbo].[Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id], [MetaData]) VALUES (N'Image 3.jpg', N'Image 3_GEPCOYSQCI.jpg', NULL, N'Image 3.jpg', N'/Content/Uploads/Serves/Image 3_GEPCOYSQCI_0_0.jpg', N'/Content/Uploads/Serves/Image 3_GEPCOYSQCI_150_150.jpg', N'image/jpeg', 0, CAST(N'2019-07-04T14:48:13.123' AS DateTime), 0, 2, 41, NULL)

INSERT [dbo].[Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id], [MetaData]) VALUES (N'Image 1.png', N'Image 1.png', NULL, N'Image 1.png', N'/Content/Uploads/Serves/Image 1_0_0.png', N'/Content/Uploads/Serves/Image 1_150_150.png', N'image/png', 0, CAST(N'2019-07-05T07:53:12.017' AS DateTime), 0, 0, 42, NULL)

INSERT [dbo].[Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id], [MetaData]) VALUES (N'Image 4.png', N'Image 4.png', NULL, N'Image 4.png', N'/Content/Uploads/Serves/Image 4_0_0.png', N'/Content/Uploads/Serves/Image 4_150_150.png', N'image/png', 0, CAST(N'2019-07-05T07:53:12.017' AS DateTime), 0, 0, 43, NULL)

INSERT [dbo].[Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id], [MetaData]) VALUES (N'Image 2.png', N'Image 2.png', NULL, N'Image 2.png', N'/Content/Uploads/Serves/Image 2_0_0.png', N'/Content/Uploads/Serves/Image 2_150_150.png', N'image/png', 0, CAST(N'2019-07-05T07:53:12.017' AS DateTime), 0, 0, 44, NULL)

INSERT [dbo].[Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id], [MetaData]) VALUES (N'Image 3.png', N'Image 3.png', NULL, N'Image 3.png', N'/Content/Uploads/Serves/Image 3_0_0.png', N'/Content/Uploads/Serves/Image 3_150_150.png', N'image/png', 0, CAST(N'2019-07-05T07:53:12.017' AS DateTime), 0, 0, 45, NULL)

INSERT [dbo].[Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id], [MetaData]) VALUES (N'Image 3.jpg', N'Image 3_ROYYRYTFLW.jpg', NULL, N'Image 3.jpg', N'/Content/Uploads/Serves/Image 3_ROYYRYTFLW_0_0.jpg', N'/Content/Uploads/Serves/Image 3_ROYYRYTFLW_150_150.jpg', N'image/jpeg', 0, CAST(N'2019-07-05T08:16:06.217' AS DateTime), 0, 3, 46, NULL)

INSERT [dbo].[Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id], [MetaData]) VALUES (N'Image 2.jpg', N'Image 2_ADBKHWFCRY.jpg', NULL, N'Image 2.jpg', N'/Content/Uploads/Serves/Image 2_ADBKHWFCRY_0_0.jpg', N'/Content/Uploads/Serves/Image 2_ADBKHWFCRY_150_150.jpg', N'image/jpeg', 0, CAST(N'2019-07-05T08:16:06.217' AS DateTime), 0, 7, 47, NULL)

INSERT [dbo].[Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id], [MetaData]) VALUES (N'Image 4.jpg', N'Image 4_TGGFMREMMI.jpg', NULL, N'Image 4.jpg', N'/Content/Uploads/Serves/Image 4_TGGFMREMMI_0_0.jpg', N'/Content/Uploads/Serves/Image 4_TGGFMREMMI_150_150.jpg', N'image/jpeg', 0, CAST(N'2019-07-05T08:16:06.227' AS DateTime), 0, 4, 48, NULL)

INSERT [dbo].[Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id], [MetaData]) VALUES (N'Image 5.jpg', N'Image 5_NWKBRKNEWR.jpg', NULL, N'Image 5.jpg', N'/Content/Uploads/Serves/Image 5_NWKBRKNEWR_0_0.jpg', N'/Content/Uploads/Serves/Image 5_NWKBRKNEWR_150_150.jpg', N'image/jpeg', 0, CAST(N'2019-07-05T08:16:06.223' AS DateTime), 0, 6, 49, NULL)

INSERT [dbo].[Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id], [MetaData]) VALUES (N'Image 6.jpg', N'Image 6_YXZNHQQNOQ.jpg', NULL, N'Image 6.jpg', N'/Content/Uploads/Serves/Image 6_YXZNHQQNOQ_0_0.jpg', N'/Content/Uploads/Serves/Image 6_YXZNHQQNOQ_150_150.jpg', N'image/jpeg', 0, CAST(N'2019-07-05T08:16:06.223' AS DateTime), 0, 5, 50, NULL)

INSERT [dbo].[Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id], [MetaData]) VALUES (N'Image 1.jpg', N'Image 1_TCPXDHBZZM.jpg', NULL, N'Image 1.jpg', N'/Content/Uploads/Serves/Image 1_TCPXDHBZZM_0_0.jpg', N'/Content/Uploads/Serves/Image 1_TCPXDHBZZM_150_150.jpg', N'image/jpeg', 0, CAST(N'2019-07-05T08:16:06.223' AS DateTime), 0, 0, 51, NULL)

INSERT [dbo].[Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id], [MetaData]) VALUES (N'Slide-1.png', N'Slide-1.png', NULL, N'Slide-1.png', N'/Content/Uploads/Serves/Slide-1_0_0.png', N'/Content/Uploads/Serves/Slide-1_150_150.png', N'image/png', 0, CAST(N'2019-07-06T10:14:20.093' AS DateTime), 0, 0, 56, NULL)

INSERT [dbo].[Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id], [MetaData]) VALUES (N'Slide-2.png', N'Slide-2.png', NULL, N'Slide-2.png', N'/Content/Uploads/Serves/Slide-2_0_0.png', N'/Content/Uploads/Serves/Slide-2_150_150.png', N'image/png', 0, CAST(N'2019-07-06T10:40:57.803' AS DateTime), 0, 0, 57, NULL)

INSERT [dbo].[Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id], [MetaData]) VALUES (N'logo.png', N'logo.png', NULL, N'logo.png', N'/Content/Uploads/Serves/logo_0_0.png', N'/Content/Uploads/Serves/logo_150_150.png', N'image/png', 0, CAST(N'2019-11-11T12:15:00.530' AS DateTime), 0, 0, 58, NULL)

INSERT [dbo].[Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id], [MetaData]) VALUES (N'logo.png', N'logo_HPGJIHSFBW.png', NULL, N'logo.png', N'/Content/Uploads/Serves/logo_HPGJIHSFBW_0_0.png', N'/Content/Uploads/Serves/logo_HPGJIHSFBW_150_150.png', N'image/png', 0, CAST(N'2019-11-11T12:15:32.230' AS DateTime), 0, 0, 59, NULL)

INSERT [dbo].[Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id], [MetaData]) VALUES (NULL, NULL, NULL, NULL, N'https://www.youtube.com/watch?v=tbputEIVPQ4', NULL, NULL, 7, CAST(N'2020-03-26T12:53:08.227' AS DateTime), 0, 0, 70, N'{"url":"https://www.youtube.com/watch?v=tbputEIVPQ4","thumbnailUrl":"https://i.ytimg.com/vi/tbputEIVPQ4/hqdefault.jpg","html":"\n<iframe width=\" 480\" height=\"270\" src=\"https://www.youtube.com/embed/tbputEIVPQ4?feature=oembed\" frameborder=\"0\" allowfullscreen=\"allowfullscreen\"></iframe>\n","title":"14. Reading Input from Keyboard - Build 2048 puzzle game in Unity 3D","author":"Dev Tutorials","providerName":"YouTube"}')

INSERT [dbo].[Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id], [MetaData]) VALUES (NULL, NULL, NULL, NULL, N'https://www.youtube.com/watch?v=YplAU5myNP4', NULL, NULL, 7, CAST(N'2020-03-26T13:53:31.613' AS DateTime), 0, 2, 72, N'{"url":"https://www.youtube.com/watch?v=YplAU5myNP4","thumbnailUrl":"https://i.ytimg.com/vi/YplAU5myNP4/hqdefault.jpg","html":"\n<iframe width=\" 480\" height=\"270\" src=\"https://www.youtube.com/embed/YplAU5myNP4?feature=oembed\" frameborder=\"0\" allowfullscreen=\"allowfullscreen\"></iframe>\n","title":"Meet the new icons for Office 365","author":"Microsoft Office 365","providerName":"YouTube"}')

INSERT [dbo].[Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id], [MetaData]) VALUES (NULL, NULL, NULL, NULL, N'https://soundcloud.com/philanthropicguy/toota-jo-kabhi-tara', NULL, NULL, 7, CAST(N'2020-03-26T14:10:24.417' AS DateTime), 0, 0, 73, N'{"url":"https://soundcloud.com/philanthropicguy/toota-jo-kabhi-tara","thumbnailUrl":"https://i1.sndcdn.com/artworks-000282572213-htwcul-t500x500.jpg","html":"<iframe width=\"100%\" height=\"400\" scrolling=\"no\" frameborder=\"no\" src=\"https://w.soundcloud.com/player/?visual=true&url=https%3A%2F%2Fapi.soundcloud.com%2Ftracks%2F279335697&show_artwork=true\"></iframe>","title":"Toota Jo Kabhi Tara by Philanthropic Guy","author":"Philanthropic Guy","providerName":"SoundCloud"}')

INSERT [dbo].[Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id], [MetaData]) VALUES (NULL, NULL, NULL, NULL, N'https://soundcloud.com/philanthropicguy/toota-jo-kabhi-tara', NULL, NULL, 7, CAST(N'2020-03-26T14:27:53.020' AS DateTime), 0, 0, 77, N'{"url":"https://soundcloud.com/philanthropicguy/toota-jo-kabhi-tara","thumbnailUrl":"https://i1.sndcdn.com/artworks-000282572213-htwcul-t500x500.jpg","html":"<iframe width=\"100%\" height=\"400\" scrolling=\"no\" frameborder=\"no\" src=\"https://w.soundcloud.com/player/?visual=true&url=https%3A%2F%2Fapi.soundcloud.com%2Ftracks%2F279335697&show_artwork=true\"></iframe>","title":"Toota Jo Kabhi Tara by Philanthropic Guy","author":"Philanthropic Guy","providerName":"SoundCloud"}')

INSERT [dbo].[Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id], [MetaData]) VALUES (NULL, NULL, NULL, NULL, N'https://www.youtube.com/watch?v=tbputEIVPQ4', NULL, NULL, 7, CAST(N'2020-03-26T14:29:47.997' AS DateTime), 0, 0, 78, N'{"url":"https://www.youtube.com/watch?v=tbputEIVPQ4","thumbnailUrl":"https://i.ytimg.com/vi/tbputEIVPQ4/hqdefault.jpg","html":"\n<iframe width=\" 480\" height=\"270\" src=\"https://www.youtube.com/embed/tbputEIVPQ4?feature=oembed\" frameborder=\"0\" allowfullscreen=\"allowfullscreen\"></iframe>\n","title":"14. Reading Input from Keyboard - Build 2048 puzzle game in Unity 3D","author":"Dev Tutorials","providerName":"YouTube"}')

SET IDENTITY_INSERT [dbo].[Media] OFF

CREATE TABLE [dbo].[UiSlider](
	[Title] [nvarchar](max) NULL,
	[MediaId] [int] NOT NULL,
	[DisplayOrder] [int] NOT NULL,
	[Visible] [bit] NOT NULL,
	[Url] [nvarchar](max) NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


ALTER TABLE [dbo].[UiSlider]  WITH CHECK ADD  CONSTRAINT [FK_Media_Id_UiSlider_MediaId] FOREIGN KEY([MediaId])
REFERENCES [dbo].[Media] ([Id])
ON DELETE CASCADE

ALTER TABLE [dbo].[UiSlider] CHECK CONSTRAINT [FK_Media_Id_UiSlider_MediaId]

SET IDENTITY_INSERT [dbo].[UiSlider] ON 

INSERT [dbo].[UiSlider] ([Title], [MediaId], [DisplayOrder], [Visible], [Url], [Id]) VALUES (N'iPhone X', 56, 2, 1, NULL, 3)

INSERT [dbo].[UiSlider] ([Title], [MediaId], [DisplayOrder], [Visible], [Url], [Id]) VALUES (N'HP Laptop', 57, 2, 1, NULL, 4)

SET IDENTITY_INSERT [dbo].[UiSlider] OFF

SET IDENTITY_INSERT [dbo].[ProductMedia] ON 

INSERT [dbo].[ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (2, 1, 1)

INSERT [dbo].[ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (2, 6, 6)

INSERT [dbo].[ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (2, 7, 7)

INSERT [dbo].[ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (3, 8, 8)

INSERT [dbo].[ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (3, 9, 9)

INSERT [dbo].[ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (3, 10, 10)

INSERT [dbo].[ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (3, 11, 11)

INSERT [dbo].[ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (4, 13, 12)

INSERT [dbo].[ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (4, 12, 13)

INSERT [dbo].[ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (4, 14, 14)

INSERT [dbo].[ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (4, 15, 15)

INSERT [dbo].[ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (4, 16, 16)

INSERT [dbo].[ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (5, 17, 17)

INSERT [dbo].[ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (5, 18, 18)

INSERT [dbo].[ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (6, 20, 19)

INSERT [dbo].[ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (6, 21, 20)

INSERT [dbo].[ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (6, 19, 21)

INSERT [dbo].[ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (6, 22, 22)

INSERT [dbo].[ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (6, 23, 23)

INSERT [dbo].[ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (6, 24, 24)

INSERT [dbo].[ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (7, 25, 25)

INSERT [dbo].[ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (8, 26, 26)

INSERT [dbo].[ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (8, 27, 27)

INSERT [dbo].[ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (9, 28, 28)

INSERT [dbo].[ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (9, 29, 29)

INSERT [dbo].[ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (10, 31, 30)

INSERT [dbo].[ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (10, 30, 31)

INSERT [dbo].[ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (11, 32, 32)

INSERT [dbo].[ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (11, 33, 33)

INSERT [dbo].[ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (11, 34, 34)

INSERT [dbo].[ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (11, 35, 35)

INSERT [dbo].[ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (11, 36, 36)

INSERT [dbo].[ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (11, 37, 37)

INSERT [dbo].[ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (12, 38, 38)

INSERT [dbo].[ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (12, 39, 39)

INSERT [dbo].[ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (12, 41, 40)

INSERT [dbo].[ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (12, 40, 41)

INSERT [dbo].[ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (13, 42, 42)

INSERT [dbo].[ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (13, 43, 43)

INSERT [dbo].[ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (13, 44, 44)

INSERT [dbo].[ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (13, 45, 45)

INSERT [dbo].[ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (14, 46, 46)

INSERT [dbo].[ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (14, 47, 47)

INSERT [dbo].[ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (14, 48, 48)

INSERT [dbo].[ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (14, 49, 49)

INSERT [dbo].[ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (14, 50, 50)

INSERT [dbo].[ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (14, 51, 51)

INSERT [dbo].[ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (16, 70, 59)

INSERT [dbo].[ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (14, 72, 61)

INSERT [dbo].[ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (14, 73, 62)

INSERT [dbo].[ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (16, 77, 66)

INSERT [dbo].[ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (16, 78, 67)

SET IDENTITY_INSERT [dbo].[ProductMedia] OFF

SET IDENTITY_INSERT [dbo].[ProductSpecification] ON 

INSERT [dbo].[ProductSpecification] ([ProductId], [AvailableAttributeId], [Label], [DisplayOrder], [ProductSpecificationGroupId], [IsVisible], [IsFilterable], [Id]) VALUES (2, 1, NULL, 3, 1, 1, 1, 1)

INSERT [dbo].[ProductSpecification] ([ProductId], [AvailableAttributeId], [Label], [DisplayOrder], [ProductSpecificationGroupId], [IsVisible], [IsFilterable], [Id]) VALUES (2, 2, NULL, 0, 1, 1, 0, 2)

INSERT [dbo].[ProductSpecification] ([ProductId], [AvailableAttributeId], [Label], [DisplayOrder], [ProductSpecificationGroupId], [IsVisible], [IsFilterable], [Id]) VALUES (2, 3, NULL, 1, 1, 1, 0, 3)

INSERT [dbo].[ProductSpecification] ([ProductId], [AvailableAttributeId], [Label], [DisplayOrder], [ProductSpecificationGroupId], [IsVisible], [IsFilterable], [Id]) VALUES (2, 4, NULL, 2, 1, 1, 1, 4)

INSERT [dbo].[ProductSpecification] ([ProductId], [AvailableAttributeId], [Label], [DisplayOrder], [ProductSpecificationGroupId], [IsVisible], [IsFilterable], [Id]) VALUES (2, 5, NULL, 5, 1, 1, 1, 5)

INSERT [dbo].[ProductSpecification] ([ProductId], [AvailableAttributeId], [Label], [DisplayOrder], [ProductSpecificationGroupId], [IsVisible], [IsFilterable], [Id]) VALUES (2, 6, NULL, 4, 1, 1, 0, 6)

INSERT [dbo].[ProductSpecification] ([ProductId], [AvailableAttributeId], [Label], [DisplayOrder], [ProductSpecificationGroupId], [IsVisible], [IsFilterable], [Id]) VALUES (3, 5, NULL, 0, 0, 1, 1, 7)

INSERT [dbo].[ProductSpecification] ([ProductId], [AvailableAttributeId], [Label], [DisplayOrder], [ProductSpecificationGroupId], [IsVisible], [IsFilterable], [Id]) VALUES (3, 7, NULL, 0, 0, 1, 1, 8)

INSERT [dbo].[ProductSpecification] ([ProductId], [AvailableAttributeId], [Label], [DisplayOrder], [ProductSpecificationGroupId], [IsVisible], [IsFilterable], [Id]) VALUES (3, 4, NULL, 0, 0, 1, 1, 9)

INSERT [dbo].[ProductSpecification] ([ProductId], [AvailableAttributeId], [Label], [DisplayOrder], [ProductSpecificationGroupId], [IsVisible], [IsFilterable], [Id]) VALUES (3, 1, N'Available Processors', 0, 0, 1, 1, 10)

INSERT [dbo].[ProductSpecification] ([ProductId], [AvailableAttributeId], [Label], [DisplayOrder], [ProductSpecificationGroupId], [IsVisible], [IsFilterable], [Id]) VALUES (4, 9, NULL, 0, 0, 1, 1, 11)

INSERT [dbo].[ProductSpecification] ([ProductId], [AvailableAttributeId], [Label], [DisplayOrder], [ProductSpecificationGroupId], [IsVisible], [IsFilterable], [Id]) VALUES (4, 10, NULL, 0, 0, 1, 1, 12)

INSERT [dbo].[ProductSpecification] ([ProductId], [AvailableAttributeId], [Label], [DisplayOrder], [ProductSpecificationGroupId], [IsVisible], [IsFilterable], [Id]) VALUES (4, 2, NULL, 0, 0, 1, 0, 13)

INSERT [dbo].[ProductSpecification] ([ProductId], [AvailableAttributeId], [Label], [DisplayOrder], [ProductSpecificationGroupId], [IsVisible], [IsFilterable], [Id]) VALUES (4, 3, NULL, 0, 0, 1, 0, 14)

INSERT [dbo].[ProductSpecification] ([ProductId], [AvailableAttributeId], [Label], [DisplayOrder], [ProductSpecificationGroupId], [IsVisible], [IsFilterable], [Id]) VALUES (4, 11, NULL, 0, 0, 1, 0, 15)

INSERT [dbo].[ProductSpecification] ([ProductId], [AvailableAttributeId], [Label], [DisplayOrder], [ProductSpecificationGroupId], [IsVisible], [IsFilterable], [Id]) VALUES (5, 12, NULL, 0, 0, 1, 1, 16)

INSERT [dbo].[ProductSpecification] ([ProductId], [AvailableAttributeId], [Label], [DisplayOrder], [ProductSpecificationGroupId], [IsVisible], [IsFilterable], [Id]) VALUES (5, 9, NULL, 0, 0, 1, 1, 17)

INSERT [dbo].[ProductSpecification] ([ProductId], [AvailableAttributeId], [Label], [DisplayOrder], [ProductSpecificationGroupId], [IsVisible], [IsFilterable], [Id]) VALUES (5, 13, NULL, 0, 0, 1, 0, 18)

INSERT [dbo].[ProductSpecification] ([ProductId], [AvailableAttributeId], [Label], [DisplayOrder], [ProductSpecificationGroupId], [IsVisible], [IsFilterable], [Id]) VALUES (5, 14, NULL, 0, 0, 1, 0, 19)

INSERT [dbo].[ProductSpecification] ([ProductId], [AvailableAttributeId], [Label], [DisplayOrder], [ProductSpecificationGroupId], [IsVisible], [IsFilterable], [Id]) VALUES (5, 15, NULL, 0, 0, 1, 1, 20)

INSERT [dbo].[ProductSpecification] ([ProductId], [AvailableAttributeId], [Label], [DisplayOrder], [ProductSpecificationGroupId], [IsVisible], [IsFilterable], [Id]) VALUES (5, 16, NULL, 0, 0, 1, 0, 21)

INSERT [dbo].[ProductSpecification] ([ProductId], [AvailableAttributeId], [Label], [DisplayOrder], [ProductSpecificationGroupId], [IsVisible], [IsFilterable], [Id]) VALUES (6, 12, NULL, 1, 0, 1, 1, 22)

INSERT [dbo].[ProductSpecification] ([ProductId], [AvailableAttributeId], [Label], [DisplayOrder], [ProductSpecificationGroupId], [IsVisible], [IsFilterable], [Id]) VALUES (6, 14, NULL, 2, 0, 1, 0, 23)

INSERT [dbo].[ProductSpecification] ([ProductId], [AvailableAttributeId], [Label], [DisplayOrder], [ProductSpecificationGroupId], [IsVisible], [IsFilterable], [Id]) VALUES (6, 13, NULL, 3, 0, 1, 0, 24)

INSERT [dbo].[ProductSpecification] ([ProductId], [AvailableAttributeId], [Label], [DisplayOrder], [ProductSpecificationGroupId], [IsVisible], [IsFilterable], [Id]) VALUES (6, 17, NULL, 0, 0, 1, 0, 25)

INSERT [dbo].[ProductSpecification] ([ProductId], [AvailableAttributeId], [Label], [DisplayOrder], [ProductSpecificationGroupId], [IsVisible], [IsFilterable], [Id]) VALUES (8, 19, NULL, 0, 0, 1, 1, 26)

INSERT [dbo].[ProductSpecification] ([ProductId], [AvailableAttributeId], [Label], [DisplayOrder], [ProductSpecificationGroupId], [IsVisible], [IsFilterable], [Id]) VALUES (8, 16, NULL, 0, 0, 1, 0, 27)

INSERT [dbo].[ProductSpecification] ([ProductId], [AvailableAttributeId], [Label], [DisplayOrder], [ProductSpecificationGroupId], [IsVisible], [IsFilterable], [Id]) VALUES (8, 15, NULL, 0, 0, 1, 0, 28)

INSERT [dbo].[ProductSpecification] ([ProductId], [AvailableAttributeId], [Label], [DisplayOrder], [ProductSpecificationGroupId], [IsVisible], [IsFilterable], [Id]) VALUES (8, 20, NULL, 0, 0, 1, 0, 29)

INSERT [dbo].[ProductSpecification] ([ProductId], [AvailableAttributeId], [Label], [DisplayOrder], [ProductSpecificationGroupId], [IsVisible], [IsFilterable], [Id]) VALUES (8, 21, NULL, 0, 0, 1, 0, 30)

INSERT [dbo].[ProductSpecification] ([ProductId], [AvailableAttributeId], [Label], [DisplayOrder], [ProductSpecificationGroupId], [IsVisible], [IsFilterable], [Id]) VALUES (9, 10, N'Available Storage', 0, 0, 1, 1, 31)

INSERT [dbo].[ProductSpecification] ([ProductId], [AvailableAttributeId], [Label], [DisplayOrder], [ProductSpecificationGroupId], [IsVisible], [IsFilterable], [Id]) VALUES (9, 16, NULL, 0, 0, 1, 0, 32)

INSERT [dbo].[ProductSpecification] ([ProductId], [AvailableAttributeId], [Label], [DisplayOrder], [ProductSpecificationGroupId], [IsVisible], [IsFilterable], [Id]) VALUES (7, 12, NULL, 0, 0, 1, 1, 33)

INSERT [dbo].[ProductSpecification] ([ProductId], [AvailableAttributeId], [Label], [DisplayOrder], [ProductSpecificationGroupId], [IsVisible], [IsFilterable], [Id]) VALUES (11, 22, NULL, 0, 0, 1, 1, 34)

INSERT [dbo].[ProductSpecification] ([ProductId], [AvailableAttributeId], [Label], [DisplayOrder], [ProductSpecificationGroupId], [IsVisible], [IsFilterable], [Id]) VALUES (11, 14, NULL, 0, 0, 1, 0, 35)

INSERT [dbo].[ProductSpecification] ([ProductId], [AvailableAttributeId], [Label], [DisplayOrder], [ProductSpecificationGroupId], [IsVisible], [IsFilterable], [Id]) VALUES (11, 16, NULL, 0, 0, 1, 0, 36)

INSERT [dbo].[ProductSpecification] ([ProductId], [AvailableAttributeId], [Label], [DisplayOrder], [ProductSpecificationGroupId], [IsVisible], [IsFilterable], [Id]) VALUES (11, 2, NULL, 0, 0, 1, 1, 37)

INSERT [dbo].[ProductSpecification] ([ProductId], [AvailableAttributeId], [Label], [DisplayOrder], [ProductSpecificationGroupId], [IsVisible], [IsFilterable], [Id]) VALUES (11, 11, NULL, 0, 0, 1, 1, 38)

INSERT [dbo].[ProductSpecification] ([ProductId], [AvailableAttributeId], [Label], [DisplayOrder], [ProductSpecificationGroupId], [IsVisible], [IsFilterable], [Id]) VALUES (16, 12, NULL, 0, 0, 0, 1, 39)

SET IDENTITY_INSERT [dbo].[ProductSpecification] OFF

SET IDENTITY_INSERT [dbo].[ProductSpecificationValue] ON 

INSERT [dbo].[ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (1, 1, NULL, 1)

INSERT [dbo].[ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (2, 2, NULL, 2)

INSERT [dbo].[ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (3, 3, NULL, 3)

INSERT [dbo].[ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (4, 4, NULL, 4)

INSERT [dbo].[ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (5, 5, NULL, 5)

INSERT [dbo].[ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (6, 6, NULL, 6)

INSERT [dbo].[ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (7, 5, NULL, 7)

INSERT [dbo].[ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (8, 8, NULL, 8)

INSERT [dbo].[ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (9, 13, NULL, 9)

INSERT [dbo].[ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (10, 10, NULL, 10)

INSERT [dbo].[ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (10, 11, NULL, 11)

INSERT [dbo].[ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (11, 16, NULL, 12)

INSERT [dbo].[ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (11, 17, NULL, 13)

INSERT [dbo].[ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (12, 14, NULL, 14)

INSERT [dbo].[ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (12, 15, NULL, 15)

INSERT [dbo].[ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (13, 18, NULL, 16)

INSERT [dbo].[ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (14, 19, NULL, 17)

INSERT [dbo].[ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (15, 20, NULL, 18)

INSERT [dbo].[ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (16, 21, NULL, 19)

INSERT [dbo].[ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (17, 22, NULL, 20)

INSERT [dbo].[ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (18, 23, NULL, 21)

INSERT [dbo].[ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (19, 24, NULL, 22)

INSERT [dbo].[ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (20, 25, NULL, 23)

INSERT [dbo].[ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (21, 26, NULL, 24)

INSERT [dbo].[ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (22, 27, NULL, 25)

INSERT [dbo].[ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (23, 28, NULL, 26)

INSERT [dbo].[ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (24, 29, NULL, 27)

INSERT [dbo].[ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (25, 30, NULL, 28)

INSERT [dbo].[ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (26, 34, NULL, 29)

INSERT [dbo].[ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (26, 35, NULL, 30)

INSERT [dbo].[ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (27, 38, NULL, 31)

INSERT [dbo].[ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (28, 39, NULL, 32)

INSERT [dbo].[ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (29, 40, NULL, 33)

INSERT [dbo].[ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (30, 41, NULL, 34)

INSERT [dbo].[ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (31, 42, NULL, 35)

INSERT [dbo].[ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (31, 43, NULL, 36)

INSERT [dbo].[ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (31, 44, NULL, 37)

INSERT [dbo].[ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (31, 45, NULL, 38)

INSERT [dbo].[ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (32, 46, NULL, 39)

INSERT [dbo].[ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (33, 21, NULL, 40)

INSERT [dbo].[ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (34, 47, NULL, 41)

INSERT [dbo].[ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (35, 48, NULL, 42)

INSERT [dbo].[ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (36, 49, NULL, 43)

INSERT [dbo].[ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (37, 50, NULL, 44)

INSERT [dbo].[ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (38, 51, NULL, 45)

INSERT [dbo].[ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (39, 21, NULL, 46)

INSERT [dbo].[ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (39, 27, NULL, 47)

SET IDENTITY_INSERT [dbo].[ProductSpecificationValue] OFF

SET IDENTITY_INSERT [dbo].[EntityStore] ON 

INSERT [dbo].[EntityStore] ([EntityId], [EntityName], [StoreId], [Id]) VALUES (1, N'Menu', 1, 2)

INSERT [dbo].[EntityStore] ([EntityId], [EntityName], [StoreId], [Id]) VALUES (2, N'Menu', 1, 3)

INSERT [dbo].[EntityStore] ([EntityId], [EntityName], [StoreId], [Id]) VALUES (3, N'Menu', 1, 4)

INSERT [dbo].[EntityStore] ([EntityId], [EntityName], [StoreId], [Id]) VALUES (4, N'Menu', 1, 5)

INSERT [dbo].[EntityStore] ([EntityId], [EntityName], [StoreId], [Id]) VALUES (1, N'ContentPage', 1, 6)

INSERT [dbo].[EntityStore] ([EntityId], [EntityName], [StoreId], [Id]) VALUES (1, N'Catalog', 1, 7)

SET IDENTITY_INSERT [dbo].[EntityStore] OFF

INSERT [dbo].[ProductCatalog] ([ProductId], [CatalogId]) VALUES (2, 1)

INSERT [dbo].[ProductCatalog] ([ProductId], [CatalogId]) VALUES (3, 1)

INSERT [dbo].[ProductCatalog] ([ProductId], [CatalogId]) VALUES (4, 1)

INSERT [dbo].[ProductCatalog] ([ProductId], [CatalogId]) VALUES (5, 1)

INSERT [dbo].[ProductCatalog] ([ProductId], [CatalogId]) VALUES (6, 1)

INSERT [dbo].[ProductCatalog] ([ProductId], [CatalogId]) VALUES (7, 1)

INSERT [dbo].[ProductCatalog] ([ProductId], [CatalogId]) VALUES (8, 1)

INSERT [dbo].[ProductCatalog] ([ProductId], [CatalogId]) VALUES (9, 1)

INSERT [dbo].[ProductCatalog] ([ProductId], [CatalogId]) VALUES (10, 1)

INSERT [dbo].[ProductCatalog] ([ProductId], [CatalogId]) VALUES (11, 1)

INSERT [dbo].[ProductCatalog] ([ProductId], [CatalogId]) VALUES (12, 1)

INSERT [dbo].[ProductCatalog] ([ProductId], [CatalogId]) VALUES (13, 1)

INSERT [dbo].[ProductCatalog] ([ProductId], [CatalogId]) VALUES (14, 1)

INSERT [dbo].[ProductCatalog] ([ProductId], [CatalogId]) VALUES (15, 1)

INSERT [dbo].[ProductCatalog] ([ProductId], [CatalogId]) VALUES (16, 1)

SET IDENTITY_INSERT [dbo].[Menu] ON 

INSERT [dbo].[Menu] ([Name], [Id]) VALUES (N'Primary Menu', 1)

INSERT [dbo].[Menu] ([Name], [Id]) VALUES (N'Footer Column One', 2)

INSERT [dbo].[Menu] ([Name], [Id]) VALUES (N'Footer Two', 3)

INSERT [dbo].[Menu] ([Name], [Id]) VALUES (N'Footer Three', 4)

SET IDENTITY_INSERT [dbo].[Menu] OFF

SET IDENTITY_INSERT [dbo].[MenuItem] ON 

INSERT [dbo].[MenuItem] ([MenuId], [Name], [SeoMetaId], [Url], [DisplayOrder], [CssClass], [IsGroup], [Id], [OpenInNewWindow], [Description], [ExtraData], [ParentId]) VALUES (1, N'Laptops', 4, NULL, 0, NULL, 0, 1, 0, N'Collection of best laptops in all the price ranges', N'test', 0)

INSERT [dbo].[MenuItem] ([MenuId], [Name], [SeoMetaId], [Url], [DisplayOrder], [CssClass], [IsGroup], [Id], [OpenInNewWindow], [Description], [ExtraData], [ParentId]) VALUES (1, N'Mobiles', 8, NULL, 1, NULL, 0, 2, 1, NULL, NULL, 0)

INSERT [dbo].[MenuItem] ([MenuId], [Name], [SeoMetaId], [Url], [DisplayOrder], [CssClass], [IsGroup], [Id], [OpenInNewWindow], [Description], [ExtraData], [ParentId]) VALUES (1, N'Printers', 11, NULL, 2, NULL, 0, 3, 0, NULL, NULL, 0)

INSERT [dbo].[MenuItem] ([MenuId], [Name], [SeoMetaId], [Url], [DisplayOrder], [CssClass], [IsGroup], [Id], [OpenInNewWindow], [Description], [ExtraData], [ParentId]) VALUES (1, N'Storage', 18, NULL, 3, NULL, 0, 4, 0, NULL, NULL, 0)

INSERT [dbo].[MenuItem] ([MenuId], [Name], [SeoMetaId], [Url], [DisplayOrder], [CssClass], [IsGroup], [Id], [OpenInNewWindow], [Description], [ExtraData], [ParentId]) VALUES (1, N'Televisions', 23, NULL, 4, NULL, 0, 5, 0, NULL, NULL, 0)

INSERT [dbo].[MenuItem] ([MenuId], [Name], [SeoMetaId], [Url], [DisplayOrder], [CssClass], [IsGroup], [Id], [OpenInNewWindow], [Description], [ExtraData], [ParentId]) VALUES (1, N'Accessories', 14, NULL, 0, NULL, 0, 6, 0, NULL, NULL, 3)

INSERT [dbo].[MenuItem] ([MenuId], [Name], [SeoMetaId], [Url], [DisplayOrder], [CssClass], [IsGroup], [Id], [OpenInNewWindow], [Description], [ExtraData], [ParentId]) VALUES (1, N'Software', 26, NULL, 5, NULL, 0, 7, 0, NULL, NULL, 0)

INSERT [dbo].[MenuItem] ([MenuId], [Name], [SeoMetaId], [Url], [DisplayOrder], [CssClass], [IsGroup], [Id], [OpenInNewWindow], [Description], [ExtraData], [ParentId]) VALUES (2, N'About us', 30, NULL, 1, NULL, 0, 8, 0, NULL, NULL, 0)

INSERT [dbo].[MenuItem] ([MenuId], [Name], [SeoMetaId], [Url], [DisplayOrder], [CssClass], [IsGroup], [Id], [OpenInNewWindow], [Description], [ExtraData], [ParentId]) VALUES (2, N'Contact Us', 31, NULL, 2, NULL, 0, 9, 0, NULL, NULL, 0)

INSERT [dbo].[MenuItem] ([MenuId], [Name], [SeoMetaId], [Url], [DisplayOrder], [CssClass], [IsGroup], [Id], [OpenInNewWindow], [Description], [ExtraData], [ParentId]) VALUES (2, N'Privacy Policy', 32, NULL, 3, NULL, 0, 10, 0, NULL, NULL, 0)

INSERT [dbo].[MenuItem] ([MenuId], [Name], [SeoMetaId], [Url], [DisplayOrder], [CssClass], [IsGroup], [Id], [OpenInNewWindow], [Description], [ExtraData], [ParentId]) VALUES (2, N'Terms & Conditions', 33, NULL, 4, NULL, 0, 11, 0, NULL, NULL, 0)

INSERT [dbo].[MenuItem] ([MenuId], [Name], [SeoMetaId], [Url], [DisplayOrder], [CssClass], [IsGroup], [Id], [OpenInNewWindow], [Description], [ExtraData], [ParentId]) VALUES (3, N'Orders', NULL, N'/account/orders', 1, NULL, 0, 12, 0, NULL, NULL, 0)

INSERT [dbo].[MenuItem] ([MenuId], [Name], [SeoMetaId], [Url], [DisplayOrder], [CssClass], [IsGroup], [Id], [OpenInNewWindow], [Description], [ExtraData], [ParentId]) VALUES (3, N'Profile', NULL, N'/account', 0, NULL, 0, 13, 0, NULL, NULL, 0)

INSERT [dbo].[MenuItem] ([MenuId], [Name], [SeoMetaId], [Url], [DisplayOrder], [CssClass], [IsGroup], [Id], [OpenInNewWindow], [Description], [ExtraData], [ParentId]) VALUES (3, N'Privacy', NULL, N'/account/privacy', 2, NULL, 0, 14, 0, NULL, NULL, 0)

INSERT [dbo].[MenuItem] ([MenuId], [Name], [SeoMetaId], [Url], [DisplayOrder], [CssClass], [IsGroup], [Id], [OpenInNewWindow], [Description], [ExtraData], [ParentId]) VALUES (3, N'My Cart', NULL, N'/cart', 4, NULL, 0, 15, 0, NULL, NULL, 0)

INSERT [dbo].[MenuItem] ([MenuId], [Name], [SeoMetaId], [Url], [DisplayOrder], [CssClass], [IsGroup], [Id], [OpenInNewWindow], [Description], [ExtraData], [ParentId]) VALUES (3, N'My Wishlist', NULL, N'/account/wishlist', 5, NULL, 0, 16, 0, NULL, NULL, 0)

SET IDENTITY_INSERT [dbo].[MenuItem] OFF

SET IDENTITY_INSERT [dbo].[Vendor] ON 

INSERT [dbo].[Vendor] ([Name], [GstNumber], [Tin], [Pan], [Address], [StateProvinceId], [StateProvinceName], [City], [CountryId], [ZipPostalCode], [Phone], [Email], [Id], [VendorStatus], [Deleted]) VALUES (N'Sojatia Infocrafts', N'NA', N'NA', N'NA', N'M25 Sterling Tower', NULL, N'Madhya Pradesh', N'Indore', 1, N'452009', N'9876543210', N'enquiry@apexol.com', 1, 0, 0)

SET IDENTITY_INSERT [dbo].[Vendor] OFF

SET IDENTITY_INSERT [dbo].[ProductVendor] ON 

INSERT [dbo].[ProductVendor] ([ProductId], [VendorId], [Price], [ShippingPrice], [Id]) VALUES (16, 1, CAST(0.00000 AS Numeric(18, 5)), CAST(0.00000 AS Numeric(18, 5)), 1)

SET IDENTITY_INSERT [dbo].[ProductVendor] OFF

SET IDENTITY_INSERT [dbo].[Review] ON 

INSERT [dbo].[Review] ([Rating], [Title], [Description], [UserId], [VerifiedPurchase], [OrderId], [ProductId], [Published], [CreatedOn], [UpdatedOn], [Private], [Id]) VALUES (4, N'Nice Buy', N'Good product.
Awesome battery backup
Worth for money
Simple drawback is that it doesn''t have anti-glare display', 1, 0, NULL, 2, 1, CAST(N'2019-07-05T14:16:16.970' AS DateTime), CAST(N'2019-07-05T14:16:16.970' AS DateTime), 0, 1)

INSERT [dbo].[Review] ([Rating], [Title], [Description], [UserId], [VerifiedPurchase], [OrderId], [ProductId], [Published], [CreatedOn], [UpdatedOn], [Private], [Id]) VALUES (5, N'It''s awesome!', N'Keeping it short!!

I think it’s still one of the best your money can buy. Spending 99,990 for xs just for 30% faster processor ( who needs that, its already amazingly fast ) 

• FaceID - I came from touchID and first few days were so irritating that I disabled my FaceID but man FaceID is amazing, I realised this af I started using it. Filling up auto passwords, opening app locks (whatsapp etc) unlocking device etc... Its so fast and amazing that you’ll forget there’s something like unlocking.

• Display - One of the best one and yes True tone is real. It’s an different experience

• Performance - It superfast yet not fast as XR or XS but still not worth spending 30k extra for that milliseconds difference in app launches // seconds late in games ( Not a gamer so Its okay for me )
It exports edited 4k 60fps video from video editors in a charm. No android do that.

• Camera - iPhone’s camera captures real colours, no AI BEAUTY MODE ETC behind the scenes so atleast please do not compare with that OnePlus, I don’t like the way that 1+ captures pictures. 

For Video recording (4k ) This is beast , real colours, amazing stability and with no lag. RIP ANDROIDS here

• Battery- I do not know why people criticise battery of X, it’s amazing what I expected from it. Easily lasts for a day.', 1, 0, NULL, 4, 1, CAST(N'2019-07-05T15:09:03.457' AS DateTime), CAST(N'2019-07-05T15:09:03.457' AS DateTime), 0, 2)

SET IDENTITY_INSERT [dbo].[Review] OFF

SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([Name], [Description], [DisplayOrder], [TaxId], [MediaId], [DisableSale], [Id], [ParentId]) VALUES (N'Computers & Accessories', NULL, 0, NULL, 0, 0, 1, 10)

INSERT [dbo].[Category] ([Name], [Description], [DisplayOrder], [TaxId], [MediaId], [DisableSale], [Id], [ParentId]) VALUES (N'Laptops', NULL, 0, NULL, 0, 0, 2, 1)

INSERT [dbo].[Category] ([Name], [Description], [DisplayOrder], [TaxId], [MediaId], [DisableSale], [Id], [ParentId]) VALUES (N'Mobiles & Accessories', NULL, 0, NULL, 0, 0, 3, 10)

INSERT [dbo].[Category] ([Name], [Description], [DisplayOrder], [TaxId], [MediaId], [DisableSale], [Id], [ParentId]) VALUES (N'Mobiles', NULL, 0, NULL, 0, 0, 4, 3)

INSERT [dbo].[Category] ([Name], [Description], [DisplayOrder], [TaxId], [MediaId], [DisableSale], [Id], [ParentId]) VALUES (N'Apple Mobiles', NULL, 0, NULL, 0, 0, 5, 4)

INSERT [dbo].[Category] ([Name], [Description], [DisplayOrder], [TaxId], [MediaId], [DisableSale], [Id], [ParentId]) VALUES (N'Printers', NULL, 0, NULL, 0, 0, 6, 1)

INSERT [dbo].[Category] ([Name], [Description], [DisplayOrder], [TaxId], [MediaId], [DisableSale], [Id], [ParentId]) VALUES (N'Accessories', NULL, 0, NULL, 0, 0, 7, 6)

INSERT [dbo].[Category] ([Name], [Description], [DisplayOrder], [TaxId], [MediaId], [DisableSale], [Id], [ParentId]) VALUES (N'Smart Watches', NULL, 0, NULL, 0, 0, 8, 10)

INSERT [dbo].[Category] ([Name], [Description], [DisplayOrder], [TaxId], [MediaId], [DisableSale], [Id], [ParentId]) VALUES (N'Storage', NULL, 0, NULL, 0, 0, 9, 1)

INSERT [dbo].[Category] ([Name], [Description], [DisplayOrder], [TaxId], [MediaId], [DisableSale], [Id], [ParentId]) VALUES (N'Technology', NULL, 0, NULL, 0, 0, 10, 0)

INSERT [dbo].[Category] ([Name], [Description], [DisplayOrder], [TaxId], [MediaId], [DisableSale], [Id], [ParentId]) VALUES (N'Electronics', NULL, 0, NULL, 0, 0, 11, 0)

INSERT [dbo].[Category] ([Name], [Description], [DisplayOrder], [TaxId], [MediaId], [DisableSale], [Id], [ParentId]) VALUES (N'Televisions', NULL, 0, NULL, 0, 0, 12, 11)

INSERT [dbo].[Category] ([Name], [Description], [DisplayOrder], [TaxId], [MediaId], [DisableSale], [Id], [ParentId]) VALUES (N'Software', NULL, 0, NULL, 0, 0, 13, 0)

INSERT [dbo].[Category] ([Name], [Description], [DisplayOrder], [TaxId], [MediaId], [DisableSale], [Id], [ParentId]) VALUES (N'Security', NULL, 0, NULL, 0, 0, 14, 13)

INSERT [dbo].[Category] ([Name], [Description], [DisplayOrder], [TaxId], [MediaId], [DisableSale], [Id], [ParentId]) VALUES (N'Utilities', NULL, 0, NULL, 0, 0, 15, 13)

SET IDENTITY_INSERT [dbo].[Category] OFF

SET IDENTITY_INSERT [dbo].[ProductCategory] ON 

INSERT [dbo].[ProductCategory] ([ProductId], [CategoryId], [DisplayOrder], [Id]) VALUES (2, 2, 0, 1)

INSERT [dbo].[ProductCategory] ([ProductId], [CategoryId], [DisplayOrder], [Id]) VALUES (3, 2, 0, 2)

INSERT [dbo].[ProductCategory] ([ProductId], [CategoryId], [DisplayOrder], [Id]) VALUES (4, 5, 0, 3)

INSERT [dbo].[ProductCategory] ([ProductId], [CategoryId], [DisplayOrder], [Id]) VALUES (5, 6, 0, 4)

INSERT [dbo].[ProductCategory] ([ProductId], [CategoryId], [DisplayOrder], [Id]) VALUES (6, 6, 0, 5)

INSERT [dbo].[ProductCategory] ([ProductId], [CategoryId], [DisplayOrder], [Id]) VALUES (7, 7, 0, 6)

INSERT [dbo].[ProductCategory] ([ProductId], [CategoryId], [DisplayOrder], [Id]) VALUES (8, 8, 0, 7)

INSERT [dbo].[ProductCategory] ([ProductId], [CategoryId], [DisplayOrder], [Id]) VALUES (9, 9, 0, 8)

INSERT [dbo].[ProductCategory] ([ProductId], [CategoryId], [DisplayOrder], [Id]) VALUES (10, 9, 0, 9)

INSERT [dbo].[ProductCategory] ([ProductId], [CategoryId], [DisplayOrder], [Id]) VALUES (11, 12, 0, 10)

INSERT [dbo].[ProductCategory] ([ProductId], [CategoryId], [DisplayOrder], [Id]) VALUES (12, 12, 0, 11)

INSERT [dbo].[ProductCategory] ([ProductId], [CategoryId], [DisplayOrder], [Id]) VALUES (13, 14, 0, 12)

INSERT [dbo].[ProductCategory] ([ProductId], [CategoryId], [DisplayOrder], [Id]) VALUES (14, 15, 0, 13)

SET IDENTITY_INSERT [dbo].[ProductCategory] OFF

SET IDENTITY_INSERT [dbo].[ProductSpecificationGroup] ON 

INSERT [dbo].[ProductSpecificationGroup] ([Name], [DisplayOrder], [ProductId], [Id]) VALUES (N'Specifications', 0, 2, 1)

SET IDENTITY_INSERT [dbo].[ProductSpecificationGroup] OFF

SET IDENTITY_INSERT [dbo].[ProductRelation] ON 

INSERT [dbo].[ProductRelation] ([SourceProductId], [DestinationProductId], [RelationType], [IsReciprocal], [Id]) VALUES (3, 2, 1, 0, 1)

INSERT [dbo].[ProductRelation] ([SourceProductId], [DestinationProductId], [RelationType], [IsReciprocal], [Id]) VALUES (2, 3, 1, 0, 2)

INSERT [dbo].[ProductRelation] ([SourceProductId], [DestinationProductId], [RelationType], [IsReciprocal], [Id]) VALUES (6, 5, 1, 0, 3)

INSERT [dbo].[ProductRelation] ([SourceProductId], [DestinationProductId], [RelationType], [IsReciprocal], [Id]) VALUES (5, 6, 1, 0, 4)

INSERT [dbo].[ProductRelation] ([SourceProductId], [DestinationProductId], [RelationType], [IsReciprocal], [Id]) VALUES (10, 16, 1, 0, 6)

SET IDENTITY_INSERT [dbo].[ProductRelation] OFF

SET IDENTITY_INSERT [dbo].[Consent] ON 

INSERT [dbo].[Consent] ([Title], [Description], [IsPluginSpecificConsent], [PluginSystemName], [IsRequired], [DisplayOrder], [LanguageCultureCode], [EnableLogging], [OneTimeSelection], [Published], [ConsentGroupId], [Id]) VALUES (N'I agree to the usage terms.', N'The website usage is governed by our <a href=''/terms-conditions''>terms and conditions</a> and <a href=''/privacy-policy''>privacy policy</a>.', 0, NULL, 1, 0, NULL, 1, 1, 1, 0, 1)

INSERT [dbo].[Consent] ([Title], [Description], [IsPluginSpecificConsent], [PluginSystemName], [IsRequired], [DisplayOrder], [LanguageCultureCode], [EnableLogging], [OneTimeSelection], [Published], [ConsentGroupId], [Id]) VALUES (N'I agree to accept Essential Cookies on my browser/app', N'These cookies are strictly necessary to provide you with services available through our websites and to use some of its features, such as access to secure areas.', 0, NULL, 1, 0, NULL, 0, 0, 1, 1, 2)

INSERT [dbo].[Consent] ([Title], [Description], [IsPluginSpecificConsent], [PluginSystemName], [IsRequired], [DisplayOrder], [LanguageCultureCode], [EnableLogging], [OneTimeSelection], [Published], [ConsentGroupId], [Id]) VALUES (N'I agree to accept Preference Cookies on my browser/app', N'Preference cookies enable a website to remember information that changes the way the website behaves or looks, like your preferred language or the region that you are in.', 0, NULL, 0, 1, NULL, 1, 0, 1, 1, 3)

INSERT [dbo].[Consent] ([Title], [Description], [IsPluginSpecificConsent], [PluginSystemName], [IsRequired], [DisplayOrder], [LanguageCultureCode], [EnableLogging], [OneTimeSelection], [Published], [ConsentGroupId], [Id]) VALUES (N'I agree to accept Statistical Cookies on my browser/app', N'Statistic cookies help website owners to understand how visitors interact with websites by collecting and reporting information anonymously.', 0, NULL, 0, 2, NULL, 1, 0, 1, 1, 4)

INSERT [dbo].[Consent] ([Title], [Description], [IsPluginSpecificConsent], [PluginSystemName], [IsRequired], [DisplayOrder], [LanguageCultureCode], [EnableLogging], [OneTimeSelection], [Published], [ConsentGroupId], [Id]) VALUES (N'I would like to receive updates on my email regarding promotions', N'Your email will not be shared with any third party websites without your explicit consent. You can always change your preferences anytime by visiting this page.', 0, NULL, 0, 0, NULL, 1, 1, 1, 2, 5)

SET IDENTITY_INSERT [dbo].[Consent] OFF

SET IDENTITY_INSERT [dbo].[ConsentGroup] ON 

INSERT [dbo].[ConsentGroup] ([Name], [Description], [DisplayOrder], [Id]) VALUES (N'Cookie Preferences', N'We use cookies to personalise content and ads, to provide social media features and to analyse our traffic. We also share information about your use of our site with our social media, advertising and analytics partners who may combine it with other information that you’ve provided to them or that they’ve collected from your use of their services. You consent to our cookies if you continue to use our website.', 1, 1)

INSERT [dbo].[ConsentGroup] ([Name], [Description], [DisplayOrder], [Id]) VALUES (N'Newsletter Preferences', NULL, 2, 2)

SET IDENTITY_INSERT [dbo].[ConsentGroup] OFF

SET IDENTITY_INSERT [dbo].[ContentPage] ON 

INSERT [dbo].[ContentPage] ([Name], [UserId], [Content], [Published], [Private], [Password], [SystemName], [CreatedOn], [UpdatedOn], [PublishedOn], [Template], [Id], [ParentId]) VALUES (N'About us', 1, N'<p>Write some story about your site. People love stories.</p><p>
<script async="" src="https://platform.twitter.com/widgets.js" charset="utf-8"></script></p><br>', 1, 0, NULL, NULL, CAST(N'2019-07-05T15:10:02.030' AS DateTime), CAST(N'2020-02-28T10:04:40.163' AS DateTime), CAST(N'2019-07-05T15:10:02.033' AS DateTime), N'0', 1, 0)

INSERT [dbo].[ContentPage] ([Name], [UserId], [Content], [Published], [Private], [Password], [SystemName], [CreatedOn], [UpdatedOn], [PublishedOn], [Template], [Id], [ParentId]) VALUES (N'Contact Us', 1, N'The contact us page will show some form. It uses a template from the theme.', 1, 0, NULL, NULL, CAST(N'2019-07-05T15:10:27.797' AS DateTime), CAST(N'2019-07-05T15:10:27.797' AS DateTime), CAST(N'2019-07-05T15:10:27.797' AS DateTime), N'ContactUs', 2, 0)

INSERT [dbo].[ContentPage] ([Name], [UserId], [Content], [Published], [Private], [Password], [SystemName], [CreatedOn], [UpdatedOn], [PublishedOn], [Template], [Id], [ParentId]) VALUES (N'Privacy Policy', 1, N'Why not write some privacy policy? It''s very important.', 1, 0, NULL, NULL, CAST(N'2019-07-05T15:10:47.203' AS DateTime), CAST(N'2019-07-05T15:10:47.203' AS DateTime), CAST(N'2019-07-05T15:10:47.203' AS DateTime), N'0', 3, 0)

INSERT [dbo].[ContentPage] ([Name], [UserId], [Content], [Published], [Private], [Password], [SystemName], [CreatedOn], [UpdatedOn], [PublishedOn], [Template], [Id], [ParentId]) VALUES (N'Terms & Conditions', 1, N'Are there any terms when people use your website? write them on this page.', 1, 0, NULL, NULL, CAST(N'2019-07-05T15:11:08.830' AS DateTime), CAST(N'2019-07-05T15:11:08.833' AS DateTime), CAST(N'2019-07-05T15:11:08.830' AS DateTime), N'0', 4, 0)

INSERT [dbo].[ContentPage] ([Name], [UserId], [Content], [Published], [Private], [Password], [SystemName], [CreatedOn], [UpdatedOn], [PublishedOn], [Template], [Id], [ParentId]) VALUES (N'Features', 1, NULL, 1, 0, NULL, NULL, CAST(N'2020-01-28T07:59:50.563' AS DateTime), CAST(N'2020-01-28T08:00:59.263' AS DateTime), CAST(N'2020-01-28T07:59:50.563' AS DateTime), N'0', 5, 0)

INSERT [dbo].[ContentPage] ([Name], [UserId], [Content], [Published], [Private], [Password], [SystemName], [CreatedOn], [UpdatedOn], [PublishedOn], [Template], [Id], [ParentId]) VALUES (N'Integrated Api', 1, NULL, 1, 0, NULL, NULL, CAST(N'2020-01-28T08:00:07.353' AS DateTime), CAST(N'2020-01-28T08:00:45.133' AS DateTime), CAST(N'2020-01-28T08:00:07.353' AS DateTime), N'0', 6, 5)

INSERT [dbo].[ContentPage] ([Name], [UserId], [Content], [Published], [Private], [Password], [SystemName], [CreatedOn], [UpdatedOn], [PublishedOn], [Template], [Id], [ParentId]) VALUES (N'Shopify Liquid', 1, NULL, 0, 0, NULL, NULL, CAST(N'2020-01-28T08:42:37.767' AS DateTime), CAST(N'2020-01-28T08:42:37.767' AS DateTime), CAST(N'2020-01-28T08:42:37.767' AS DateTime), N'0', 8, 5)

SET IDENTITY_INSERT [dbo].[ContentPage] OFF

SET IDENTITY_INSERT [dbo].[Manufacturer] ON 

INSERT [dbo].[Manufacturer] ([Name], [Id]) VALUES (N'Hewlett Packard', 1)

INSERT [dbo].[Manufacturer] ([Name], [Id]) VALUES (N'Microsoft', 2)

INSERT [dbo].[Manufacturer] ([Name], [Id]) VALUES (N'Apple', 3)

INSERT [dbo].[Manufacturer] ([Name], [Id]) VALUES (N'Canon', 4)

INSERT [dbo].[Manufacturer] ([Name], [Id]) VALUES (N'Western Digital', 5)

INSERT [dbo].[Manufacturer] ([Name], [Id]) VALUES (N'Samsung', 6)

INSERT [dbo].[Manufacturer] ([Name], [Id]) VALUES (N'Sony', 7)

INSERT [dbo].[Manufacturer] ([Name], [Id]) VALUES (N'Kaspersky Labs', 8)

SET IDENTITY_INSERT [dbo].[Manufacturer] OFF

SET IDENTITY_INSERT [dbo].[SeoMeta] ON 

INSERT [dbo].[SeoMeta] ([PageTitle], [MetaDescription], [MetaKeywords], [EntityId], [EntityName], [Slug], [LanguageCultureCode], [Id]) VALUES (NULL, NULL, NULL, 2, N'Product', N'hp-15-core-i3-7th-gen-156-inch-laptop', N'en-US', 2)

INSERT [dbo].[SeoMeta] ([PageTitle], [MetaDescription], [MetaKeywords], [EntityId], [EntityName], [Slug], [LanguageCultureCode], [Id]) VALUES (NULL, NULL, NULL, 1, N'Category', N'computers-accessories', N'en-US', 3)

INSERT [dbo].[SeoMeta] ([PageTitle], [MetaDescription], [MetaKeywords], [EntityId], [EntityName], [Slug], [LanguageCultureCode], [Id]) VALUES (NULL, NULL, NULL, 2, N'Category', N'laptops', N'en-US', 4)

INSERT [dbo].[SeoMeta] ([PageTitle], [MetaDescription], [MetaKeywords], [EntityId], [EntityName], [Slug], [LanguageCultureCode], [Id]) VALUES (NULL, NULL, NULL, 3, N'Product', N'microsoft-surface-pro-6-1796-2019-123-inch-la', N'en-US', 5)

INSERT [dbo].[SeoMeta] ([PageTitle], [MetaDescription], [MetaKeywords], [EntityId], [EntityName], [Slug], [LanguageCultureCode], [Id]) VALUES (N'Apple iPhone X', NULL, NULL, 4, N'Product', N'apple-iphone-x', N'en-US', 6)

INSERT [dbo].[SeoMeta] ([PageTitle], [MetaDescription], [MetaKeywords], [EntityId], [EntityName], [Slug], [LanguageCultureCode], [Id]) VALUES (NULL, NULL, NULL, 3, N'Category', N'mobiles-accessories', N'en-US', 7)

INSERT [dbo].[SeoMeta] ([PageTitle], [MetaDescription], [MetaKeywords], [EntityId], [EntityName], [Slug], [LanguageCultureCode], [Id]) VALUES (NULL, NULL, NULL, 4, N'Category', N'mobiles', N'en-US', 8)

INSERT [dbo].[SeoMeta] ([PageTitle], [MetaDescription], [MetaKeywords], [EntityId], [EntityName], [Slug], [LanguageCultureCode], [Id]) VALUES (NULL, NULL, NULL, 5, N'Category', N'apple-mobiles', N'en-US', 9)

INSERT [dbo].[SeoMeta] ([PageTitle], [MetaDescription], [MetaKeywords], [EntityId], [EntityName], [Slug], [LanguageCultureCode], [Id]) VALUES (NULL, NULL, NULL, 5, N'Product', N'canon-pixma-g-3000', N'en-US', 10)

INSERT [dbo].[SeoMeta] ([PageTitle], [MetaDescription], [MetaKeywords], [EntityId], [EntityName], [Slug], [LanguageCultureCode], [Id]) VALUES (NULL, NULL, NULL, 6, N'Category', N'printers', N'en-US', 11)

INSERT [dbo].[SeoMeta] ([PageTitle], [MetaDescription], [MetaKeywords], [EntityId], [EntityName], [Slug], [LanguageCultureCode], [Id]) VALUES (NULL, NULL, NULL, 6, N'Product', N'hp-310-all-in-one-ink-tank-color-printer', N'en-US', 12)

INSERT [dbo].[SeoMeta] ([PageTitle], [MetaDescription], [MetaKeywords], [EntityId], [EntityName], [Slug], [LanguageCultureCode], [Id]) VALUES (NULL, NULL, NULL, 7, N'Product', N'canon-pg-47-ink-cartridge', N'en-US', 13)

INSERT [dbo].[SeoMeta] ([PageTitle], [MetaDescription], [MetaKeywords], [EntityId], [EntityName], [Slug], [LanguageCultureCode], [Id]) VALUES (NULL, NULL, NULL, 7, N'Category', N'accessories', N'en-US', 14)

INSERT [dbo].[SeoMeta] ([PageTitle], [MetaDescription], [MetaKeywords], [EntityId], [EntityName], [Slug], [LanguageCultureCode], [Id]) VALUES (NULL, NULL, NULL, 8, N'Product', N'apple-watch-series-4', N'en-US', 15)

INSERT [dbo].[SeoMeta] ([PageTitle], [MetaDescription], [MetaKeywords], [EntityId], [EntityName], [Slug], [LanguageCultureCode], [Id]) VALUES (NULL, NULL, NULL, 8, N'Category', N'smart-watches', N'en-US', 16)

INSERT [dbo].[SeoMeta] ([PageTitle], [MetaDescription], [MetaKeywords], [EntityId], [EntityName], [Slug], [LanguageCultureCode], [Id]) VALUES (NULL, NULL, NULL, 9, N'Product', N'wd-elements-portable', N'en-US', 17)

INSERT [dbo].[SeoMeta] ([PageTitle], [MetaDescription], [MetaKeywords], [EntityId], [EntityName], [Slug], [LanguageCultureCode], [Id]) VALUES (NULL, NULL, NULL, 9, N'Category', N'storage', N'en-US', 18)

INSERT [dbo].[SeoMeta] ([PageTitle], [MetaDescription], [MetaKeywords], [EntityId], [EntityName], [Slug], [LanguageCultureCode], [Id]) VALUES (NULL, NULL, NULL, 10, N'Category', N'technology', N'en-US', 19)

INSERT [dbo].[SeoMeta] ([PageTitle], [MetaDescription], [MetaKeywords], [EntityId], [EntityName], [Slug], [LanguageCultureCode], [Id]) VALUES (NULL, NULL, NULL, 10, N'Product', N'my-passport-x', N'en-US', 20)

INSERT [dbo].[SeoMeta] ([PageTitle], [MetaDescription], [MetaKeywords], [EntityId], [EntityName], [Slug], [LanguageCultureCode], [Id]) VALUES (NULL, NULL, NULL, 11, N'Product', N'samsung-led-smart-tv', N'en-US', 21)

INSERT [dbo].[SeoMeta] ([PageTitle], [MetaDescription], [MetaKeywords], [EntityId], [EntityName], [Slug], [LanguageCultureCode], [Id]) VALUES (NULL, NULL, NULL, 11, N'Category', N'electronics', N'en-US', 22)

INSERT [dbo].[SeoMeta] ([PageTitle], [MetaDescription], [MetaKeywords], [EntityId], [EntityName], [Slug], [LanguageCultureCode], [Id]) VALUES (NULL, NULL, NULL, 12, N'Category', N'televisions', N'en-US', 23)

INSERT [dbo].[SeoMeta] ([PageTitle], [MetaDescription], [MetaKeywords], [EntityId], [EntityName], [Slug], [LanguageCultureCode], [Id]) VALUES (NULL, NULL, NULL, 12, N'Product', N'sony-bravia-full-hd-led-smart-tv', N'en-US', 24)

INSERT [dbo].[SeoMeta] ([PageTitle], [MetaDescription], [MetaKeywords], [EntityId], [EntityName], [Slug], [LanguageCultureCode], [Id]) VALUES (NULL, NULL, NULL, 13, N'Product', N'kaspersky-internet-security', N'en-US', 25)

INSERT [dbo].[SeoMeta] ([PageTitle], [MetaDescription], [MetaKeywords], [EntityId], [EntityName], [Slug], [LanguageCultureCode], [Id]) VALUES (NULL, NULL, NULL, 13, N'Category', N'software', N'en-US', 26)

INSERT [dbo].[SeoMeta] ([PageTitle], [MetaDescription], [MetaKeywords], [EntityId], [EntityName], [Slug], [LanguageCultureCode], [Id]) VALUES (NULL, NULL, NULL, 14, N'Category', N'security', N'en-US', 27)

INSERT [dbo].[SeoMeta] ([PageTitle], [MetaDescription], [MetaKeywords], [EntityId], [EntityName], [Slug], [LanguageCultureCode], [Id]) VALUES (NULL, NULL, NULL, 14, N'Product', N'microsoft-office-home-and-business-2016', N'en-US', 28)

INSERT [dbo].[SeoMeta] ([PageTitle], [MetaDescription], [MetaKeywords], [EntityId], [EntityName], [Slug], [LanguageCultureCode], [Id]) VALUES (NULL, NULL, NULL, 15, N'Category', N'utilities', N'en-US', 29)

INSERT [dbo].[SeoMeta] ([PageTitle], [MetaDescription], [MetaKeywords], [EntityId], [EntityName], [Slug], [LanguageCultureCode], [Id]) VALUES (NULL, NULL, NULL, 1, N'ContentPage', N'about-us', N'en-US', 30)

INSERT [dbo].[SeoMeta] ([PageTitle], [MetaDescription], [MetaKeywords], [EntityId], [EntityName], [Slug], [LanguageCultureCode], [Id]) VALUES (NULL, NULL, NULL, 2, N'ContentPage', N'contact-us', N'en-US', 31)

INSERT [dbo].[SeoMeta] ([PageTitle], [MetaDescription], [MetaKeywords], [EntityId], [EntityName], [Slug], [LanguageCultureCode], [Id]) VALUES (NULL, NULL, NULL, 3, N'ContentPage', N'privacy-policy', N'en-US', 32)

INSERT [dbo].[SeoMeta] ([PageTitle], [MetaDescription], [MetaKeywords], [EntityId], [EntityName], [Slug], [LanguageCultureCode], [Id]) VALUES (NULL, NULL, NULL, 4, N'ContentPage', N'terms-conditions', N'en-US', 33)

INSERT [dbo].[SeoMeta] ([PageTitle], [MetaDescription], [MetaKeywords], [EntityId], [EntityName], [Slug], [LanguageCultureCode], [Id]) VALUES (NULL, NULL, NULL, 16, N'Product', N'subscription-product', N'en-US', 35)

INSERT [dbo].[SeoMeta] ([PageTitle], [MetaDescription], [MetaKeywords], [EntityId], [EntityName], [Slug], [LanguageCultureCode], [Id]) VALUES (NULL, NULL, NULL, 5, N'ContentPage', N'features', N'en-US', 36)

INSERT [dbo].[SeoMeta] ([PageTitle], [MetaDescription], [MetaKeywords], [EntityId], [EntityName], [Slug], [LanguageCultureCode], [Id]) VALUES (NULL, NULL, NULL, 6, N'ContentPage', N'integrated-api', N'en-US', 37)

INSERT [dbo].[SeoMeta] ([PageTitle], [MetaDescription], [MetaKeywords], [EntityId], [EntityName], [Slug], [LanguageCultureCode], [Id]) VALUES (NULL, NULL, NULL, 8, N'ContentPage', N'shopify-liquid', N'en-US', 39)

SET IDENTITY_INSERT [dbo].[SeoMeta] OFF

UPDATE [Setting] SET [Value] = 1 WHERE [GroupName] = 'GeneralSettings' AND [KEY] = 'PrimaryNavigationId'

DELETE FROM [Setting] WHERE [Key] = 'SitePlugins'

INSERT [Setting] ([GroupName], [Key], [Value], [StoreId]) VALUES (N'PluginSettings', N'SitePlugins', N'[{"pluginSystemName":"EvenCart.Ui.Slider","installed":true,"active":true,"activeStoreIds":[1]}]', 0)

DELETE FROM [Setting] WHERE [Key] = 'SiteWidgets'

INSERT [Setting] ([GroupName], [Key], [Value], [StoreId]) VALUES (N'PluginSettings', N'SiteWidgets', N'[{"pluginSystemName":"EvenCart.InbuiltWidgets","widgetSystemName":"Menu","displayOrder":0,"zoneName":"footer-one","id":"c3309a82-1e46-4b0c-b420-22c1d0fac4d2","storeId":1},{"pluginSystemName":"EvenCart.InbuiltWidgets","widgetSystemName":"Menu","displayOrder":0,"zoneName":"footer-two","id":"8b66cb71-7d69-4414-94ac-13954ba86a74","storeId":1},{"pluginSystemName":"EvenCart.InbuiltWidgets","widgetSystemName":"SocialIcons","displayOrder":0,"zoneName":"footer-four","id":"4f464c03-8b33-4dbc-8458-d89904d3f252","storeId":1},{"pluginSystemName":"EvenCart.InbuiltWidgets","widgetSystemName":"CustomHtml","displayOrder":0,"zoneName":"footer-three","id":"4fa74cb6-3602-4674-b9e1-94619a6662d8","storeId":1},{"pluginSystemName":"EvenCart.InbuiltWidgets","widgetSystemName":"CustomHtml","displayOrder":0,"zoneName":"footer","id":"98ae4133-a388-47d4-9489-25064b2a4ee0","storeId":1},{"pluginSystemName":"EvenCart.Ui.Slider","widgetSystemName":"SliderWidget","displayOrder":0,"zoneName":"slider","id":"04aeb61e-b4af-434e-a1ac-d47a2c282861","storeId":1},{"pluginSystemName":"EvenCart.InbuiltWidgets","widgetSystemName":"ProductCarousel","displayOrder":1,"zoneName":"slider","id":"5fce627c-e9e1-4060-b691-e55535563c43","storeId":1},{"pluginSystemName":"EvenCart.InbuiltWidgets","widgetSystemName":"ProductCarousel","displayOrder":2,"zoneName":"slider","id":"eb4cece4-4586-4cc2-980e-9f6664f3c6e9","storeId":1},{"pluginSystemName":"EvenCart.Ui.SearchPlus","widgetSystemName":"SearchPlusWidget","displayOrder":0,"zoneName":"after_global_search","id":"c3b1ecb8-1989-4727-b8a4-7371b4b041f7","storeId":1}]', 0)

DELETE FROM [Setting] WHERE [Key] LIKE 'widget_%'

INSERT [Setting] ([GroupName], [Key], [Value], [StoreId]) VALUES (N'WidgetSettings', N'widget_c3309a82-1e46-4b0c-b420-22c1d0fac4d2', N'{"title":"EvenCart Store","menuId":2,"id":"c3309a82-1e46-4b0c-b420-22c1d0fac4d2"}', 1)

INSERT [Setting] ([GroupName], [Key], [Value], [StoreId]) VALUES (N'WidgetSettings', N'widget_8b66cb71-7d69-4414-94ac-13954ba86a74', N'{"title":"My Account","menuId":3,"id":"8b66cb71-7d69-4414-94ac-13954ba86a74"}', 1)

INSERT [Setting] ([GroupName], [Key], [Value], [StoreId]) VALUES (N'WidgetSettings', N'widget_4f464c03-8b33-4dbc-8458-d89904d3f252', N'{"title":"Connect With Us","facebookUrl":"https://www.facebook.com/evencart","twitterUrl":"https://www.twitter.com/evencarthq","instagramUrl":"","linkedInUrl":"https://www.linkedin.com/evencart","youtubeUrl":"","rssFeedUrl":"","whatsAppUrl":"","skypeUrl":"","emailUrl":"mailto:support@evencart.com","id":"4f464c03-8b33-4dbc-8458-d89904d3f252"}', 1)

INSERT [Setting] ([GroupName], [Key], [Value], [StoreId]) VALUES (N'WidgetSettings', N'widget_4fa74cb6-3602-4674-b9e1-94619a6662d8', N'{"title":"Reach Us","content":"<p><strong>Sojatia Infocrafts Pvt. Ltd.</strong><br><span style=\"font-size: 0.9rem; letter-spacing: 0.02rem;\">M25, Sterling Tower, Near Apollo Tower<br></span><span style=\"font-size: 0.9rem; letter-spacing: 0.02rem;\">M.G. Road, Indore - 452009<br></span><span style=\"font-size: 0.9rem; letter-spacing: 0.02rem;\">Madhya Pradesh, India</span></p>","customFormat":"","id":"4fa74cb6-3602-4674-b9e1-94619a6662d8"}', 1)

INSERT [Setting] ([GroupName], [Key], [Value], [StoreId]) VALUES (N'WidgetSettings', N'widget_98ae4133-a388-47d4-9489-25064b2a4ee0', N'{"title":"","content":"<p style=\"text-align: center;\"><span style=\"text-align: left; font-size: 14.4px;\">©&nbsp;</span><span style=\"font-size: 0.9rem; letter-spacing: 0.02rem;\">Copyright 2019 Sojatia Infocrafts Private Limited. All rights reserved.</span></p>","customFormat":"","id":"98ae4133-a388-47d4-9489-25064b2a4ee0"}', 1)

INSERT [Setting] ([GroupName], [Key], [Value], [StoreId]) VALUES (N'WidgetSettings', N'widget_5fce627c-e9e1-4060-b691-e55535563c43', N'{"title":"Best Sellers","productIds":[12,8,6,11],"id":"5fce627c-e9e1-4060-b691-e55535563c43"}', 1)

INSERT [Setting] ([GroupName], [Key], [Value], [StoreId]) VALUES (N'WidgetSettings', N'widget_eb4cece4-4586-4cc2-980e-9f6664f3c6e9', N'{"title":"Premium Apple Collection","productIds":[4,8],"id":"eb4cece4-4586-4cc2-980e-9f6664f3c6e9"}', 1)

SET IDENTITY_INSERT [Tax] ON 

INSERT [Tax] ([Name], [Id]) VALUES (N'Goods and Services Tax', 1)

SET IDENTITY_INSERT [Tax] OFF

-- enable all constraints
exec sp_MSforeachtable @command1="print '?'", @command2="ALTER TABLE ? WITH CHECK CHECK CONSTRAINT all"