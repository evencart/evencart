EXEC sp_MSforeachtable "ALTER TABLE ? NOCHECK CONSTRAINT all"

SET IDENTITY_INSERT [Category] ON 


INSERT [Category] ([Name], [Description], [DisplayOrder], [TaxId], [ParentId], [MediaId], [Id]) VALUES (N'Computers & Accessories', NULL, 0, NULL, 10, 0, 1)

INSERT [Category] ([Name], [Description], [DisplayOrder], [TaxId], [ParentId], [MediaId], [Id]) VALUES (N'Laptops', NULL, 0, NULL, 1, 0, 2)

INSERT [Category] ([Name], [Description], [DisplayOrder], [TaxId], [ParentId], [MediaId], [Id]) VALUES (N'Mobiles & Accessories', NULL, 0, NULL, 10, 0, 3)

INSERT [Category] ([Name], [Description], [DisplayOrder], [TaxId], [ParentId], [MediaId], [Id]) VALUES (N'Mobiles', NULL, 0, NULL, 3, 0, 4)

INSERT [Category] ([Name], [Description], [DisplayOrder], [TaxId], [ParentId], [MediaId], [Id]) VALUES (N'Apple Mobiles', NULL, 0, NULL, 4, 0, 5)

INSERT [Category] ([Name], [Description], [DisplayOrder], [TaxId], [ParentId], [MediaId], [Id]) VALUES (N'Printers', NULL, 0, NULL, 1, 0, 6)

INSERT [Category] ([Name], [Description], [DisplayOrder], [TaxId], [ParentId], [MediaId], [Id]) VALUES (N'Accessories', NULL, 0, NULL, 6, 0, 7)

INSERT [Category] ([Name], [Description], [DisplayOrder], [TaxId], [ParentId], [MediaId], [Id]) VALUES (N'Smart Watches', NULL, 0, NULL, 10, 0, 8)

INSERT [Category] ([Name], [Description], [DisplayOrder], [TaxId], [ParentId], [MediaId], [Id]) VALUES (N'Storage', NULL, 0, NULL, 1, 0, 9)

INSERT [Category] ([Name], [Description], [DisplayOrder], [TaxId], [ParentId], [MediaId], [Id]) VALUES (N'Technology', NULL, 0, NULL, 0, 0, 10)

INSERT [Category] ([Name], [Description], [DisplayOrder], [TaxId], [ParentId], [MediaId], [Id]) VALUES (N'Electronics', NULL, 0, NULL, 0, 0, 11)

INSERT [Category] ([Name], [Description], [DisplayOrder], [TaxId], [ParentId], [MediaId], [Id]) VALUES (N'Televisions', NULL, 0, NULL, 11, 0, 12)

INSERT [Category] ([Name], [Description], [DisplayOrder], [TaxId], [ParentId], [MediaId], [Id]) VALUES (N'Software', NULL, 0, NULL, 0, 0, 13)

INSERT [Category] ([Name], [Description], [DisplayOrder], [TaxId], [ParentId], [MediaId], [Id]) VALUES (N'Security', NULL, 0, NULL, 13, 0, 14)

INSERT [Category] ([Name], [Description], [DisplayOrder], [TaxId], [ParentId], [MediaId], [Id]) VALUES (N'Utilities', NULL, 0, NULL, 13, 0, 15)

SET IDENTITY_INSERT [Category] OFF

SET IDENTITY_INSERT [Product] ON


INSERT [Product] ([Name], [Summary], [Description], [IsShippable], [IsDownloadable], [IsFeatured], [IsVisibleIndividually], [TrackInventory], [CanOrderWhenOutOfStock], [ComparePrice], [Price], [Sku], [Gtin], [Mpn], [MinimumPurchaseQuantity], [MaximumPurchaseQuantity], [CreatedOn], [UpdatedOn], [ParentProductId], [DisplayOrder], [ChargeTaxes], [Published], [Deleted], [ManufacturerId], [TaxId], [HasVariants], [ReviewsDisabled], [PopularityIndex], [PackageWeight], [PackageWeightUnit], [PackageWidth], [PackageWidthUnit], [PackageHeight], [PackageHeightUnit], [PackageLength], [PackageLengthUnit], [AdditionalShippingCharge], [IndividuallyShipped], [AllowReturns], [DaysForReturn], [Id], [ProductType], [ProductSaleType], [SubscriptionCycle], [CycleCount], [TrialDays], [RequireLoginToPurchase], [RequireLoginToViewPrice]) VALUES (N'HP 15 Core i3 7th gen 15.6-inch Laptop', N'<p><span style="font-size: 14.4px;">Designed for long-lasting performance, this stylishly designed HP laptop has a long-lasting battery that keeps you connected, entertained, and productive all day. Speed through tasks, or sit back and socialize - with the latest processors and a rich Full HD display. Do it all, all day.</span></p>', N'<p><span style="font-size: 14.4px;">Processor: 7th Gen Intel Core i3-7100U processor, 2.4GHz base processor speed, 2 cores, 3MB cache</span></p><p><span style="font-size: 14.4px;">Operating System: Pre-loaded Windows 10 Home with lifetime validity</span></p><p><span style="font-size: 14.4px;">Display: 15.6-inch Full HD (1920x1080) WLED display, Display Features: Diagonal FHD SVA Anti-Glare WLED-backlit Display</span></p><p></p><ul><li><span style="font-size: 14.4px;">Memory &amp; Storage: 4GB DDR4 RAM Intel HD Graphics 620 | Storage: 1TB HDD, HDD Speed(RPM): 5400 RPM</span></li><li>Design &amp; battery: Multi-touch gesture support | Thin and light design | Laptop weight: 2.2 kg | Average battery life = 7 hours, HP Fast Charge battery, Battery: 3 Cell, Li-Ion, Power Supply: 41 W AC Adapter W</li><li>Warranty: This genuine HP laptop comes with a 1-year domestic warranty from HP covering manufacturing defects and not covering physical damage. For more details, see Warranty section below</li><li>Preinstalled Software: Windows 10 Home | In the Box: Laptop with included battery and charger Ports &amp; CD drive: 1 HDMI, 2 USB 3.0, 1 USB 2.0, 1 Audio-output | With CD drive Other features: Anti Glare Display</li></ul><p></p>', 1, 0, 0, 0, 0, 0, CAST(650.00000 AS Numeric(18, 5)), CAST(449.00000 AS Numeric(18, 5)), N'15-DA0326TU', NULL, NULL, 1, 1, CAST(N'2019-07-02T12:06:46.667' AS DateTime), CAST(N'2019-11-19T11:53:52.597' AS DateTime), NULL, 0, 0, 1, 0, 1, NULL, 1, 0, 0, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 0, 0, 0, 2, 0, 0, 0, 0, NULL, 0, 0)

INSERT [Product] ([Name], [Summary], [Description], [IsShippable], [IsDownloadable], [IsFeatured], [IsVisibleIndividually], [TrackInventory], [CanOrderWhenOutOfStock], [ComparePrice], [Price], [Sku], [Gtin], [Mpn], [MinimumPurchaseQuantity], [MaximumPurchaseQuantity], [CreatedOn], [UpdatedOn], [ParentProductId], [DisplayOrder], [ChargeTaxes], [Published], [Deleted], [ManufacturerId], [TaxId], [HasVariants], [ReviewsDisabled], [PopularityIndex], [PackageWeight], [PackageWeightUnit], [PackageWidth], [PackageWidthUnit], [PackageHeight], [PackageHeightUnit], [PackageLength], [PackageLengthUnit], [AdditionalShippingCharge], [IndividuallyShipped], [AllowReturns], [DaysForReturn], [Id], [ProductType], [ProductSaleType], [SubscriptionCycle], [CycleCount], [TrialDays], [RequireLoginToPurchase], [RequireLoginToViewPrice]) VALUES (N'Microsoft Surface Pro 6 1796 2019 12.3-inch Laptop', N'<p><span style="font-size: 14.4px;">Unplug. Pack light. Get productive your way, all day, with new Surface Pro 6 – now faster than ever with the latest 8th Generation Intel Core processor. Wherever you are, new Surface Pro 6 makes it easy to work and play virtually anywhere, with laptop-to-tablet versatility that adapts to you.</span></p>', N'<p style="letter-spacing: 0.32px;"><span style="font-weight: bolder;">Your Laptop, your Way</span></p><p style="letter-spacing: 0.32px;">Wherever you are, new Surface Pro 6 makes it easy to work and play virtually anywhere, with laptop-to-tablet versatility that adapts to you.</p><span style="letter-spacing: 0.32px;"><br></span><p style="letter-spacing: 0.32px;"><span style="font-weight: bolder;">More Power for your Ideas</span></p><p style="letter-spacing: 0.32px;">Professional. Student. Creator. Whatever you do, let next-generation Surface Pro 6, featuring the latest 8th Generation Intel Core processor and all-day battery life, help you take your ideas to the next level.</p><span style="letter-spacing: 0.32px;"><br></span><p style="letter-spacing: 0.32px;"><span style="font-weight: bolder;">Mix, Match, Make it your own Personalize</span></p><p style="letter-spacing: 0.32px;">Personalize Surface Pro 6 to suit your style with a choice of Surface Accessories. Make it a full laptop with our Signature Type Cover*, Surface Pen*, and Surface Arc Mouse*.</p><span style="letter-spacing: 0.32px;"><br></span><p style="letter-spacing: 0.32px;"><span style="font-weight: bolder;">Do More with the Windows you Know</span></p><p style="letter-spacing: 0.32px;">With Windows 10 Home, enjoy familiar features like password-free Windows Hello sign-in and Cortana* intelligent assistant - and create your best work with Office 365* on Windows.</p>', 1, 0, 0, 0, 1, 0, NULL, CAST(1566.00000 AS Numeric(18, 5)), N'Pro1796', NULL, NULL, 1, 0, CAST(N'2019-07-03T11:45:03.197' AS DateTime), CAST(N'2019-07-03T12:32:55.583' AS DateTime), NULL, 0, 0, 1, 0, 2, NULL, 1, 0, 0, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 1, 0, 0, 3, 0, 0, 0, 0, NULL, 0, 0)

INSERT [Product] ([Name], [Summary], [Description], [IsShippable], [IsDownloadable], [IsFeatured], [IsVisibleIndividually], [TrackInventory], [CanOrderWhenOutOfStock], [ComparePrice], [Price], [Sku], [Gtin], [Mpn], [MinimumPurchaseQuantity], [MaximumPurchaseQuantity], [CreatedOn], [UpdatedOn], [ParentProductId], [DisplayOrder], [ChargeTaxes], [Published], [Deleted], [ManufacturerId], [TaxId], [HasVariants], [ReviewsDisabled], [PopularityIndex], [PackageWeight], [PackageWeightUnit], [PackageWidth], [PackageWidthUnit], [PackageHeight], [PackageHeightUnit], [PackageLength], [PackageLengthUnit], [AdditionalShippingCharge], [IndividuallyShipped], [AllowReturns], [DaysForReturn], [Id], [ProductType], [ProductSaleType], [SubscriptionCycle], [CycleCount], [TrialDays], [RequireLoginToPurchase], [RequireLoginToViewPrice]) VALUES (N'Apple iPhone X', N'<p><span style="font-size: 14.4px;">Meet the iPhone X - the device that''s so smart that it responds to a tap, your voice, and even a glance. Elegantly designed with a large 14.73 cm (5.8) Super Retina screen and a durable front-and-back glass, this smartphone is designed to impress. What''s more, you can charge this iPhone wirelessly.</span></p>', N'<p><span style="font-size: 14.4px;"><strong>14.73 cm Super Retina Screen</strong></span></p><p><span style="font-size: 14.4px;">Movies or games - with its Super Retina screen, you can enjoy an immersive-viewing experience that dazzles the eyes.</span></p><p style=""><span style="font-size: 14.4px;"><strong>Innovative Display Technology</strong></span></p><p style=""><span style="font-size: 14.4px;">The display, with new techniques and technology, follows the curves and its elegantly rounded corners.</span></p><p style=""><span style="font-size: 14.4px;"><strong>OLED Screen</strong></span></p><p style=""><span style="font-size: 14.4px;">Everything on the screen looks vibrant and beautiful, with true blacks, stunning colors, high brightness, and a 1,000,000 to 1 contrast ratio.</span></p><p style=""><span style="font-size: 14.4px;"><strong>IP67 Rating</strong></span></p><p style=""><span style="font-size: 14.4px;">Crafted using durable glass on both the sides, this phone, with surgical-grade stainless steel, is water- and dust-resistant.</span></p><p style=""><span style="font-size: 14.4px;"><strong>Intuitive Gestures</strong></span></p><p style=""><span style="font-size: 14.4px;">Navigating your phone using familiar gestures will be intuitive and natural. All it takes is a simple swipe to take you to your home screen from anywhere.</span></p><p style=""><span style="font-size: 14.4px;"><strong>Your Face is Your Password</strong></span></p><p style=""><span style="font-size: 14.4px;">Experience secure authentication with its Face ID; it projects and analyses more than 30,000 invisible dots on your face to create a depth map. What''s more, enabled by the TrueDepth camera and equipped with an adaptive recognition, the Face ID adapts to your face''s physical changes over time.</span></p><p style=""><span style="font-size: 14.4px;"><strong>Portrait Mode Selfies and Portrait Lighting</strong></span></p><p style=""><span style="font-size: 14.4px;">Click beautiful selfies with sharp foregrounds, blurred backgrounds and impressive studio-quality lighting effects.</span></p>', 1, 0, 0, 0, 0, 0, NULL, CAST(1029.00000 AS Numeric(18, 5)), NULL, NULL, NULL, 1, 0, CAST(N'2019-07-03T14:05:01.890' AS DateTime), CAST(N'2019-07-04T09:22:08.990' AS DateTime), NULL, 0, 0, 1, 0, 3, NULL, 1, 0, 0, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 0, 0, 0, 4, 0, 0, 0, 0, NULL, 0, 0)

INSERT [Product] ([Name], [Summary], [Description], [IsShippable], [IsDownloadable], [IsFeatured], [IsVisibleIndividually], [TrackInventory], [CanOrderWhenOutOfStock], [ComparePrice], [Price], [Sku], [Gtin], [Mpn], [MinimumPurchaseQuantity], [MaximumPurchaseQuantity], [CreatedOn], [UpdatedOn], [ParentProductId], [DisplayOrder], [ChargeTaxes], [Published], [Deleted], [ManufacturerId], [TaxId], [HasVariants], [ReviewsDisabled], [PopularityIndex], [PackageWeight], [PackageWeightUnit], [PackageWidth], [PackageWidthUnit], [PackageHeight], [PackageHeightUnit], [PackageLength], [PackageLengthUnit], [AdditionalShippingCharge], [IndividuallyShipped], [AllowReturns], [DaysForReturn], [Id], [ProductType], [ProductSaleType], [SubscriptionCycle], [CycleCount], [TrialDays], [RequireLoginToPurchase], [RequireLoginToViewPrice]) VALUES (N'Canon Pixma G3000 All-in-One Wireless Ink Tank Color Printer', N'<p><span style="font-size: 14.4px;">Canon''s first refillable ink tank system All-In-One wireless printer is designed for high volume printing at low running cost.</span></p>', N'<p><span style="font-size: 14.4px;"><strong>High Page Yield Ink Bottles</strong></span></p><p><span style="font-size: 14.4px;">With high page yield ink bottles up to 7000 pages, users can enjoy printing without having to worry about cost of ink, or ink supplies running low.</span></p><p style=""><span style="font-size: 14.4px;"><strong>Integrated Ink Tank System</strong></span></p><p style=""><span style="font-size: 14.4px;">Built-in integrated ink tanks create a compact printer body. Users can also view remaining ink levels easily at a glance.</span></p><p style=""><span style="font-size: 14.4px;"><strong>Quality Photo and Document Printing</strong></span></p><p style=""><span style="font-size: 14.4px;">Borderless photos can be printed up to A4 size, and Canon’s Hybrid ink system is equally adept at producing crisp black text documents and stunning photos.</span></p><p style=""><span style="font-size: 14.4px;"><strong>Wireless LAN</strong></span></p><p style=""><span style="font-size: 14.4px;">Built-in wireless LAN connectivity allows users to print wirelessly from PCs, laptops, mobile phones and tablet computers.</span></p><p style=""><span style="font-size: 14.4px;"><strong>Access Point Mode</strong></span></p><p style=""><span style="font-size: 14.4px;">The G3000 can behave as an access point in the absence of a wireless router, allowing a direct connection to be established to mobile phones and tablet PCs.</span></p><p style=""><span style="font-size: 14.4px;"><strong>Mobile and Cloud Printing</strong></span></p><p style=""><span style="font-size: 14.4px;">The new Canon PRINT Inkjet/SELPHY app supports easy, guided printing from mobile phones and tablet PCs. Also supports printing from cloud services such as Facebook and Dropbox via PIXMA Cloud Link.</span></p>', 1, 0, 0, 0, 1, 0, NULL, CAST(169.00000 AS Numeric(18, 5)), NULL, NULL, NULL, 1, 1, CAST(N'2019-07-04T05:39:33.700' AS DateTime), CAST(N'2019-07-04T10:56:44.030' AS DateTime), NULL, 0, 0, 1, 0, 4, NULL, 0, 0, 0, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 0, 0, 0, 5, 0, 0, 0, 0, NULL, 0, 0)

INSERT [Product] ([Name], [Summary], [Description], [IsShippable], [IsDownloadable], [IsFeatured], [IsVisibleIndividually], [TrackInventory], [CanOrderWhenOutOfStock], [ComparePrice], [Price], [Sku], [Gtin], [Mpn], [MinimumPurchaseQuantity], [MaximumPurchaseQuantity], [CreatedOn], [UpdatedOn], [ParentProductId], [DisplayOrder], [ChargeTaxes], [Published], [Deleted], [ManufacturerId], [TaxId], [HasVariants], [ReviewsDisabled], [PopularityIndex], [PackageWeight], [PackageWeightUnit], [PackageWidth], [PackageWidthUnit], [PackageHeight], [PackageHeightUnit], [PackageLength], [PackageLengthUnit], [AdditionalShippingCharge], [IndividuallyShipped], [AllowReturns], [DaysForReturn], [Id], [ProductType], [ProductSaleType], [SubscriptionCycle], [CycleCount], [TrialDays], [RequireLoginToPurchase], [RequireLoginToViewPrice]) VALUES (N'HP 310 All-in-One Ink Tank Color Printer', NULL, N'<p><span style="font-size: 14.4px;">Get up to 8,000 color or 6,000 black pages at an extremely low cost-per page. Print thousands of pages with high-capacity ink tank system. Restore ink levels with resealable bottles and our spill-free refill system. Easily refill your ink tank system with spill-free, resealable bottles. Print darker, crisper text and get borderless, fade-resistant photos and documents that last up to 22 times longer. Count on darker, crisper text, time after time. Functions: Print, copy, scan Color, A4: Up to 5 ppm, Black &amp; White, A4: Up to 8 ppm Orderable Supplies: HP GT52 Cyan Original Ink Bottle, HP GT52 Magenta Original Ink Bottle, HP GT52 Yellow Original Ink Bottle, HP GT51 Black Original Ink Bottle.</span></p>', 1, 0, 0, 0, 1, 0, CAST(199.00000 AS Numeric(18, 5)), CAST(149.00000 AS Numeric(18, 5)), N'Z6Z11A', NULL, NULL, 1, 0, CAST(N'2019-07-04T11:01:53.777' AS DateTime), CAST(N'2019-07-04T11:04:47.537' AS DateTime), NULL, 0, 0, 1, 0, 1, NULL, 0, 0, 0, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 1, 0, 0, 6, 0, 0, 0, 0, NULL, 0, 0)

INSERT [Product] ([Name], [Summary], [Description], [IsShippable], [IsDownloadable], [IsFeatured], [IsVisibleIndividually], [TrackInventory], [CanOrderWhenOutOfStock], [ComparePrice], [Price], [Sku], [Gtin], [Mpn], [MinimumPurchaseQuantity], [MaximumPurchaseQuantity], [CreatedOn], [UpdatedOn], [ParentProductId], [DisplayOrder], [ChargeTaxes], [Published], [Deleted], [ManufacturerId], [TaxId], [HasVariants], [ReviewsDisabled], [PopularityIndex], [PackageWeight], [PackageWeightUnit], [PackageWidth], [PackageWidthUnit], [PackageHeight], [PackageHeightUnit], [PackageLength], [PackageLengthUnit], [AdditionalShippingCharge], [IndividuallyShipped], [AllowReturns], [DaysForReturn], [Id], [ProductType], [ProductSaleType], [SubscriptionCycle], [CycleCount], [TrialDays], [RequireLoginToPurchase], [RequireLoginToViewPrice]) VALUES (N'Canon PG-47 Ink Cartridge', NULL, N'<p><span style="font-size: 14.4px;"><strong>Smooth Performance</strong></span></p><p><span style="font-size: 14.4px;">Running out of Ink for your Canon Inkjet printer? Get the Canon original PG-47 Ink Cartridge for flawless prints. This black ink cartridge offers smear free prints that lasts for a long time. Be it home or office, it is easy to install and not messy at all. It is engineered for high performance with smudge free, no smears and rich prints. With a high yield capacity of 400 pages, this cartridge is meant for superior performance. The durable and sturdy cartridge makes sure your Canon printer works smoothly without any glitches. Make sure you use original cartridge for a flawless printing experience.</span></p><span style="font-size: 14.4px;"><strong><br></strong></span><p><span style="font-size: 14.4px;"><strong>Compatibility and Design</strong></span></p><p><span style="font-size: 14.4px;">Built for inkjet printing technology, this Canon Cartridge is compatible with Canon E400 printers. Get prominent and superior prints at your office or at home with this cartridge. Your kid’s homework print or that urgent print at office can happen in no time with perfection. It has a dimension of 4.5 x 3 x 4.5 cm that fits perfectly for Canon’s Inkjet technology. It is light weight with just 32 g. You can easily carry it along with your office stationary. Grab the Canon PG-47 Ink Cartridge (Black) for improved productivity.</span></p>', 1, 0, 0, 0, 1, 0, CAST(11.00000 AS Numeric(18, 5)), CAST(10.00000 AS Numeric(18, 5)), NULL, NULL, NULL, 1, 0, CAST(N'2019-07-04T11:45:43.337' AS DateTime), CAST(N'2019-07-04T11:46:40.013' AS DateTime), NULL, 0, 0, 1, 0, 4, NULL, 1, 0, 0, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 0, 0, 0, 7, 0, 0, 0, 0, NULL, 0, 0)

INSERT [Product] ([Name], [Summary], [Description], [IsShippable], [IsDownloadable], [IsFeatured], [IsVisibleIndividually], [TrackInventory], [CanOrderWhenOutOfStock], [ComparePrice], [Price], [Sku], [Gtin], [Mpn], [MinimumPurchaseQuantity], [MaximumPurchaseQuantity], [CreatedOn], [UpdatedOn], [ParentProductId], [DisplayOrder], [ChargeTaxes], [Published], [Deleted], [ManufacturerId], [TaxId], [HasVariants], [ReviewsDisabled], [PopularityIndex], [PackageWeight], [PackageWeightUnit], [PackageWidth], [PackageWidthUnit], [PackageHeight], [PackageHeightUnit], [PackageLength], [PackageLengthUnit], [AdditionalShippingCharge], [IndividuallyShipped], [AllowReturns], [DaysForReturn], [Id], [ProductType], [ProductSaleType], [SubscriptionCycle], [CycleCount], [TrialDays], [RequireLoginToPurchase], [RequireLoginToViewPrice]) VALUES (N'Apple Watch Series 4', N'<p><span style="font-size: 14.4px;">Apple Watch Series 4. Fundamentally redesigned and re‑engineered to help you be even more active, healthy, and connected.</span></p>', N'<p><span style="font-size: 14.4px;"><strong>Part guardian. Part guru.</strong></span></p><p><span style="font-size: 14.4px;">ECG on your wrist. Notifications for low and high heart rate, and irregular rhythm. Fall detection and Emergency SOS. Breathe watch faces. It’s designed to improve your health every day and powerful enough to help protect it.</span></p><p style=""><span style="font-size: 14.4px;"><strong>Workouts that work harder.</strong></span></p><p style=""><span style="font-size: 14.4px;">Automatic workout detection. Yoga and hiking workouts. Advanced features for runners like cadence and pace alerts. See up to five metrics at a glance as you precisely track all your favorite ways to train.</span></p><p style=""><span style="font-size: 14.4px;"><strong>Made to motivate.</strong></span></p><p style=""><span style="font-size: 14.4px;">Head-to-head competitions. Activity sharing with friends. Personalized coaching. Monthly challenges. Achievement awards. Get all the motivation you need to close your Activity rings every day.</span></p><p><span style="font-size: 14.4px;"><strong>The freedom of cellular.</strong></span></p><p><span style="font-size: 14.4px;">Walkie-Talkie, phone calls, and messages. Stream Apple Music and Apple Podcasts.* More ways to use Siri. Built-in cellular lets you do it all on your watch — even while you’re away from your phone.</span></p>', 1, 0, 0, 0, 1, 0, NULL, CAST(399.00000 AS Numeric(18, 5)), NULL, NULL, NULL, 1, 0, CAST(N'2019-07-04T12:20:17.103' AS DateTime), CAST(N'2019-07-04T12:22:43.027' AS DateTime), NULL, 0, 0, 1, 0, 3, NULL, 1, 0, 0, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 0, 0, 0, 8, 0, 0, 0, 0, NULL, 0, 0)

INSERT [Product] ([Name], [Summary], [Description], [IsShippable], [IsDownloadable], [IsFeatured], [IsVisibleIndividually], [TrackInventory], [CanOrderWhenOutOfStock], [ComparePrice], [Price], [Sku], [Gtin], [Mpn], [MinimumPurchaseQuantity], [MaximumPurchaseQuantity], [CreatedOn], [UpdatedOn], [ParentProductId], [DisplayOrder], [ChargeTaxes], [Published], [Deleted], [ManufacturerId], [TaxId], [HasVariants], [ReviewsDisabled], [PopularityIndex], [PackageWeight], [PackageWeightUnit], [PackageWidth], [PackageWidthUnit], [PackageHeight], [PackageHeightUnit], [PackageLength], [PackageLengthUnit], [AdditionalShippingCharge], [IndividuallyShipped], [AllowReturns], [DaysForReturn], [Id], [ProductType], [ProductSaleType], [SubscriptionCycle], [CycleCount], [TrialDays], [RequireLoginToPurchase], [RequireLoginToViewPrice]) VALUES (N'WD Elements Portable', N'<p><span style="font-size: 14.4px;">WD Elements™ portable hard drives with USB 3.0 offer reliable, high-capacity storage to go, fast data transfer rates, universal connectivity and massive capacity for value-conscious consumers.</span></p>', N'<p><span style="font-size: 14.4px;"><strong>MASSIVE CAPACITY IN A SMALL ENCLOSURE</strong></span></p><p><span style="font-size: 14.4px;">The small, lightweight design offers up to 4TB capacity, making WD Elements portable hard drives the ideal companion for consumers on the go.</span></p><p style=""><span style="font-size: 14.4px;"><strong>WD QUALITY INSIDE AND OUT</strong></span></p><p style=""><span style="font-size: 14.4px;">We know your data is important to you. So we build the drive inside to our demanding requirements for durability, shock tolerance, and long-term reliability. Then we protect the drive with a durable enclosure designed for style and protection.</span></p><p style=""><span style="font-size: 14.4px;"><strong>TECHNICAL SPECIFICATIONS</strong></span></p><p style=""><span style="font-size: 14.4px;">Formatted NTFS for Windows® 10, Windows 8.1, Windows 7. Reformatting may be required for other operating systems. Compatibility may vary depending on user’s hardware configuration and operating system.</span></p><br>', 1, 1, 0, 0, 0, 0, CAST(69.99000 AS Numeric(18, 5)), CAST(54.99000 AS Numeric(18, 5)), NULL, NULL, NULL, 1, 0, CAST(N'2019-07-04T12:44:12.007' AS DateTime), CAST(N'2019-07-04T12:48:14.563' AS DateTime), NULL, 0, 0, 1, 0, 5, NULL, 1, 0, 0, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 0, 0, 0, 9, 0, 0, 0, 0, NULL, 0, 0)

INSERT [Product] ([Name], [Summary], [Description], [IsShippable], [IsDownloadable], [IsFeatured], [IsVisibleIndividually], [TrackInventory], [CanOrderWhenOutOfStock], [ComparePrice], [Price], [Sku], [Gtin], [Mpn], [MinimumPurchaseQuantity], [MaximumPurchaseQuantity], [CreatedOn], [UpdatedOn], [ParentProductId], [DisplayOrder], [ChargeTaxes], [Published], [Deleted], [ManufacturerId], [TaxId], [HasVariants], [ReviewsDisabled], [PopularityIndex], [PackageWeight], [PackageWeightUnit], [PackageWidth], [PackageWidthUnit], [PackageHeight], [PackageHeightUnit], [PackageLength], [PackageLengthUnit], [AdditionalShippingCharge], [IndividuallyShipped], [AllowReturns], [DaysForReturn], [Id], [ProductType], [ProductSaleType], [SubscriptionCycle], [CycleCount], [TrialDays], [RequireLoginToPurchase], [RequireLoginToViewPrice]) VALUES (N'My Passport X', N'<p><span style="font-size: 14.4px;">Connect this portable and powerful drive to immediately add storage capacity and expand your console or PC gaming experience.</span></p>', N'<p><span style="font-size: 14.4px;"><strong>More Zombies to Conquer</strong></span></p><p><span style="font-size: 14.4px;">The My Passport X drive is the perfect way to store and expand your gaming experience.</span></p><p><span style="font-size: 14.4px;"><strong>Play Anywhere</strong></span></p><p><span style="font-size: 14.4px;">Compact design allows you to take your gaming lifestyle with you – and look good doing it.1</span></p><p><span style="font-size: 14.4px;"><strong>Performance Tweaked</strong></span></p><p><span style="font-size: 14.4px;">It’s like giving your gaming avatar super-lifting strength and lightning-quick gaming speed.</span></p>', 1, 0, 0, 0, 1, 0, NULL, CAST(89.99000 AS Numeric(18, 5)), NULL, NULL, NULL, 1, 0, CAST(N'2019-07-04T13:30:02.120' AS DateTime), CAST(N'2019-07-04T13:30:27.297' AS DateTime), NULL, 0, 0, 1, 0, 5, NULL, 1, 0, 0, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 0, 0, 0, 10, 0, 0, 0, 0, NULL, 0, 0)

INSERT [Product] ([Name], [Summary], [Description], [IsShippable], [IsDownloadable], [IsFeatured], [IsVisibleIndividually], [TrackInventory], [CanOrderWhenOutOfStock], [ComparePrice], [Price], [Sku], [Gtin], [Mpn], [MinimumPurchaseQuantity], [MaximumPurchaseQuantity], [CreatedOn], [UpdatedOn], [ParentProductId], [DisplayOrder], [ChargeTaxes], [Published], [Deleted], [ManufacturerId], [TaxId], [HasVariants], [ReviewsDisabled], [PopularityIndex], [PackageWeight], [PackageWeightUnit], [PackageWidth], [PackageWidthUnit], [PackageHeight], [PackageHeightUnit], [PackageLength], [PackageLengthUnit], [AdditionalShippingCharge], [IndividuallyShipped], [AllowReturns], [DaysForReturn], [Id], [ProductType], [ProductSaleType], [SubscriptionCycle], [CycleCount], [TrialDays], [RequireLoginToPurchase], [RequireLoginToViewPrice]) VALUES (N'Samsung LED Smart TV', N'<p><span style="font-size: 14.4px;">Make blurry images a thing of the past. Digital Clean View improves your content no matter the quality. See impressive colour with Wide Colour Enhancer. Improve the quality of any image, uncover hidden details and see colours as they were meant to be seen.</span></p>', N'<p><span style="font-size: 14.4px;"><strong>Share the Moment</strong></span></p><p><span style="font-size: 14.4px;">Live stream what you like, share it and entertain everyone. With live cast, you can broadcast your experiences from anywhere at any time, right on to your Samsung Smart TV.</span></p><p><span style="font-size: 14.4px;"><strong>Your Entertainment, on Any Screen</strong></span></p><p><span style="font-size: 14.4px;">Let your smartphone** and Smart TV work together to maximize your entertainment. Play music and videos from your phone to your TV for a big screen experience or carry your TV content to your phone for personal enjoyment.</span></p><p><span style="font-size: 14.4px;"><strong>Single Access for all Your Content</strong></span></p><p><span style="font-size: 14.4px;">The Smart Hub provides a single access to live TV, apps and other sources. You can browse content while watching TV and check out the thumbnail previews before diving in.</span></p><p><span style="font-size: 14.4px;"><strong>Immersive Sound</strong></span></p><p><span style="font-size: 14.4px;">Great sound for great entertainment, and no need for a separate system. Dive into your content with more immersive audio. Beamforming technology and 4Ch 40W sound surrounds with the dynamism of a concert hall.</span></p><p><span style="font-size: 14.4px;"><strong>HDR</strong></span></p><p><span style="font-size: 14.4px;">Watch HDR content with better clarity and detailed colour expression. Samsung HD TV gives you more accurate details in bright and dark scenes.</span></p><p><span style="font-size: 14.4px;"><strong>Ultra Clean View</strong></span></p><p><span style="font-size: 14.4px;">Analyzing original content with an advanced algorithm, ultra clean view gives you higher quality images with less distortion. Enjoy the clear picture.</span></p><p><span style="font-size: 14.4px;"><strong>PurColour</strong></span></p><p><span style="font-size: 14.4px;">Watch your favourite content with natural colours that deliver details as crisp as the real thing. Get a more colourful viewing experience.</span></p><p><span style="font-size: 14.4px;"><strong>Micro Dimming Pro</strong></span></p><p><span style="font-size: 14.4px;">Experience shadow detail and colour. Dividing the screen into zones, micro dimming pro analyzes each one for deeper blacks and purer whites.</span></p><p><span style="font-size: 14.4px;"><strong>SmartThings App, Just One App for all</strong></span></p><p><span style="font-size: 14.4px;">Availability of the feature and Graphic User Interface (GUI) may vary by region. Check before use.The SmartThings app also offers features such as remote control, and mirror screen.</span></p>', 1, 0, 0, 0, 1, 0, CAST(369.00000 AS Numeric(18, 5)), CAST(329.00000 AS Numeric(18, 5)), N'UA32N4310', NULL, NULL, 1, 0, CAST(N'2019-07-04T14:33:48.143' AS DateTime), CAST(N'2019-07-04T14:42:17.227' AS DateTime), NULL, 0, 0, 1, 0, 6, NULL, 0, 0, 0, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(200.00000 AS Numeric(18, 5)), 1, 0, 0, 11, 0, 0, 0, 0, NULL, 0, 0)

INSERT [Product] ([Name], [Summary], [Description], [IsShippable], [IsDownloadable], [IsFeatured], [IsVisibleIndividually], [TrackInventory], [CanOrderWhenOutOfStock], [ComparePrice], [Price], [Sku], [Gtin], [Mpn], [MinimumPurchaseQuantity], [MaximumPurchaseQuantity], [CreatedOn], [UpdatedOn], [ParentProductId], [DisplayOrder], [ChargeTaxes], [Published], [Deleted], [ManufacturerId], [TaxId], [HasVariants], [ReviewsDisabled], [PopularityIndex], [PackageWeight], [PackageWeightUnit], [PackageWidth], [PackageWidthUnit], [PackageHeight], [PackageHeightUnit], [PackageLength], [PackageLengthUnit], [AdditionalShippingCharge], [IndividuallyShipped], [AllowReturns], [DaysForReturn], [Id], [ProductType], [ProductSaleType], [SubscriptionCycle], [CycleCount], [TrialDays], [RequireLoginToPurchase], [RequireLoginToViewPrice]) VALUES (N'Sony Bravia Full HD LED Smart TV', N'<p><span style="font-size: 14.4px;">X-Reality PRO picture processing upscales every pixel for exceptional Full HD clarity. As frames are analysed, each scene is matched with our special image database to refine images and reduce noise. See how the architecture in the building is enhanced with extra details.</span></p>', N'<p><span style="font-size: 14.4px;"><strong>Discover thrilling HDR entertainment</strong></span></p><p><span style="font-size: 14.4px;">This TV brings you the excitement of movies and games in vividly detailed HDR. It handles a variety of HDR formats, including HDR10 and Hybrid Log-Gamma.</span></p><p style=""><span style="font-size: 14.4px;"><strong>Access YouTube™ instantly with one click</strong></span></p><p style=""><span style="font-size: 14.4px;">Go straight to YouTube and enjoy all your favourite videos. We have made watching clips ultra-fast on this internet-ready TV with YouTube and included a YouTube button on the remote control for easy browsing</span></p><p style=""><span style="font-size: 14.4px;"><strong>Power your entertainment with deep bass</strong></span></p><p style=""><span style="font-size: 14.4px;">Take your place in the front row. We''ve built a subwoofer into this TV so you can feel right at the heart of the action when watching concerts and movies. Hear deep bass riffs, soaring vocals and powerful soundtracks.</span></p><p style=""><span style="font-size: 14.4px;"><strong>Made to listen</strong></span></p><p style=""><span style="font-size: 14.4px;">Make your listening as lifelike as your viewing. ClearAudio+ fine tunes TV sound offers an immersive, emotionally enriching experience that seems to surround you. Hear music and dialogue with greater clarity and separation, whatever you''re watching.</span></p><p style=""><span style="font-size: 14.4px;"><strong>A smarter way to enjoy your smartphone</strong></span></p><p style=""><span style="font-size: 14.4px;">Take all the things you love on your smartphone or USB drive and enjoy them in beautiful detail on your large-screen TV. Smart Plug and Play lets you easily share videos, photos and music from multiple devices</span></p><p style=""><span style="font-size: 14.4px;"><strong>Performs in the toughest conditions</strong></span></p><p style=""><span style="font-size: 14.4px;">Enjoy your entertainment, without any disruption. This TV with X-Protection PRO is extremely durable and protected from dust, humidity, power surges and even lightning strikes.</span></p>', 1, 1, 0, 0, 1, 0, NULL, CAST(439.00000 AS Numeric(18, 5)), N'KLV-32W672F', NULL, NULL, 1, 1, CAST(N'2019-07-04T14:45:51.763' AS DateTime), CAST(N'2019-12-26T08:56:48.993' AS DateTime), NULL, 0, 0, 1, 0, 7, NULL, 0, 0, 0, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 1, 0, 0, 12, 0, 0, 1, 0, NULL, 1, 1)

INSERT [Product] ([Name], [Summary], [Description], [IsShippable], [IsDownloadable], [IsFeatured], [IsVisibleIndividually], [TrackInventory], [CanOrderWhenOutOfStock], [ComparePrice], [Price], [Sku], [Gtin], [Mpn], [MinimumPurchaseQuantity], [MaximumPurchaseQuantity], [CreatedOn], [UpdatedOn], [ParentProductId], [DisplayOrder], [ChargeTaxes], [Published], [Deleted], [ManufacturerId], [TaxId], [HasVariants], [ReviewsDisabled], [PopularityIndex], [PackageWeight], [PackageWeightUnit], [PackageWidth], [PackageWidthUnit], [PackageHeight], [PackageHeightUnit], [PackageLength], [PackageLengthUnit], [AdditionalShippingCharge], [IndividuallyShipped], [AllowReturns], [DaysForReturn], [Id], [ProductType], [ProductSaleType], [SubscriptionCycle], [CycleCount], [TrialDays], [RequireLoginToPurchase], [RequireLoginToViewPrice]) VALUES (N'Kaspersky Internet Security', N'<p><span style="font-size: 14.4px;">When you go online shopping or banking – we protect your money &amp; account details… when you socialize – we safeguard your identity… when you surf – we prevent attacks… when you download or stream – we block infected files.</span></p>', N'<p><span style="font-size: 14.4px;">Whatever you do in your digital life – our premium protection is here to help you protect it all.</span></p><p></p><ul><li><span style="font-size: 14.4px;">Protects against attacks, ransomware &amp; more</span></li><li>Protects your privacy &amp; personal information</li><li>Protects money when you bank &amp; shop online</li></ul><p></p>', 1, 1, 0, 0, 0, 0, NULL, CAST(39.99000 AS Numeric(18, 5)), NULL, NULL, NULL, 1, 0, CAST(N'2019-07-05T07:51:04.043' AS DateTime), CAST(N'2019-07-05T08:04:21.607' AS DateTime), NULL, 0, 0, 1, 0, 8, NULL, 1, 0, 0, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 0, 0, 0, 13, 0, 0, 0, 0, NULL, 0, 0)

INSERT [Product] ([Name], [Summary], [Description], [IsShippable], [IsDownloadable], [IsFeatured], [IsVisibleIndividually], [TrackInventory], [CanOrderWhenOutOfStock], [ComparePrice], [Price], [Sku], [Gtin], [Mpn], [MinimumPurchaseQuantity], [MaximumPurchaseQuantity], [CreatedOn], [UpdatedOn], [ParentProductId], [DisplayOrder], [ChargeTaxes], [Published], [Deleted], [ManufacturerId], [TaxId], [HasVariants], [ReviewsDisabled], [PopularityIndex], [PackageWeight], [PackageWeightUnit], [PackageWidth], [PackageWidthUnit], [PackageHeight], [PackageHeightUnit], [PackageLength], [PackageLengthUnit], [AdditionalShippingCharge], [IndividuallyShipped], [AllowReturns], [DaysForReturn], [Id], [ProductType], [ProductSaleType], [SubscriptionCycle], [CycleCount], [TrialDays], [RequireLoginToPurchase], [RequireLoginToViewPrice]) VALUES (N'Microsoft Office Home and Business 2016', NULL, N'<p><span style="font-size: 14.4px;">Microsoft Office 2016 helps you to do your best work - anywhere, anytime and with anyone. New, modern versions of the classic desktop applications, Word, Excel, PowerPoint, Outlook, and OneNote, are built for maximum productivity. You''ll quickly produce professional documents with rich authoring features, design controls for pixel-perfect layouts and intuitive tools to help you make the most of your data. You''ll have access to your docs in the cloud whenever you need them. With your documents stored online, it''s easy to get your team on the same page. Share, present and work together on projects with built in team collaboration tools across the suite.&nbsp;</span></p>', 0, 1, 0, 0, 0, 0, NULL, CAST(229.99000 AS Numeric(18, 5)), NULL, NULL, NULL, 1, 0, CAST(N'2019-07-05T08:13:21.567' AS DateTime), CAST(N'2019-07-05T08:19:54.770' AS DateTime), NULL, 0, 0, 1, 0, 2, NULL, 0, 0, 0, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 1, CAST(0.00000 AS Numeric(18, 5)), 0, 0, 0, 14, 0, 0, 0, 0, NULL, 0, 0)

SET IDENTITY_INSERT [Product] OFF

SET IDENTITY_INSERT [ProductCategory] ON 


INSERT [ProductCategory] ([ProductId], [CategoryId], [DisplayOrder], [Id]) VALUES (2, 2, 0, 1)

INSERT [ProductCategory] ([ProductId], [CategoryId], [DisplayOrder], [Id]) VALUES (3, 2, 0, 2)

INSERT [ProductCategory] ([ProductId], [CategoryId], [DisplayOrder], [Id]) VALUES (4, 5, 0, 3)

INSERT [ProductCategory] ([ProductId], [CategoryId], [DisplayOrder], [Id]) VALUES (5, 6, 0, 4)

INSERT [ProductCategory] ([ProductId], [CategoryId], [DisplayOrder], [Id]) VALUES (6, 6, 0, 5)

INSERT [ProductCategory] ([ProductId], [CategoryId], [DisplayOrder], [Id]) VALUES (7, 7, 0, 6)

INSERT [ProductCategory] ([ProductId], [CategoryId], [DisplayOrder], [Id]) VALUES (8, 8, 0, 7)

INSERT [ProductCategory] ([ProductId], [CategoryId], [DisplayOrder], [Id]) VALUES (9, 9, 0, 8)

INSERT [ProductCategory] ([ProductId], [CategoryId], [DisplayOrder], [Id]) VALUES (10, 9, 0, 9)

INSERT [ProductCategory] ([ProductId], [CategoryId], [DisplayOrder], [Id]) VALUES (11, 12, 0, 10)

INSERT [ProductCategory] ([ProductId], [CategoryId], [DisplayOrder], [Id]) VALUES (12, 12, 0, 11)

INSERT [ProductCategory] ([ProductId], [CategoryId], [DisplayOrder], [Id]) VALUES (13, 14, 0, 12)

INSERT [ProductCategory] ([ProductId], [CategoryId], [DisplayOrder], [Id]) VALUES (14, 15, 0, 13)

SET IDENTITY_INSERT [ProductCategory] OFF

SET IDENTITY_INSERT [Consent] ON 


INSERT [Consent] ([Title], [Description], [IsPluginSpecificConsent], [PluginSystemName], [IsRequired], [DisplayOrder], [LanguageCultureCode], [EnableLogging], [OneTimeSelection], [Published], [ConsentGroupId], [Id]) VALUES (N'I agree to the usage terms.', N'The website usage is governed by our <a href=''/terms-conditions''>terms and conditions</a> and <a href=''/privacy-policy''>privacy policy</a>.', 0, NULL, 1, 0, NULL, 1, 1, 1, 0, 1)

INSERT [Consent] ([Title], [Description], [IsPluginSpecificConsent], [PluginSystemName], [IsRequired], [DisplayOrder], [LanguageCultureCode], [EnableLogging], [OneTimeSelection], [Published], [ConsentGroupId], [Id]) VALUES (N'I agree to accept Essential Cookies on my browser/app', N'These cookies are strictly necessary to provide you with services available through our websites and to use some of its features, such as access to secure areas.', 0, NULL, 1, 0, NULL, 0, 0, 1, 1, 2)

INSERT [Consent] ([Title], [Description], [IsPluginSpecificConsent], [PluginSystemName], [IsRequired], [DisplayOrder], [LanguageCultureCode], [EnableLogging], [OneTimeSelection], [Published], [ConsentGroupId], [Id]) VALUES (N'I agree to accept Preference Cookies on my browser/app', N'Preference cookies enable a website to remember information that changes the way the website behaves or looks, like your preferred language or the region that you are in.', 0, NULL, 0, 1, NULL, 1, 0, 1, 1, 3)

INSERT [Consent] ([Title], [Description], [IsPluginSpecificConsent], [PluginSystemName], [IsRequired], [DisplayOrder], [LanguageCultureCode], [EnableLogging], [OneTimeSelection], [Published], [ConsentGroupId], [Id]) VALUES (N'I agree to accept Statistical Cookies on my browser/app', N'Statistic cookies help website owners to understand how visitors interact with websites by collecting and reporting information anonymously.', 0, NULL, 0, 2, NULL, 1, 0, 1, 1, 4)

INSERT [Consent] ([Title], [Description], [IsPluginSpecificConsent], [PluginSystemName], [IsRequired], [DisplayOrder], [LanguageCultureCode], [EnableLogging], [OneTimeSelection], [Published], [ConsentGroupId], [Id]) VALUES (N'I would like to receive updates on my email regarding promotions', N'Your email will not be shared with any third party websites without your explicit consent. You can always change your preferences anytime by visiting this page.', 0, NULL, 0, 0, NULL, 1, 1, 1, 2, 5)

SET IDENTITY_INSERT [Consent] OFF

SET IDENTITY_INSERT [Media] ON 


INSERT [Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id]) VALUES (N'Image 1.jpg', N'Image 1.jpg', NULL, N'Image 1.jpg', N'/Content/Uploads/Serves/Image 1_0_0.jpg', N'/Content/Uploads/Serves/Image 1_150_150.jpg', N'image/jpeg', 0, CAST(N'2019-07-03T09:50:41.857' AS DateTime), 0, 1, 1)

INSERT [Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id]) VALUES (N'Image 2.jpg', N'Image 2_WNTEYLJATT.jpg', NULL, N'Image 2.jpg', N'/Content/Uploads/Serves/Image 2_WNTEYLJATT_0_0.jpg', N'/Content/Uploads/Serves/Image 2_WNTEYLJATT_150_150.jpg', N'image/jpeg', 0, CAST(N'2019-07-03T10:00:18.413' AS DateTime), 0, 2, 6)

INSERT [Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id]) VALUES (N'Image 3.jpg', N'Image 3.jpg', NULL, N'Image 3.jpg', N'/Content/Uploads/Serves/Image 3_0_0.jpg', N'/Content/Uploads/Serves/Image 3_150_150.jpg', N'image/jpeg', 0, CAST(N'2019-07-03T10:51:26.930' AS DateTime), 0, 0, 7)

INSERT [Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id]) VALUES (N'Image 1.jpg', N'Image 1_TAODLLZOOB.jpg', NULL, N'Image 1.jpg', N'/Content/Uploads/Serves/Image 1_TAODLLZOOB_0_0.jpg', N'/Content/Uploads/Serves/Image 1_TAODLLZOOB_150_150.jpg', N'image/jpeg', 0, CAST(N'2019-07-03T12:04:41.773' AS DateTime), 0, 0, 8)

INSERT [Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id]) VALUES (N'Image 2.jpg', N'Image 2_UNSJHCQYWE.jpg', NULL, N'Image 2.jpg', N'/Content/Uploads/Serves/Image 2_UNSJHCQYWE_0_0.jpg', N'/Content/Uploads/Serves/Image 2_UNSJHCQYWE_150_150.jpg', N'image/jpeg', 0, CAST(N'2019-07-03T12:04:47.057' AS DateTime), 0, 0, 9)

INSERT [Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id]) VALUES (N'Image 3.jpg', N'Image 3_GUJSNHXUJX.jpg', NULL, N'Image 3.jpg', N'/Content/Uploads/Serves/Image 3_GUJSNHXUJX_0_0.jpg', N'/Content/Uploads/Serves/Image 3_GUJSNHXUJX_150_150.jpg', N'image/jpeg', 0, CAST(N'2019-07-03T12:04:51.373' AS DateTime), 0, 0, 10)

INSERT [Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id]) VALUES (N'Image 4.jpg', N'Image 4.jpg', NULL, N'Image 4.jpg', N'/Content/Uploads/Serves/Image 4_0_0.jpg', N'/Content/Uploads/Serves/Image 4_150_150.jpg', N'image/jpeg', 0, CAST(N'2019-07-03T12:04:56.243' AS DateTime), 0, 0, 11)

INSERT [Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id]) VALUES (N'Image04.jpeg', N'Image04.jpeg', NULL, N'Image04.jpeg', N'/Content/Uploads/Serves/Image04_0_0.jpeg', N'/Content/Uploads/Serves/Image04_150_150.jpeg', N'image/jpeg', 0, CAST(N'2019-07-03T14:07:38.047' AS DateTime), 0, 4, 12)

INSERT [Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id]) VALUES (N'Image05.jpeg', N'Image05.jpeg', NULL, N'Image05.jpeg', N'/Content/Uploads/Serves/Image05_0_0.jpeg', N'/Content/Uploads/Serves/Image05_150_150.jpeg', N'image/jpeg', 0, CAST(N'2019-07-03T14:07:38.050' AS DateTime), 0, 3, 13)

INSERT [Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id]) VALUES (N'Image03.jpeg', N'Image03.jpeg', NULL, N'Image03.jpeg', N'/Content/Uploads/Serves/Image03_0_0.jpeg', N'/Content/Uploads/Serves/Image03_150_150.jpeg', N'image/jpeg', 0, CAST(N'2019-07-03T14:07:38.050' AS DateTime), 0, 2, 14)

INSERT [Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id]) VALUES (N'Image02.jpeg', N'Image02.jpeg', NULL, N'Image02.jpeg', N'/Content/Uploads/Serves/Image02_0_0.jpeg', N'/Content/Uploads/Serves/Image02_150_150.jpeg', N'image/jpeg', 0, CAST(N'2019-07-03T14:07:38.047' AS DateTime), 0, 1, 15)

INSERT [Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id]) VALUES (N'Image01.jpeg', N'Image01.jpeg', NULL, N'Image01.jpeg', N'/Content/Uploads/Serves/Image01_0_0.jpeg', N'/Content/Uploads/Serves/Image01_150_150.jpeg', N'image/jpeg', 0, CAST(N'2019-07-03T14:07:38.050' AS DateTime), 0, 0, 16)

INSERT [Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id]) VALUES (N'Image1.jpg', N'Image1.jpg', NULL, N'Image1.jpg', N'/Content/Uploads/Serves/Image1_0_0.jpg', N'/Content/Uploads/Serves/Image1_150_150.jpg', N'image/jpeg', 0, CAST(N'2019-07-04T07:31:02.417' AS DateTime), 0, 0, 17)

INSERT [Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id]) VALUES (N'Image2.jpg', N'Image2.jpg', NULL, N'Image2.jpg', N'/Content/Uploads/Serves/Image2_0_0.jpg', N'/Content/Uploads/Serves/Image2_150_150.jpg', N'image/jpeg', 0, CAST(N'2019-07-04T07:31:02.417' AS DateTime), 0, 0, 18)

INSERT [Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id]) VALUES (N'Image 5.jpg', N'Image 5.jpg', NULL, N'Image 5.jpg', N'/Content/Uploads/Serves/Image 5_0_0.jpg', N'/Content/Uploads/Serves/Image 5_150_150.jpg', N'image/jpeg', 0, CAST(N'2019-07-04T11:04:44.957' AS DateTime), 0, 0, 19)

INSERT [Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id]) VALUES (N'Image 4.jpg', N'Image 4_CWWUEKKCXD.jpg', NULL, N'Image 4.jpg', N'/Content/Uploads/Serves/Image 4_CWWUEKKCXD_0_0.jpg', N'/Content/Uploads/Serves/Image 4_CWWUEKKCXD_150_150.jpg', N'image/jpeg', 0, CAST(N'2019-07-04T11:04:44.953' AS DateTime), 0, 0, 20)

INSERT [Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id]) VALUES (N'Image 3.jpg', N'Image 3_WLRANZVQMZ.jpg', NULL, N'Image 3.jpg', N'/Content/Uploads/Serves/Image 3_WLRANZVQMZ_0_0.jpg', N'/Content/Uploads/Serves/Image 3_WLRANZVQMZ_150_150.jpg', N'image/jpeg', 0, CAST(N'2019-07-04T11:04:44.957' AS DateTime), 0, 0, 21)

INSERT [Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id]) VALUES (N'Image 1.jpg', N'Image 1_LTWAQZCKAW.jpg', NULL, N'Image 1.jpg', N'/Content/Uploads/Serves/Image 1_LTWAQZCKAW_0_0.jpg', N'/Content/Uploads/Serves/Image 1_LTWAQZCKAW_150_150.jpg', N'image/jpeg', 0, CAST(N'2019-07-04T11:04:44.953' AS DateTime), 0, 0, 22)

INSERT [Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id]) VALUES (N'Image 2.jpg', N'Image 2_DEYNQMKCRV.jpg', NULL, N'Image 2.jpg', N'/Content/Uploads/Serves/Image 2_DEYNQMKCRV_0_0.jpg', N'/Content/Uploads/Serves/Image 2_DEYNQMKCRV_150_150.jpg', N'image/jpeg', 0, CAST(N'2019-07-04T11:04:44.953' AS DateTime), 0, 0, 23)

INSERT [Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id]) VALUES (N'Image 6.jpg', N'Image 6.jpg', NULL, N'Image 6.jpg', N'/Content/Uploads/Serves/Image 6_0_0.jpg', N'/Content/Uploads/Serves/Image 6_150_150.jpg', N'image/jpeg', 0, CAST(N'2019-07-04T11:04:44.957' AS DateTime), 0, 0, 24)

INSERT [Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id]) VALUES (N'Image 1.jpg', N'Image 1_JVYSWCDWSU.jpg', NULL, N'Image 1.jpg', N'/Content/Uploads/Serves/Image 1_JVYSWCDWSU_0_0.jpg', N'/Content/Uploads/Serves/Image 1_JVYSWCDWSU_150_150.jpg', N'image/jpeg', 0, CAST(N'2019-07-04T12:00:56.773' AS DateTime), 0, 0, 25)

INSERT [Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id]) VALUES (N'Image2.jpg', N'Image2_YDYWECTEVO.jpg', NULL, N'Image2.jpg', N'/Content/Uploads/Serves/Image2_YDYWECTEVO_0_0.jpg', N'/Content/Uploads/Serves/Image2_YDYWECTEVO_150_150.jpg', N'image/jpeg', 0, CAST(N'2019-07-04T12:22:26.557' AS DateTime), 0, 1, 26)

INSERT [Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id]) VALUES (N'Image1.jpg', N'Image1_DWABHQZFMK.jpg', NULL, N'Image1.jpg', N'/Content/Uploads/Serves/Image1_DWABHQZFMK_0_0.jpg', N'/Content/Uploads/Serves/Image1_DWABHQZFMK_150_150.jpg', N'image/jpeg', 0, CAST(N'2019-07-04T12:22:26.590' AS DateTime), 0, 0, 27)

INSERT [Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id]) VALUES (N'Image 2.jpg', N'Image 2_DLFJYPAIVH.jpg', NULL, N'Image 2.jpg', N'/Content/Uploads/Serves/Image 2_DLFJYPAIVH_0_0.jpg', N'/Content/Uploads/Serves/Image 2_DLFJYPAIVH_150_150.jpg', N'image/jpeg', 0, CAST(N'2019-07-04T12:49:22.970' AS DateTime), 0, 1, 28)

INSERT [Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id]) VALUES (N'Image 1.jpg', N'Image 1_MWFFVCYQSU.jpg', NULL, N'Image 1.jpg', N'/Content/Uploads/Serves/Image 1_MWFFVCYQSU_0_0.jpg', N'/Content/Uploads/Serves/Image 1_MWFFVCYQSU_150_150.jpg', N'image/jpeg', 0, CAST(N'2019-07-04T12:49:22.973' AS DateTime), 0, 0, 29)

INSERT [Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id]) VALUES (N'Image 1.jpg', N'Image 1_LSFCSKSPAD.jpg', NULL, N'Image 1.jpg', N'/Content/Uploads/Serves/Image 1_LSFCSKSPAD_0_0.jpg', N'/Content/Uploads/Serves/Image 1_LSFCSKSPAD_150_150.jpg', N'image/jpeg', 0, CAST(N'2019-07-04T13:32:18.807' AS DateTime), 0, 1, 30)

INSERT [Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id]) VALUES (N'Image 2.jpg', N'Image 2_YAMQBGYQZM.jpg', NULL, N'Image 2.jpg', N'/Content/Uploads/Serves/Image 2_YAMQBGYQZM_0_0.jpg', N'/Content/Uploads/Serves/Image 2_YAMQBGYQZM_150_150.jpg', N'image/jpeg', 0, CAST(N'2019-07-04T13:32:18.803' AS DateTime), 0, 0, 31)

INSERT [Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id]) VALUES (N'Image 5.jpg', N'Image 5_BHLFRSXROL.jpg', NULL, N'Image 5.jpg', N'/Content/Uploads/Serves/Image 5_BHLFRSXROL_0_0.jpg', N'/Content/Uploads/Serves/Image 5_BHLFRSXROL_150_150.jpg', N'image/jpeg', 0, CAST(N'2019-07-04T14:36:08.053' AS DateTime), 0, 4, 32)

INSERT [Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id]) VALUES (N'Image 6.jpg', N'Image 6_JTGUVSJLPZ.jpg', NULL, N'Image 6.jpg', N'/Content/Uploads/Serves/Image 6_JTGUVSJLPZ_0_0.jpg', N'/Content/Uploads/Serves/Image 6_JTGUVSJLPZ_150_150.jpg', N'image/jpeg', 0, CAST(N'2019-07-04T14:36:08.020' AS DateTime), 0, 5, 33)

INSERT [Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id]) VALUES (N'Image 1.jpg', N'Image 1_ANVRPZEXAF.jpg', NULL, N'Image 1.jpg', N'/Content/Uploads/Serves/Image 1_ANVRPZEXAF_0_0.jpg', N'/Content/Uploads/Serves/Image 1_ANVRPZEXAF_150_150.jpg', N'image/jpeg', 0, CAST(N'2019-07-04T14:36:08.070' AS DateTime), 0, 0, 34)

INSERT [Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id]) VALUES (N'Image 4.jpg', N'Image 4_RBXJYKGZJZ.jpg', NULL, N'Image 4.jpg', N'/Content/Uploads/Serves/Image 4_RBXJYKGZJZ_0_0.jpg', N'/Content/Uploads/Serves/Image 4_RBXJYKGZJZ_150_150.jpg', N'image/jpeg', 0, CAST(N'2019-07-04T14:36:08.107' AS DateTime), 0, 2, 35)

INSERT [Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id]) VALUES (N'Image 2.jpg', N'Image 2_AHTRZQOGGE.jpg', NULL, N'Image 2.jpg', N'/Content/Uploads/Serves/Image 2_AHTRZQOGGE_0_0.jpg', N'/Content/Uploads/Serves/Image 2_AHTRZQOGGE_150_150.jpg', N'image/jpeg', 0, CAST(N'2019-07-04T14:36:08.107' AS DateTime), 0, 3, 36)

INSERT [Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id]) VALUES (N'Image 3.jpg', N'Image 3_PFCHRAQZJK.jpg', NULL, N'Image 3.jpg', N'/Content/Uploads/Serves/Image 3_PFCHRAQZJK_0_0.jpg', N'/Content/Uploads/Serves/Image 3_PFCHRAQZJK_150_150.jpg', N'image/jpeg', 0, CAST(N'2019-07-04T14:36:08.170' AS DateTime), 0, 1, 37)

INSERT [Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id]) VALUES (N'Image 1.jpg', N'Image 1_LNREOZMAVI.jpg', NULL, N'Image 1.jpg', N'/Content/Uploads/Serves/Image 1_LNREOZMAVI_0_0.jpg', N'/Content/Uploads/Serves/Image 1_LNREOZMAVI_150_150.jpg', N'image/jpeg', 0, CAST(N'2019-07-04T14:48:13.127' AS DateTime), 0, 0, 38)

INSERT [Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id]) VALUES (N'Image 2.jpg', N'Image 2_EZHSXFNTUE.jpg', NULL, N'Image 2.jpg', N'/Content/Uploads/Serves/Image 2_EZHSXFNTUE_0_0.jpg', N'/Content/Uploads/Serves/Image 2_EZHSXFNTUE_150_150.jpg', N'image/jpeg', 0, CAST(N'2019-07-04T14:48:13.107' AS DateTime), 0, 0, 39)

INSERT [Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id]) VALUES (N'Image 4.jpg', N'Image 4_GMXPEIMIUM.jpg', NULL, N'Image 4.jpg', N'/Content/Uploads/Serves/Image 4_GMXPEIMIUM_0_0.jpg', N'/Content/Uploads/Serves/Image 4_GMXPEIMIUM_150_150.jpg', N'image/jpeg', 0, CAST(N'2019-07-04T14:48:13.113' AS DateTime), 0, 0, 40)

INSERT [Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id]) VALUES (N'Image 3.jpg', N'Image 3_GEPCOYSQCI.jpg', NULL, N'Image 3.jpg', N'/Content/Uploads/Serves/Image 3_GEPCOYSQCI_0_0.jpg', N'/Content/Uploads/Serves/Image 3_GEPCOYSQCI_150_150.jpg', N'image/jpeg', 0, CAST(N'2019-07-04T14:48:13.123' AS DateTime), 0, 0, 41)

INSERT [Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id]) VALUES (N'Image 1.png', N'Image 1.png', NULL, N'Image 1.png', N'/Content/Uploads/Serves/Image 1_0_0.png', N'/Content/Uploads/Serves/Image 1_150_150.png', N'image/png', 0, CAST(N'2019-07-05T07:53:12.017' AS DateTime), 0, 0, 42)

INSERT [Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id]) VALUES (N'Image 4.png', N'Image 4.png', NULL, N'Image 4.png', N'/Content/Uploads/Serves/Image 4_0_0.png', N'/Content/Uploads/Serves/Image 4_150_150.png', N'image/png', 0, CAST(N'2019-07-05T07:53:12.017' AS DateTime), 0, 0, 43)

INSERT [Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id]) VALUES (N'Image 2.png', N'Image 2.png', NULL, N'Image 2.png', N'/Content/Uploads/Serves/Image 2_0_0.png', N'/Content/Uploads/Serves/Image 2_150_150.png', N'image/png', 0, CAST(N'2019-07-05T07:53:12.017' AS DateTime), 0, 0, 44)

INSERT [Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id]) VALUES (N'Image 3.png', N'Image 3.png', NULL, N'Image 3.png', N'/Content/Uploads/Serves/Image 3_0_0.png', N'/Content/Uploads/Serves/Image 3_150_150.png', N'image/png', 0, CAST(N'2019-07-05T07:53:12.017' AS DateTime), 0, 0, 45)

INSERT [Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id]) VALUES (N'Image 3.jpg', N'Image 3_ROYYRYTFLW.jpg', NULL, N'Image 3.jpg', N'/Content/Uploads/Serves/Image 3_ROYYRYTFLW_0_0.jpg', N'/Content/Uploads/Serves/Image 3_ROYYRYTFLW_150_150.jpg', N'image/jpeg', 0, CAST(N'2019-07-05T08:16:06.217' AS DateTime), 0, 1, 46)

INSERT [Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id]) VALUES (N'Image 2.jpg', N'Image 2_ADBKHWFCRY.jpg', NULL, N'Image 2.jpg', N'/Content/Uploads/Serves/Image 2_ADBKHWFCRY_0_0.jpg', N'/Content/Uploads/Serves/Image 2_ADBKHWFCRY_150_150.jpg', N'image/jpeg', 0, CAST(N'2019-07-05T08:16:06.217' AS DateTime), 0, 5, 47)

INSERT [Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id]) VALUES (N'Image 4.jpg', N'Image 4_TGGFMREMMI.jpg', NULL, N'Image 4.jpg', N'/Content/Uploads/Serves/Image 4_TGGFMREMMI_0_0.jpg', N'/Content/Uploads/Serves/Image 4_TGGFMREMMI_150_150.jpg', N'image/jpeg', 0, CAST(N'2019-07-05T08:16:06.227' AS DateTime), 0, 2, 48)

INSERT [Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id]) VALUES (N'Image 5.jpg', N'Image 5_NWKBRKNEWR.jpg', NULL, N'Image 5.jpg', N'/Content/Uploads/Serves/Image 5_NWKBRKNEWR_0_0.jpg', N'/Content/Uploads/Serves/Image 5_NWKBRKNEWR_150_150.jpg', N'image/jpeg', 0, CAST(N'2019-07-05T08:16:06.223' AS DateTime), 0, 4, 49)

INSERT [Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id]) VALUES (N'Image 6.jpg', N'Image 6_YXZNHQQNOQ.jpg', NULL, N'Image 6.jpg', N'/Content/Uploads/Serves/Image 6_YXZNHQQNOQ_0_0.jpg', N'/Content/Uploads/Serves/Image 6_YXZNHQQNOQ_150_150.jpg', N'image/jpeg', 0, CAST(N'2019-07-05T08:16:06.223' AS DateTime), 0, 3, 50)

INSERT [Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id]) VALUES (N'Image 1.jpg', N'Image 1_TCPXDHBZZM.jpg', NULL, N'Image 1.jpg', N'/Content/Uploads/Serves/Image 1_TCPXDHBZZM_0_0.jpg', N'/Content/Uploads/Serves/Image 1_TCPXDHBZZM_150_150.jpg', N'image/jpeg', 0, CAST(N'2019-07-05T08:16:06.223' AS DateTime), 0, 0, 51)

INSERT [Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id]) VALUES (N'Slide-1.png', N'Slide-1.png', NULL, N'Slide-1.png', N'/Content/Uploads/Serves/Slide-1_0_0.png', N'/Content/Uploads/Serves/Slide-1_150_150.png', N'image/png', 0, CAST(N'2019-07-06T10:14:20.093' AS DateTime), 0, 0, 56)

INSERT [Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id]) VALUES (N'Slide-2.png', N'Slide-2.png', NULL, N'Slide-2.png', N'/Content/Uploads/Serves/Slide-2_0_0.png', N'/Content/Uploads/Serves/Slide-2_150_150.png', N'image/png', 0, CAST(N'2019-07-06T10:40:57.803' AS DateTime), 0, 0, 57)

INSERT [Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id]) VALUES (N'logo.png', N'logo.png', NULL, N'logo.png', N'/Content/Uploads/Serves/logo_0_0.png', N'/Content/Uploads/Serves/logo_150_150.png', N'image/png', 0, CAST(N'2019-11-11T12:15:00.530' AS DateTime), 0, 0, 58)

INSERT [Media] ([Name], [SystemName], [Description], [AlternativeText], [LocalPath], [ThumbnailPath], [MimeType], [MediaType], [CreatedOn], [UserId], [DisplayOrder], [Id]) VALUES (N'logo.png', N'logo_HPGJIHSFBW.png', NULL, N'logo.png', N'/Content/Uploads/Serves/logo_HPGJIHSFBW_0_0.png', N'/Content/Uploads/Serves/logo_HPGJIHSFBW_150_150.png', N'image/png', 0, CAST(N'2019-11-11T12:15:32.230' AS DateTime), 0, 0, 59)

SET IDENTITY_INSERT [Media] OFF

SET IDENTITY_INSERT [ProductMedia] ON 


INSERT [ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (2, 1, 1)

INSERT [ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (2, 6, 6)

INSERT [ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (2, 7, 7)

INSERT [ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (3, 8, 8)

INSERT [ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (3, 9, 9)

INSERT [ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (3, 10, 10)

INSERT [ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (3, 11, 11)

INSERT [ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (4, 13, 12)

INSERT [ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (4, 12, 13)

INSERT [ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (4, 14, 14)

INSERT [ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (4, 15, 15)

INSERT [ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (4, 16, 16)

INSERT [ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (5, 17, 17)

INSERT [ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (5, 18, 18)

INSERT [ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (6, 20, 19)

INSERT [ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (6, 21, 20)

INSERT [ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (6, 19, 21)

INSERT [ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (6, 22, 22)

INSERT [ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (6, 23, 23)

INSERT [ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (6, 24, 24)

INSERT [ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (7, 25, 25)

INSERT [ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (8, 26, 26)

INSERT [ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (8, 27, 27)

INSERT [ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (9, 28, 28)

INSERT [ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (9, 29, 29)

INSERT [ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (10, 31, 30)

INSERT [ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (10, 30, 31)

INSERT [ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (11, 32, 32)

INSERT [ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (11, 33, 33)

INSERT [ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (11, 34, 34)

INSERT [ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (11, 35, 35)

INSERT [ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (11, 36, 36)

INSERT [ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (11, 37, 37)

INSERT [ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (12, 38, 38)

INSERT [ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (12, 39, 39)

INSERT [ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (12, 41, 40)

INSERT [ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (12, 40, 41)

INSERT [ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (13, 42, 42)

INSERT [ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (13, 43, 43)

INSERT [ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (13, 44, 44)

INSERT [ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (13, 45, 45)

INSERT [ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (14, 46, 46)

INSERT [ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (14, 47, 47)

INSERT [ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (14, 48, 48)

INSERT [ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (14, 49, 49)

INSERT [ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (14, 50, 50)

INSERT [ProductMedia] ([ProductId], [MediaId], [Id]) VALUES (14, 51, 51)

SET IDENTITY_INSERT [ProductMedia] OFF

SET IDENTITY_INSERT [Menu] ON 


INSERT [Menu] ([Name], [Id]) VALUES (N'Primary Menu', 1)

INSERT [Menu] ([Name], [Id]) VALUES (N'Footer Column One', 2)

INSERT [Menu] ([Name], [Id]) VALUES (N'Footer Two', 3)

INSERT [Menu] ([Name], [Id]) VALUES (N'Footer Three', 4)

SET IDENTITY_INSERT [Menu] OFF

SET IDENTITY_INSERT [MenuItem] ON 


INSERT [MenuItem] ([MenuId], [ParentId], [Name], [SeoMetaId], [Url], [DisplayOrder], [CssClass], [IsGroup], [Id]) VALUES (1, 0, N'Laptops', 4, NULL, 0, NULL, 0, 1)

INSERT [MenuItem] ([MenuId], [ParentId], [Name], [SeoMetaId], [Url], [DisplayOrder], [CssClass], [IsGroup], [Id]) VALUES (1, 0, N'Mobiles', 8, NULL, 1, NULL, 0, 2)

INSERT [MenuItem] ([MenuId], [ParentId], [Name], [SeoMetaId], [Url], [DisplayOrder], [CssClass], [IsGroup], [Id]) VALUES (1, 0, N'Printers', 11, NULL, 2, NULL, 0, 3)

INSERT [MenuItem] ([MenuId], [ParentId], [Name], [SeoMetaId], [Url], [DisplayOrder], [CssClass], [IsGroup], [Id]) VALUES (1, 0, N'Storage', 18, NULL, 3, NULL, 0, 4)

INSERT [MenuItem] ([MenuId], [ParentId], [Name], [SeoMetaId], [Url], [DisplayOrder], [CssClass], [IsGroup], [Id]) VALUES (1, 0, N'Televisions', 23, NULL, 4, NULL, 0, 5)

INSERT [MenuItem] ([MenuId], [ParentId], [Name], [SeoMetaId], [Url], [DisplayOrder], [CssClass], [IsGroup], [Id]) VALUES (1, 3, N'Accessories', 14, NULL, 0, NULL, 0, 6)

INSERT [MenuItem] ([MenuId], [ParentId], [Name], [SeoMetaId], [Url], [DisplayOrder], [CssClass], [IsGroup], [Id]) VALUES (1, 0, N'Software', 26, NULL, 5, NULL, 0, 7)

INSERT [MenuItem] ([MenuId], [ParentId], [Name], [SeoMetaId], [Url], [DisplayOrder], [CssClass], [IsGroup], [Id]) VALUES (2, 0, N'About us', 30, NULL, 1, NULL, 0, 8)

INSERT [MenuItem] ([MenuId], [ParentId], [Name], [SeoMetaId], [Url], [DisplayOrder], [CssClass], [IsGroup], [Id]) VALUES (2, 0, N'Contact Us', 31, NULL, 2, NULL, 0, 9)

INSERT [MenuItem] ([MenuId], [ParentId], [Name], [SeoMetaId], [Url], [DisplayOrder], [CssClass], [IsGroup], [Id]) VALUES (2, 0, N'Privacy Policy', 32, NULL, 3, NULL, 0, 10)

INSERT [MenuItem] ([MenuId], [ParentId], [Name], [SeoMetaId], [Url], [DisplayOrder], [CssClass], [IsGroup], [Id]) VALUES (2, 0, N'Terms & Conditions', 33, NULL, 4, NULL, 0, 11)

INSERT [MenuItem] ([MenuId], [ParentId], [Name], [SeoMetaId], [Url], [DisplayOrder], [CssClass], [IsGroup], [Id]) VALUES (3, 0, N'Orders', NULL, N'/account/orders', 1, NULL, 0, 12)

INSERT [MenuItem] ([MenuId], [ParentId], [Name], [SeoMetaId], [Url], [DisplayOrder], [CssClass], [IsGroup], [Id]) VALUES (3, 0, N'Profile', NULL, N'/account', 0, NULL, 0, 13)

INSERT [MenuItem] ([MenuId], [ParentId], [Name], [SeoMetaId], [Url], [DisplayOrder], [CssClass], [IsGroup], [Id]) VALUES (3, 0, N'Privacy', NULL, N'/account/privacy', 2, NULL, 0, 14)

INSERT [MenuItem] ([MenuId], [ParentId], [Name], [SeoMetaId], [Url], [DisplayOrder], [CssClass], [IsGroup], [Id]) VALUES (3, 0, N'My Cart', NULL, N'/cart', 4, NULL, 0, 15)

INSERT [MenuItem] ([MenuId], [ParentId], [Name], [SeoMetaId], [Url], [DisplayOrder], [CssClass], [IsGroup], [Id]) VALUES (3, 0, N'My Wishlist', NULL, N'/account/wishlist', 5, NULL, 0, 16)

SET IDENTITY_INSERT [MenuItem] OFF

SET IDENTITY_INSERT [AvailableAttribute] ON 


INSERT [AvailableAttribute] ([Name], [Description], [Id]) VALUES (N'Processor', NULL, 1)

INSERT [AvailableAttribute] ([Name], [Description], [Id]) VALUES (N'Screen Size', NULL, 2)

INSERT [AvailableAttribute] ([Name], [Description], [Id]) VALUES (N'Screen Resolution', NULL, 3)

INSERT [AvailableAttribute] ([Name], [Description], [Id]) VALUES (N'Hard Disk Size', NULL, 4)

INSERT [AvailableAttribute] ([Name], [Description], [Id]) VALUES (N'Operating System', NULL, 5)

INSERT [AvailableAttribute] ([Name], [Description], [Id]) VALUES (N'Graphics', NULL, 6)

INSERT [AvailableAttribute] ([Name], [Description], [Id]) VALUES (N'Memory', NULL, 7)

INSERT [AvailableAttribute] ([Name], [Description], [Id]) VALUES (N'Without Type Cover', NULL, 8)

INSERT [AvailableAttribute] ([Name], [Description], [Id]) VALUES (N'Color', NULL, 9)

INSERT [AvailableAttribute] ([Name], [Description], [Id]) VALUES (N'Storage', NULL, 10)

INSERT [AvailableAttribute] ([Name], [Description], [Id]) VALUES (N'Display Type', NULL, 11)

INSERT [AvailableAttribute] ([Name], [Description], [Id]) VALUES (N'Brand', NULL, 12)

INSERT [AvailableAttribute] ([Name], [Description], [Id]) VALUES (N'Weight', NULL, 13)

INSERT [AvailableAttribute] ([Name], [Description], [Id]) VALUES (N'Dimensions', NULL, 14)

INSERT [AvailableAttribute] ([Name], [Description], [Id]) VALUES (N'Connectivity Type', NULL, 15)

INSERT [AvailableAttribute] ([Name], [Description], [Id]) VALUES (N'Package Contents', NULL, 16)

INSERT [AvailableAttribute] ([Name], [Description], [Id]) VALUES (N'Series', NULL, 17)

INSERT [AvailableAttribute] ([Name], [Description], [Id]) VALUES (N'Size', NULL, 18)

INSERT [AvailableAttribute] ([Name], [Description], [Id]) VALUES (N'Case Size', NULL, 19)

INSERT [AvailableAttribute] ([Name], [Description], [Id]) VALUES (N'Width', NULL, 20)

INSERT [AvailableAttribute] ([Name], [Description], [Id]) VALUES (N'Depth', NULL, 21)

INSERT [AvailableAttribute] ([Name], [Description], [Id]) VALUES (N'Model Year', NULL, 22)

INSERT [AvailableAttribute] ([Name], [Description], [Id]) VALUES (N'Number of Devices', NULL, 23)

INSERT [AvailableAttribute] ([Name], [Description], [Id]) VALUES (N'License Term', NULL, 24)

SET IDENTITY_INSERT [AvailableAttribute] OFF

SET IDENTITY_INSERT [ProductAttribute] ON 


INSERT [ProductAttribute] ([ProductId], [AvailableAttributeId], [InputFieldType], [Label], [DisplayOrder], [IsRequired], [Id]) VALUES (2, 7, 12, NULL, 0, 1, 1)

INSERT [ProductAttribute] ([ProductId], [AvailableAttributeId], [InputFieldType], [Label], [DisplayOrder], [IsRequired], [Id]) VALUES (3, 1, 12, NULL, 0, 1, 2)

INSERT [ProductAttribute] ([ProductId], [AvailableAttributeId], [InputFieldType], [Label], [DisplayOrder], [IsRequired], [Id]) VALUES (3, 8, 7, NULL, 1, 0, 3)

INSERT [ProductAttribute] ([ProductId], [AvailableAttributeId], [InputFieldType], [Label], [DisplayOrder], [IsRequired], [Id]) VALUES (4, 10, 12, NULL, 0, 1, 4)

INSERT [ProductAttribute] ([ProductId], [AvailableAttributeId], [InputFieldType], [Label], [DisplayOrder], [IsRequired], [Id]) VALUES (4, 9, 12, NULL, 0, 1, 5)

INSERT [ProductAttribute] ([ProductId], [AvailableAttributeId], [InputFieldType], [Label], [DisplayOrder], [IsRequired], [Id]) VALUES (7, 9, 12, NULL, 0, 1, 6)

INSERT [ProductAttribute] ([ProductId], [AvailableAttributeId], [InputFieldType], [Label], [DisplayOrder], [IsRequired], [Id]) VALUES (7, 18, 12, NULL, 0, 1, 7)

INSERT [ProductAttribute] ([ProductId], [AvailableAttributeId], [InputFieldType], [Label], [DisplayOrder], [IsRequired], [Id]) VALUES (8, 19, 12, NULL, 0, 1, 8)

INSERT [ProductAttribute] ([ProductId], [AvailableAttributeId], [InputFieldType], [Label], [DisplayOrder], [IsRequired], [Id]) VALUES (8, 15, 12, NULL, 0, 1, 9)

INSERT [ProductAttribute] ([ProductId], [AvailableAttributeId], [InputFieldType], [Label], [DisplayOrder], [IsRequired], [Id]) VALUES (9, 10, 12, NULL, 0, 1, 10)

INSERT [ProductAttribute] ([ProductId], [AvailableAttributeId], [InputFieldType], [Label], [DisplayOrder], [IsRequired], [Id]) VALUES (10, 10, 12, NULL, 0, 1, 11)

INSERT [ProductAttribute] ([ProductId], [AvailableAttributeId], [InputFieldType], [Label], [DisplayOrder], [IsRequired], [Id]) VALUES (13, 23, 12, NULL, 0, 1, 12)

INSERT [ProductAttribute] ([ProductId], [AvailableAttributeId], [InputFieldType], [Label], [DisplayOrder], [IsRequired], [Id]) VALUES (13, 24, 12, NULL, 0, 1, 13)

SET IDENTITY_INSERT [ProductAttribute] OFF

SET IDENTITY_INSERT [ProductRelation] ON 


INSERT [ProductRelation] ([SourceProductId], [DestinationProductId], [RelationType], [IsReciprocal], [Id]) VALUES (3, 2, 1, 0, 1)

INSERT [ProductRelation] ([SourceProductId], [DestinationProductId], [RelationType], [IsReciprocal], [Id]) VALUES (2, 3, 1, 0, 2)

INSERT [ProductRelation] ([SourceProductId], [DestinationProductId], [RelationType], [IsReciprocal], [Id]) VALUES (6, 5, 1, 0, 3)

INSERT [ProductRelation] ([SourceProductId], [DestinationProductId], [RelationType], [IsReciprocal], [Id]) VALUES (5, 6, 1, 0, 4)

SET IDENTITY_INSERT [ProductRelation] OFF

SET IDENTITY_INSERT [ProductSpecification] ON 


INSERT [ProductSpecification] ([ProductId], [AvailableAttributeId], [Label], [DisplayOrder], [ProductSpecificationGroupId], [IsVisible], [IsFilterable], [Id]) VALUES (2, 1, NULL, 3, 1, 1, 1, 1)

INSERT [ProductSpecification] ([ProductId], [AvailableAttributeId], [Label], [DisplayOrder], [ProductSpecificationGroupId], [IsVisible], [IsFilterable], [Id]) VALUES (2, 2, NULL, 0, 1, 1, 0, 2)

INSERT [ProductSpecification] ([ProductId], [AvailableAttributeId], [Label], [DisplayOrder], [ProductSpecificationGroupId], [IsVisible], [IsFilterable], [Id]) VALUES (2, 3, NULL, 1, 1, 1, 0, 3)

INSERT [ProductSpecification] ([ProductId], [AvailableAttributeId], [Label], [DisplayOrder], [ProductSpecificationGroupId], [IsVisible], [IsFilterable], [Id]) VALUES (2, 4, NULL, 2, 1, 1, 1, 4)

INSERT [ProductSpecification] ([ProductId], [AvailableAttributeId], [Label], [DisplayOrder], [ProductSpecificationGroupId], [IsVisible], [IsFilterable], [Id]) VALUES (2, 5, NULL, 5, 1, 1, 1, 5)

INSERT [ProductSpecification] ([ProductId], [AvailableAttributeId], [Label], [DisplayOrder], [ProductSpecificationGroupId], [IsVisible], [IsFilterable], [Id]) VALUES (2, 6, NULL, 4, 1, 1, 0, 6)

INSERT [ProductSpecification] ([ProductId], [AvailableAttributeId], [Label], [DisplayOrder], [ProductSpecificationGroupId], [IsVisible], [IsFilterable], [Id]) VALUES (3, 5, NULL, 0, 0, 1, 1, 7)

INSERT [ProductSpecification] ([ProductId], [AvailableAttributeId], [Label], [DisplayOrder], [ProductSpecificationGroupId], [IsVisible], [IsFilterable], [Id]) VALUES (3, 7, NULL, 0, 0, 1, 1, 8)

INSERT [ProductSpecification] ([ProductId], [AvailableAttributeId], [Label], [DisplayOrder], [ProductSpecificationGroupId], [IsVisible], [IsFilterable], [Id]) VALUES (3, 4, NULL, 0, 0, 1, 1, 9)

INSERT [ProductSpecification] ([ProductId], [AvailableAttributeId], [Label], [DisplayOrder], [ProductSpecificationGroupId], [IsVisible], [IsFilterable], [Id]) VALUES (3, 1, N'Available Processors', 0, 0, 1, 1, 10)

INSERT [ProductSpecification] ([ProductId], [AvailableAttributeId], [Label], [DisplayOrder], [ProductSpecificationGroupId], [IsVisible], [IsFilterable], [Id]) VALUES (4, 9, NULL, 0, 0, 1, 1, 11)

INSERT [ProductSpecification] ([ProductId], [AvailableAttributeId], [Label], [DisplayOrder], [ProductSpecificationGroupId], [IsVisible], [IsFilterable], [Id]) VALUES (4, 10, NULL, 0, 0, 1, 1, 12)

INSERT [ProductSpecification] ([ProductId], [AvailableAttributeId], [Label], [DisplayOrder], [ProductSpecificationGroupId], [IsVisible], [IsFilterable], [Id]) VALUES (4, 2, NULL, 0, 0, 1, 0, 13)

INSERT [ProductSpecification] ([ProductId], [AvailableAttributeId], [Label], [DisplayOrder], [ProductSpecificationGroupId], [IsVisible], [IsFilterable], [Id]) VALUES (4, 3, NULL, 0, 0, 1, 0, 14)

INSERT [ProductSpecification] ([ProductId], [AvailableAttributeId], [Label], [DisplayOrder], [ProductSpecificationGroupId], [IsVisible], [IsFilterable], [Id]) VALUES (4, 11, NULL, 0, 0, 1, 0, 15)

INSERT [ProductSpecification] ([ProductId], [AvailableAttributeId], [Label], [DisplayOrder], [ProductSpecificationGroupId], [IsVisible], [IsFilterable], [Id]) VALUES (5, 12, NULL, 0, 0, 1, 1, 16)

INSERT [ProductSpecification] ([ProductId], [AvailableAttributeId], [Label], [DisplayOrder], [ProductSpecificationGroupId], [IsVisible], [IsFilterable], [Id]) VALUES (5, 9, NULL, 0, 0, 1, 1, 17)

INSERT [ProductSpecification] ([ProductId], [AvailableAttributeId], [Label], [DisplayOrder], [ProductSpecificationGroupId], [IsVisible], [IsFilterable], [Id]) VALUES (5, 13, NULL, 0, 0, 1, 0, 18)

INSERT [ProductSpecification] ([ProductId], [AvailableAttributeId], [Label], [DisplayOrder], [ProductSpecificationGroupId], [IsVisible], [IsFilterable], [Id]) VALUES (5, 14, NULL, 0, 0, 1, 0, 19)

INSERT [ProductSpecification] ([ProductId], [AvailableAttributeId], [Label], [DisplayOrder], [ProductSpecificationGroupId], [IsVisible], [IsFilterable], [Id]) VALUES (5, 15, NULL, 0, 0, 1, 1, 20)

INSERT [ProductSpecification] ([ProductId], [AvailableAttributeId], [Label], [DisplayOrder], [ProductSpecificationGroupId], [IsVisible], [IsFilterable], [Id]) VALUES (5, 16, NULL, 0, 0, 1, 0, 21)

INSERT [ProductSpecification] ([ProductId], [AvailableAttributeId], [Label], [DisplayOrder], [ProductSpecificationGroupId], [IsVisible], [IsFilterable], [Id]) VALUES (6, 12, NULL, 1, 0, 1, 1, 22)

INSERT [ProductSpecification] ([ProductId], [AvailableAttributeId], [Label], [DisplayOrder], [ProductSpecificationGroupId], [IsVisible], [IsFilterable], [Id]) VALUES (6, 14, NULL, 2, 0, 1, 0, 23)

INSERT [ProductSpecification] ([ProductId], [AvailableAttributeId], [Label], [DisplayOrder], [ProductSpecificationGroupId], [IsVisible], [IsFilterable], [Id]) VALUES (6, 13, NULL, 3, 0, 1, 0, 24)

INSERT [ProductSpecification] ([ProductId], [AvailableAttributeId], [Label], [DisplayOrder], [ProductSpecificationGroupId], [IsVisible], [IsFilterable], [Id]) VALUES (6, 17, NULL, 0, 0, 1, 0, 25)

INSERT [ProductSpecification] ([ProductId], [AvailableAttributeId], [Label], [DisplayOrder], [ProductSpecificationGroupId], [IsVisible], [IsFilterable], [Id]) VALUES (8, 19, NULL, 0, 0, 1, 1, 26)

INSERT [ProductSpecification] ([ProductId], [AvailableAttributeId], [Label], [DisplayOrder], [ProductSpecificationGroupId], [IsVisible], [IsFilterable], [Id]) VALUES (8, 16, NULL, 0, 0, 1, 0, 27)

INSERT [ProductSpecification] ([ProductId], [AvailableAttributeId], [Label], [DisplayOrder], [ProductSpecificationGroupId], [IsVisible], [IsFilterable], [Id]) VALUES (8, 15, NULL, 0, 0, 1, 0, 28)

INSERT [ProductSpecification] ([ProductId], [AvailableAttributeId], [Label], [DisplayOrder], [ProductSpecificationGroupId], [IsVisible], [IsFilterable], [Id]) VALUES (8, 20, NULL, 0, 0, 1, 0, 29)

INSERT [ProductSpecification] ([ProductId], [AvailableAttributeId], [Label], [DisplayOrder], [ProductSpecificationGroupId], [IsVisible], [IsFilterable], [Id]) VALUES (8, 21, NULL, 0, 0, 1, 0, 30)

INSERT [ProductSpecification] ([ProductId], [AvailableAttributeId], [Label], [DisplayOrder], [ProductSpecificationGroupId], [IsVisible], [IsFilterable], [Id]) VALUES (9, 10, N'Available Storage', 0, 0, 1, 1, 31)

INSERT [ProductSpecification] ([ProductId], [AvailableAttributeId], [Label], [DisplayOrder], [ProductSpecificationGroupId], [IsVisible], [IsFilterable], [Id]) VALUES (9, 16, NULL, 0, 0, 1, 0, 32)

INSERT [ProductSpecification] ([ProductId], [AvailableAttributeId], [Label], [DisplayOrder], [ProductSpecificationGroupId], [IsVisible], [IsFilterable], [Id]) VALUES (7, 12, NULL, 0, 0, 1, 1, 33)

INSERT [ProductSpecification] ([ProductId], [AvailableAttributeId], [Label], [DisplayOrder], [ProductSpecificationGroupId], [IsVisible], [IsFilterable], [Id]) VALUES (11, 22, NULL, 0, 0, 1, 1, 34)

INSERT [ProductSpecification] ([ProductId], [AvailableAttributeId], [Label], [DisplayOrder], [ProductSpecificationGroupId], [IsVisible], [IsFilterable], [Id]) VALUES (11, 14, NULL, 0, 0, 1, 0, 35)

INSERT [ProductSpecification] ([ProductId], [AvailableAttributeId], [Label], [DisplayOrder], [ProductSpecificationGroupId], [IsVisible], [IsFilterable], [Id]) VALUES (11, 16, NULL, 0, 0, 1, 0, 36)

INSERT [ProductSpecification] ([ProductId], [AvailableAttributeId], [Label], [DisplayOrder], [ProductSpecificationGroupId], [IsVisible], [IsFilterable], [Id]) VALUES (11, 2, NULL, 0, 0, 1, 1, 37)

INSERT [ProductSpecification] ([ProductId], [AvailableAttributeId], [Label], [DisplayOrder], [ProductSpecificationGroupId], [IsVisible], [IsFilterable], [Id]) VALUES (11, 11, NULL, 0, 0, 1, 1, 38)

SET IDENTITY_INSERT [ProductSpecification] OFF

SET IDENTITY_INSERT [ProductSpecificationGroup] ON 


INSERT [ProductSpecificationGroup] ([Name], [DisplayOrder], [ProductId], [Id]) VALUES (N'Specifications', 0, 2, 1)

SET IDENTITY_INSERT [ProductSpecificationGroup] OFF

SET IDENTITY_INSERT [ProductVariant] ON 


INSERT [ProductVariant] ([ProductId], [Sku], [Gtin], [Mpn], [Price], [ComparePrice], [TrackInventory], [CanOrderWhenOutOfStock], [MediaId], [Id]) VALUES (2, NULL, NULL, NULL, NULL, NULL, 1, 0, 0, 1)

INSERT [ProductVariant] ([ProductId], [Sku], [Gtin], [Mpn], [Price], [ComparePrice], [TrackInventory], [CanOrderWhenOutOfStock], [MediaId], [Id]) VALUES (2, NULL, NULL, NULL, CAST(520.00000 AS Numeric(18, 5)), NULL, 1, 0, 0, 2)

INSERT [ProductVariant] ([ProductId], [Sku], [Gtin], [Mpn], [Price], [ComparePrice], [TrackInventory], [CanOrderWhenOutOfStock], [MediaId], [Id]) VALUES (3, NULL, NULL, NULL, NULL, NULL, 1, 0, 0, 3)

INSERT [ProductVariant] ([ProductId], [Sku], [Gtin], [Mpn], [Price], [ComparePrice], [TrackInventory], [CanOrderWhenOutOfStock], [MediaId], [Id]) VALUES (3, NULL, NULL, NULL, CAST(1979.00000 AS Numeric(18, 5)), NULL, 1, 0, 0, 4)

INSERT [ProductVariant] ([ProductId], [Sku], [Gtin], [Mpn], [Price], [ComparePrice], [TrackInventory], [CanOrderWhenOutOfStock], [MediaId], [Id]) VALUES (3, NULL, NULL, NULL, CAST(1100.00000 AS Numeric(18, 5)), NULL, 1, 0, 0, 5)

INSERT [ProductVariant] ([ProductId], [Sku], [Gtin], [Mpn], [Price], [ComparePrice], [TrackInventory], [CanOrderWhenOutOfStock], [MediaId], [Id]) VALUES (4, NULL, NULL, NULL, NULL, NULL, 1, 0, 0, 6)

INSERT [ProductVariant] ([ProductId], [Sku], [Gtin], [Mpn], [Price], [ComparePrice], [TrackInventory], [CanOrderWhenOutOfStock], [MediaId], [Id]) VALUES (4, NULL, NULL, NULL, NULL, NULL, 1, 0, 0, 7)

INSERT [ProductVariant] ([ProductId], [Sku], [Gtin], [Mpn], [Price], [ComparePrice], [TrackInventory], [CanOrderWhenOutOfStock], [MediaId], [Id]) VALUES (4, NULL, NULL, NULL, CAST(1095.00000 AS Numeric(18, 5)), NULL, 1, 0, 0, 8)

INSERT [ProductVariant] ([ProductId], [Sku], [Gtin], [Mpn], [Price], [ComparePrice], [TrackInventory], [CanOrderWhenOutOfStock], [MediaId], [Id]) VALUES (4, NULL, NULL, NULL, CAST(1095.00000 AS Numeric(18, 5)), NULL, 1, 0, 0, 9)

INSERT [ProductVariant] ([ProductId], [Sku], [Gtin], [Mpn], [Price], [ComparePrice], [TrackInventory], [CanOrderWhenOutOfStock], [MediaId], [Id]) VALUES (7, NULL, NULL, NULL, CAST(10.00000 AS Numeric(18, 5)), NULL, 1, 0, 0, 10)

INSERT [ProductVariant] ([ProductId], [Sku], [Gtin], [Mpn], [Price], [ComparePrice], [TrackInventory], [CanOrderWhenOutOfStock], [MediaId], [Id]) VALUES (7, NULL, NULL, NULL, NULL, NULL, 1, 0, 0, 11)

INSERT [ProductVariant] ([ProductId], [Sku], [Gtin], [Mpn], [Price], [ComparePrice], [TrackInventory], [CanOrderWhenOutOfStock], [MediaId], [Id]) VALUES (7, NULL, NULL, NULL, CAST(15.00000 AS Numeric(18, 5)), NULL, 1, 0, 0, 12)

INSERT [ProductVariant] ([ProductId], [Sku], [Gtin], [Mpn], [Price], [ComparePrice], [TrackInventory], [CanOrderWhenOutOfStock], [MediaId], [Id]) VALUES (7, NULL, NULL, NULL, CAST(11.00000 AS Numeric(18, 5)), NULL, 1, 0, 0, 13)

INSERT [ProductVariant] ([ProductId], [Sku], [Gtin], [Mpn], [Price], [ComparePrice], [TrackInventory], [CanOrderWhenOutOfStock], [MediaId], [Id]) VALUES (8, NULL, NULL, NULL, CAST(399.00000 AS Numeric(18, 5)), NULL, 1, 0, 0, 14)

INSERT [ProductVariant] ([ProductId], [Sku], [Gtin], [Mpn], [Price], [ComparePrice], [TrackInventory], [CanOrderWhenOutOfStock], [MediaId], [Id]) VALUES (8, NULL, NULL, NULL, CAST(429.00000 AS Numeric(18, 5)), NULL, 1, 0, 0, 15)

INSERT [ProductVariant] ([ProductId], [Sku], [Gtin], [Mpn], [Price], [ComparePrice], [TrackInventory], [CanOrderWhenOutOfStock], [MediaId], [Id]) VALUES (8, NULL, NULL, NULL, CAST(499.00000 AS Numeric(18, 5)), NULL, 1, 0, 0, 16)

INSERT [ProductVariant] ([ProductId], [Sku], [Gtin], [Mpn], [Price], [ComparePrice], [TrackInventory], [CanOrderWhenOutOfStock], [MediaId], [Id]) VALUES (8, NULL, NULL, NULL, CAST(529.00000 AS Numeric(18, 5)), NULL, 1, 0, 0, 17)

INSERT [ProductVariant] ([ProductId], [Sku], [Gtin], [Mpn], [Price], [ComparePrice], [TrackInventory], [CanOrderWhenOutOfStock], [MediaId], [Id]) VALUES (9, NULL, NULL, NULL, NULL, NULL, 1, 0, 0, 18)

INSERT [ProductVariant] ([ProductId], [Sku], [Gtin], [Mpn], [Price], [ComparePrice], [TrackInventory], [CanOrderWhenOutOfStock], [MediaId], [Id]) VALUES (9, NULL, NULL, NULL, CAST(74.99000 AS Numeric(18, 5)), NULL, 1, 0, 0, 19)

INSERT [ProductVariant] ([ProductId], [Sku], [Gtin], [Mpn], [Price], [ComparePrice], [TrackInventory], [CanOrderWhenOutOfStock], [MediaId], [Id]) VALUES (9, NULL, NULL, NULL, CAST(99.99000 AS Numeric(18, 5)), NULL, 1, 0, 0, 20)

INSERT [ProductVariant] ([ProductId], [Sku], [Gtin], [Mpn], [Price], [ComparePrice], [TrackInventory], [CanOrderWhenOutOfStock], [MediaId], [Id]) VALUES (10, N'WDBCRM0020BBK-NESN', NULL, NULL, NULL, NULL, 1, 0, 0, 21)

INSERT [ProductVariant] ([ProductId], [Sku], [Gtin], [Mpn], [Price], [ComparePrice], [TrackInventory], [CanOrderWhenOutOfStock], [MediaId], [Id]) VALUES (10, N'WDBCRM0030BBK-NESN', NULL, NULL, CAST(119.99000 AS Numeric(18, 5)), NULL, 1, 0, 0, 22)

INSERT [ProductVariant] ([ProductId], [Sku], [Gtin], [Mpn], [Price], [ComparePrice], [TrackInventory], [CanOrderWhenOutOfStock], [MediaId], [Id]) VALUES (13, NULL, NULL, NULL, CAST(39.99000 AS Numeric(18, 5)), NULL, 0, 0, 0, 23)

INSERT [ProductVariant] ([ProductId], [Sku], [Gtin], [Mpn], [Price], [ComparePrice], [TrackInventory], [CanOrderWhenOutOfStock], [MediaId], [Id]) VALUES (13, NULL, NULL, NULL, CAST(68.99000 AS Numeric(18, 5)), NULL, 0, 0, 0, 24)

INSERT [ProductVariant] ([ProductId], [Sku], [Gtin], [Mpn], [Price], [ComparePrice], [TrackInventory], [CanOrderWhenOutOfStock], [MediaId], [Id]) VALUES (13, NULL, NULL, NULL, CAST(89.98000 AS Numeric(18, 5)), NULL, 0, 0, 0, 25)

INSERT [ProductVariant] ([ProductId], [Sku], [Gtin], [Mpn], [Price], [ComparePrice], [TrackInventory], [CanOrderWhenOutOfStock], [MediaId], [Id]) VALUES (13, NULL, NULL, NULL, CAST(44.99000 AS Numeric(18, 5)), NULL, 0, 0, 0, 26)

INSERT [ProductVariant] ([ProductId], [Sku], [Gtin], [Mpn], [Price], [ComparePrice], [TrackInventory], [CanOrderWhenOutOfStock], [MediaId], [Id]) VALUES (13, NULL, NULL, NULL, CAST(77.99000 AS Numeric(18, 5)), NULL, 0, 0, 0, 27)

INSERT [ProductVariant] ([ProductId], [Sku], [Gtin], [Mpn], [Price], [ComparePrice], [TrackInventory], [CanOrderWhenOutOfStock], [MediaId], [Id]) VALUES (13, NULL, NULL, NULL, CAST(100.98000 AS Numeric(18, 5)), NULL, 0, 0, 0, 28)

SET IDENTITY_INSERT [ProductVariant] OFF

SET IDENTITY_INSERT [AvailableAttributeValue] ON 


INSERT [AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (1, N'Core i3 7100U', 1)

INSERT [AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (2, N'15.6', 2)

INSERT [AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (3, N'1920 x 1080 (Full HD)', 3)

INSERT [AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (4, N'1 TB', 4)

INSERT [AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (5, N'Windows 10 Home', 5)

INSERT [AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (6, N'Intel HD Graphics', 6)

INSERT [AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (7, N'4 GB', 7)

INSERT [AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (7, N'8 GB', 8)

INSERT [AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (1, N'8th Gen - Core i3', 9)

INSERT [AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (1, N'8th Gen - Core i5', 10)

INSERT [AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (1, N'8th Gen - Core i7', 11)

INSERT [AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (8, N'Yes', 12)

INSERT [AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (4, N'128 GB SSD', 13)

INSERT [AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (10, N'64 GB', 14)

INSERT [AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (10, N'256 GB', 15)

INSERT [AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (9, N'Silver', 16)

INSERT [AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (9, N'Space Gray', 17)

INSERT [AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (2, N'5.8 Inches', 18)

INSERT [AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (3, N'2436 x 1125 Pixels', 19)

INSERT [AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (11, N'OLED Multi-touch', 20)

INSERT [AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (12, N'Canon', 21)

INSERT [AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (9, N'Black', 22)

INSERT [AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (13, N'5.8 KG', 23)

INSERT [AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (14, N'44.5 x 33 x 16.3 cm', 24)

INSERT [AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (15, N'WiFi', 25)

INSERT [AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (16, N'Printer with Ink Tank and Cartridge Unit Set;USB Cable, Power Cable, Print Head x2/2N;Manual and Driver, Ink Bottle x6/6N', 26)

INSERT [AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (12, N'HP', 27)

INSERT [AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (14, N'58.7 x 39.4 x 20.8 cm', 28)

INSERT [AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (13, N'6.53 KG', 29)

INSERT [AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (17, N'310', 30)

INSERT [AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (9, N'Color', 31)

INSERT [AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (18, N'Regular', 32)

INSERT [AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (18, N'Small', 33)

INSERT [AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (19, N'40mm', 34)

INSERT [AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (19, N'44mm', 35)

INSERT [AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (15, N'GPS', 36)

INSERT [AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (15, N'GPS + Cellular', 37)

INSERT [AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (16, N'Case, Band, 1m Magnetic Charging Cable, 5W USB Power Adapter', 38)

INSERT [AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (15, N'Wi-Fi 802.11b/g/n 2.4GHz, Bluetooth 5.0', 39)

INSERT [AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (20, N'34mm', 40)

INSERT [AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (21, N'10.7mm', 41)

INSERT [AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (10, N'1 TB', 42)

INSERT [AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (10, N'2 TB', 43)

INSERT [AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (10, N'3 TB', 44)

INSERT [AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (10, N'4 TB', 45)

INSERT [AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (16, N'Portable hard drive, USB 3.0 cable, Quick install guide', 46)

INSERT [AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (22, N'2018', 47)

INSERT [AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (14, N'73.7 x 43.8 x 7.4 cm', 48)

INSERT [AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (16, N'1 LED TV, 1 Table Top Stand, 1 User Manual, 1 Warranty Card, 1 Remote Control, 1 Power Cable / Power Supply Adopter', 49)

INSERT [AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (2, N'32 Inches', 50)

INSERT [AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (11, N'LED', 51)

INSERT [AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (23, N'3 Devices', 52)

INSERT [AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (23, N'5 Devices', 53)

INSERT [AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (24, N'1 Year', 54)

INSERT [AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (24, N'2 Years', 55)

INSERT [AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (24, N'3 Years', 56)

INSERT [AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (9, N'Red', 60)

INSERT [AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (9, N'Green', 61)

INSERT [AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (9, N'Yello', 62)

INSERT [AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (9, N'Orange', 63)

INSERT [AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (9, N'Purple', 64)

INSERT [AvailableAttributeValue] ([AvailableAttributeId], [Value], [Id]) VALUES (9, N'Magenta', 65)

SET IDENTITY_INSERT [AvailableAttributeValue] OFF

SET IDENTITY_INSERT [ProductAttributeValue] ON 


INSERT [ProductAttributeValue] ([ProductAttributeId], [AvailableAttributeValueId], [Label], [Id]) VALUES (1, 7, NULL, 1)

INSERT [ProductAttributeValue] ([ProductAttributeId], [AvailableAttributeValueId], [Label], [Id]) VALUES (1, 8, NULL, 2)

INSERT [ProductAttributeValue] ([ProductAttributeId], [AvailableAttributeValueId], [Label], [Id]) VALUES (2, 10, NULL, 3)

INSERT [ProductAttributeValue] ([ProductAttributeId], [AvailableAttributeValueId], [Label], [Id]) VALUES (2, 11, NULL, 4)

INSERT [ProductAttributeValue] ([ProductAttributeId], [AvailableAttributeValueId], [Label], [Id]) VALUES (3, 12, NULL, 5)

INSERT [ProductAttributeValue] ([ProductAttributeId], [AvailableAttributeValueId], [Label], [Id]) VALUES (4, 14, NULL, 6)

INSERT [ProductAttributeValue] ([ProductAttributeId], [AvailableAttributeValueId], [Label], [Id]) VALUES (4, 15, NULL, 7)

INSERT [ProductAttributeValue] ([ProductAttributeId], [AvailableAttributeValueId], [Label], [Id]) VALUES (5, 16, NULL, 8)

INSERT [ProductAttributeValue] ([ProductAttributeId], [AvailableAttributeValueId], [Label], [Id]) VALUES (5, 17, NULL, 9)

INSERT [ProductAttributeValue] ([ProductAttributeId], [AvailableAttributeValueId], [Label], [Id]) VALUES (6, 22, NULL, 10)

INSERT [ProductAttributeValue] ([ProductAttributeId], [AvailableAttributeValueId], [Label], [Id]) VALUES (6, 31, NULL, 11)

INSERT [ProductAttributeValue] ([ProductAttributeId], [AvailableAttributeValueId], [Label], [Id]) VALUES (7, 32, NULL, 12)

INSERT [ProductAttributeValue] ([ProductAttributeId], [AvailableAttributeValueId], [Label], [Id]) VALUES (7, 33, NULL, 13)

INSERT [ProductAttributeValue] ([ProductAttributeId], [AvailableAttributeValueId], [Label], [Id]) VALUES (8, 34, NULL, 14)

INSERT [ProductAttributeValue] ([ProductAttributeId], [AvailableAttributeValueId], [Label], [Id]) VALUES (8, 35, NULL, 15)

INSERT [ProductAttributeValue] ([ProductAttributeId], [AvailableAttributeValueId], [Label], [Id]) VALUES (9, 36, NULL, 16)

INSERT [ProductAttributeValue] ([ProductAttributeId], [AvailableAttributeValueId], [Label], [Id]) VALUES (9, 37, NULL, 17)

INSERT [ProductAttributeValue] ([ProductAttributeId], [AvailableAttributeValueId], [Label], [Id]) VALUES (10, 42, NULL, 18)

INSERT [ProductAttributeValue] ([ProductAttributeId], [AvailableAttributeValueId], [Label], [Id]) VALUES (10, 43, NULL, 19)

INSERT [ProductAttributeValue] ([ProductAttributeId], [AvailableAttributeValueId], [Label], [Id]) VALUES (10, 44, NULL, 20)

INSERT [ProductAttributeValue] ([ProductAttributeId], [AvailableAttributeValueId], [Label], [Id]) VALUES (10, 45, NULL, 21)

INSERT [ProductAttributeValue] ([ProductAttributeId], [AvailableAttributeValueId], [Label], [Id]) VALUES (11, 43, NULL, 22)

INSERT [ProductAttributeValue] ([ProductAttributeId], [AvailableAttributeValueId], [Label], [Id]) VALUES (11, 44, NULL, 23)

INSERT [ProductAttributeValue] ([ProductAttributeId], [AvailableAttributeValueId], [Label], [Id]) VALUES (12, 52, NULL, 24)

INSERT [ProductAttributeValue] ([ProductAttributeId], [AvailableAttributeValueId], [Label], [Id]) VALUES (12, 53, NULL, 25)

INSERT [ProductAttributeValue] ([ProductAttributeId], [AvailableAttributeValueId], [Label], [Id]) VALUES (13, 54, NULL, 26)

INSERT [ProductAttributeValue] ([ProductAttributeId], [AvailableAttributeValueId], [Label], [Id]) VALUES (13, 55, NULL, 27)

INSERT [ProductAttributeValue] ([ProductAttributeId], [AvailableAttributeValueId], [Label], [Id]) VALUES (13, 56, NULL, 28)

SET IDENTITY_INSERT [ProductAttributeValue] OFF

SET IDENTITY_INSERT [ProductVariantAttribute] ON 


INSERT [ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (1, 1, 1, 1)

INSERT [ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (2, 1, 2, 2)

INSERT [ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (3, 2, 3, 3)

INSERT [ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (4, 2, 4, 4)

INSERT [ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (5, 2, 3, 5)

INSERT [ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (5, 3, 5, 6)

INSERT [ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (6, 4, 6, 7)

INSERT [ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (6, 5, 8, 8)

INSERT [ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (7, 4, 6, 9)

INSERT [ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (7, 5, 9, 10)

INSERT [ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (8, 4, 7, 11)

INSERT [ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (8, 5, 8, 12)

INSERT [ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (9, 4, 7, 13)

INSERT [ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (9, 5, 9, 14)

INSERT [ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (10, 6, 10, 15)

INSERT [ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (10, 7, 12, 16)

INSERT [ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (11, 6, 10, 17)

INSERT [ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (11, 7, 13, 18)

INSERT [ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (12, 6, 11, 19)

INSERT [ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (12, 7, 12, 20)

INSERT [ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (13, 6, 11, 21)

INSERT [ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (13, 7, 13, 22)

INSERT [ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (14, 8, 14, 23)

INSERT [ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (14, 9, 16, 24)

INSERT [ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (15, 8, 15, 25)

INSERT [ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (15, 9, 16, 26)

INSERT [ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (16, 8, 14, 27)

INSERT [ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (16, 9, 17, 28)

INSERT [ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (17, 8, 15, 29)

INSERT [ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (17, 9, 17, 30)

INSERT [ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (18, 10, 18, 31)

INSERT [ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (19, 10, 19, 32)

INSERT [ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (20, 10, 20, 33)

INSERT [ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (21, 11, 22, 34)

INSERT [ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (22, 11, 23, 35)

INSERT [ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (23, 12, 24, 36)

INSERT [ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (23, 13, 26, 37)

INSERT [ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (24, 12, 24, 38)

INSERT [ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (24, 13, 27, 39)

INSERT [ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (25, 12, 24, 40)

INSERT [ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (25, 13, 28, 41)

INSERT [ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (26, 12, 25, 42)

INSERT [ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (26, 13, 26, 43)

INSERT [ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (27, 12, 25, 44)

INSERT [ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (27, 13, 27, 45)

INSERT [ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (28, 12, 25, 46)

INSERT [ProductVariantAttribute] ([ProductVariantId], [ProductAttributeId], [ProductAttributeValueId], [Id]) VALUES (28, 13, 28, 47)

SET IDENTITY_INSERT [ProductVariantAttribute] OFF

SET IDENTITY_INSERT [ProductSpecificationValue] ON 


INSERT [ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (1, 1, NULL, 1)

INSERT [ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (2, 2, NULL, 2)

INSERT [ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (3, 3, NULL, 3)

INSERT [ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (4, 4, NULL, 4)

INSERT [ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (5, 5, NULL, 5)

INSERT [ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (6, 6, NULL, 6)

INSERT [ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (7, 5, NULL, 7)

INSERT [ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (8, 8, NULL, 8)

INSERT [ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (9, 13, NULL, 9)

INSERT [ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (10, 10, NULL, 10)

INSERT [ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (10, 11, NULL, 11)

INSERT [ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (11, 16, NULL, 12)

INSERT [ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (11, 17, NULL, 13)

INSERT [ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (12, 14, NULL, 14)

INSERT [ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (12, 15, NULL, 15)

INSERT [ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (13, 18, NULL, 16)

INSERT [ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (14, 19, NULL, 17)

INSERT [ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (15, 20, NULL, 18)

INSERT [ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (16, 21, NULL, 19)

INSERT [ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (17, 22, NULL, 20)

INSERT [ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (18, 23, NULL, 21)

INSERT [ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (19, 24, NULL, 22)

INSERT [ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (20, 25, NULL, 23)

INSERT [ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (21, 26, NULL, 24)

INSERT [ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (22, 27, NULL, 25)

INSERT [ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (23, 28, NULL, 26)

INSERT [ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (24, 29, NULL, 27)

INSERT [ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (25, 30, NULL, 28)

INSERT [ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (26, 34, NULL, 29)

INSERT [ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (26, 35, NULL, 30)

INSERT [ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (27, 38, NULL, 31)

INSERT [ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (28, 39, NULL, 32)

INSERT [ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (29, 40, NULL, 33)

INSERT [ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (30, 41, NULL, 34)

INSERT [ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (31, 42, NULL, 35)

INSERT [ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (31, 43, NULL, 36)

INSERT [ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (31, 44, NULL, 37)

INSERT [ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (31, 45, NULL, 38)

INSERT [ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (32, 46, NULL, 39)

INSERT [ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (33, 21, NULL, 40)

INSERT [ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (34, 47, NULL, 41)

INSERT [ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (35, 48, NULL, 42)

INSERT [ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (36, 49, NULL, 43)

INSERT [ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (37, 50, NULL, 44)

INSERT [ProductSpecificationValue] ([ProductSpecificationId], [AvailableAttributeValueId], [Label], [Id]) VALUES (38, 51, NULL, 45)

SET IDENTITY_INSERT [ProductSpecificationValue] OFF

SET IDENTITY_INSERT [ConsentGroup] ON 


INSERT [ConsentGroup] ([Name], [Description], [DisplayOrder], [Id]) VALUES (N'Cookie Preferences', N'We use cookies to personalise content and ads, to provide social media features and to analyse our traffic. We also share information about your use of our site with our social media, advertising and analytics partners who may combine it with other information that you’ve provided to them or that they’ve collected from your use of their services. You consent to our cookies if you continue to use our website.', 1, 1)

INSERT [ConsentGroup] ([Name], [Description], [DisplayOrder], [Id]) VALUES (N'Newsletter Preferences', NULL, 2, 2)

SET IDENTITY_INSERT [ConsentGroup] OFF

SET IDENTITY_INSERT [ContentPage] ON 


INSERT [ContentPage] ([Name], [UserId], [Content], [Published], [Private], [Password], [SystemName], [CreatedOn], [UpdatedOn], [PublishedOn], [Template], [Id]) VALUES (N'About us', 1, N'Write some story about your site. People love stories.', 1, 0, NULL, NULL, CAST(N'2019-07-05T15:10:02.030' AS DateTime), CAST(N'2019-07-05T15:10:02.033' AS DateTime), CAST(N'2019-07-05T15:10:02.033' AS DateTime), N'0', 1)

INSERT [ContentPage] ([Name], [UserId], [Content], [Published], [Private], [Password], [SystemName], [CreatedOn], [UpdatedOn], [PublishedOn], [Template], [Id]) VALUES (N'Contact Us', 1, N'The contact us page will show some form. It uses a template from the theme.', 1, 0, NULL, NULL, CAST(N'2019-07-05T15:10:27.797' AS DateTime), CAST(N'2019-07-05T15:10:27.797' AS DateTime), CAST(N'2019-07-05T15:10:27.797' AS DateTime), N'ContactUs', 2)

INSERT [ContentPage] ([Name], [UserId], [Content], [Published], [Private], [Password], [SystemName], [CreatedOn], [UpdatedOn], [PublishedOn], [Template], [Id]) VALUES (N'Privacy Policy', 1, N'Why not write some privacy policy? It''s very important.', 1, 0, NULL, NULL, CAST(N'2019-07-05T15:10:47.203' AS DateTime), CAST(N'2019-07-05T15:10:47.203' AS DateTime), CAST(N'2019-07-05T15:10:47.203' AS DateTime), N'0', 3)

INSERT [ContentPage] ([Name], [UserId], [Content], [Published], [Private], [Password], [SystemName], [CreatedOn], [UpdatedOn], [PublishedOn], [Template], [Id]) VALUES (N'Terms & Conditions', 1, N'Are there any terms when people use your website? write them on this page.', 1, 0, NULL, NULL, CAST(N'2019-07-05T15:11:08.830' AS DateTime), CAST(N'2019-07-05T15:11:08.833' AS DateTime), CAST(N'2019-07-05T15:11:08.830' AS DateTime), N'0', 4)

SET IDENTITY_INSERT [ContentPage] OFF

SET IDENTITY_INSERT [Country] ON 


INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Afghanistan', N'AF', 0, 1, 0, 1)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Albania', N'AL', 0, 1, 0, 2)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Algeria', N'DZ', 0, 1, 0, 3)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'American Samoa', N'AS', 0, 1, 0, 4)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Andorra', N'AD', 0, 1, 0, 5)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Angola', N'AO', 0, 1, 0, 6)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Anguilla', N'AI', 0, 1, 0, 7)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Antarctica', N'AQ', 0, 1, 0, 8)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Antigua And Barbuda', N'AG', 0, 1, 0, 9)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Argentina', N'AR', 0, 1, 0, 10)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Armenia', N'AM', 0, 1, 0, 11)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Aruba', N'AW', 0, 1, 0, 12)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Australia', N'AU', 0, 1, 0, 13)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Austria', N'AT', 0, 1, 0, 14)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Azerbaijan', N'AZ', 0, 1, 0, 15)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Bahamas The', N'BS', 0, 1, 0, 16)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Bahrain', N'BH', 0, 1, 0, 17)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Bangladesh', N'BD', 0, 1, 0, 18)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Barbados', N'BB', 0, 1, 0, 19)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Belarus', N'BY', 0, 1, 0, 20)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Belgium', N'BE', 0, 1, 0, 21)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Belize', N'BZ', 0, 1, 0, 22)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Benin', N'BJ', 0, 1, 0, 23)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Bermuda', N'BM', 0, 1, 0, 24)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Bhutan', N'BT', 0, 1, 0, 25)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Bolivia', N'BO', 0, 1, 0, 26)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Bosnia and Herzegovina', N'BA', 0, 1, 0, 27)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Botswana', N'BW', 0, 1, 0, 28)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Bouvet Island', N'BV', 0, 1, 0, 29)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Brazil', N'BR', 0, 1, 0, 30)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'British Indian Ocean Territory', N'IO', 0, 1, 0, 31)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Brunei', N'BN', 0, 1, 0, 32)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Bulgaria', N'BG', 0, 1, 0, 33)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Burkina Faso', N'BF', 0, 1, 0, 34)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Burundi', N'BI', 0, 1, 0, 35)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Cambodia', N'KH', 0, 1, 0, 36)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Cameroon', N'CM', 0, 1, 0, 37)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Canada', N'CA', 0, 1, 0, 38)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Cape Verde', N'CV', 0, 1, 0, 39)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Cayman Islands', N'KY', 0, 1, 0, 40)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Central African Republic', N'CF', 0, 1, 0, 41)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Chad', N'TD', 0, 1, 0, 42)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Chile', N'CL', 0, 1, 0, 43)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'China', N'CN', 0, 1, 0, 44)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Christmas Island', N'CX', 0, 1, 0, 45)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Cocos (Keeling) Islands', N'CC', 0, 1, 0, 46)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Colombia', N'CO', 0, 1, 0, 47)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Comoros', N'KM', 0, 1, 0, 48)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Republic Of The Congo', N'CG', 0, 1, 0, 49)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Democratic Republic Of The Congo', N'CD', 0, 1, 0, 50)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Cook Islands', N'CK', 0, 1, 0, 51)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Costa Rica', N'CR', 0, 1, 0, 52)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Cote D''''Ivoire (Ivory Coast)', N'CI', 0, 1, 0, 53)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Croatia (Hrvatska)', N'HR', 0, 1, 0, 54)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Cuba', N'CU', 0, 1, 0, 55)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Cyprus', N'CY', 0, 1, 0, 56)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Czech Republic', N'CZ', 0, 1, 0, 57)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Denmark', N'DK', 0, 1, 0, 58)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Djibouti', N'DJ', 0, 1, 0, 59)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Dominica', N'DM', 0, 1, 0, 60)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Dominican Republic', N'DO', 0, 1, 0, 61)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'East Timor', N'TP', 0, 1, 0, 62)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Ecuador', N'EC', 0, 1, 0, 63)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Egypt', N'EG', 0, 1, 0, 64)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'El Salvador', N'SV', 0, 1, 0, 65)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Equatorial Guinea', N'GQ', 0, 1, 0, 66)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Eritrea', N'ER', 0, 1, 0, 67)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Estonia', N'EE', 0, 1, 0, 68)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Ethiopia', N'ET', 0, 1, 0, 69)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'External Territories of Australia', N'XA', 0, 1, 0, 70)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Falkland Islands', N'FK', 0, 1, 0, 71)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Faroe Islands', N'FO', 0, 1, 0, 72)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Fiji Islands', N'FJ', 0, 1, 0, 73)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Finland', N'FI', 0, 1, 0, 74)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'France', N'FR', 0, 1, 0, 75)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'French Guiana', N'GF', 0, 1, 0, 76)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'French Polynesia', N'PF', 0, 1, 0, 77)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'French Southern Territories', N'TF', 0, 1, 0, 78)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Gabon', N'GA', 0, 1, 0, 79)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Gambia The', N'GM', 0, 1, 0, 80)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Georgia', N'GE', 0, 1, 0, 81)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Germany', N'DE', 0, 1, 0, 82)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Ghana', N'GH', 0, 1, 0, 83)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Gibraltar', N'GI', 0, 1, 0, 84)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Greece', N'GR', 0, 1, 0, 85)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Greenland', N'GL', 0, 1, 0, 86)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Grenada', N'GD', 0, 1, 0, 87)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Guadeloupe', N'GP', 0, 1, 0, 88)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Guam', N'GU', 0, 1, 0, 89)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Guatemala', N'GT', 0, 1, 0, 90)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Guernsey and Alderney', N'XU', 0, 1, 0, 91)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Guinea', N'GN', 0, 1, 0, 92)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Guinea-Bissau', N'GW', 0, 1, 0, 93)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Guyana', N'GY', 0, 1, 0, 94)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Haiti', N'HT', 0, 1, 0, 95)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Heard and McDonald Islands', N'HM', 0, 1, 0, 96)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Honduras', N'HN', 0, 1, 0, 97)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Hong Kong S.A.R.', N'HK', 0, 1, 0, 98)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Hungary', N'HU', 0, 1, 0, 99)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Iceland', N'IS', 0, 1, 0, 100)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'India', N'IN', 1, 1, 0, 101)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Indonesia', N'ID', 0, 1, 0, 102)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Iran', N'IR', 0, 1, 0, 103)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Iraq', N'IQ', 0, 1, 0, 104)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Ireland', N'IE', 0, 1, 0, 105)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Israel', N'IL', 0, 1, 0, 106)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Italy', N'IT', 0, 1, 0, 107)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Jamaica', N'JM', 0, 1, 0, 108)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Japan', N'JP', 0, 1, 0, 109)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Jersey', N'XJ', 0, 1, 0, 110)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Jordan', N'JO', 0, 1, 0, 111)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Kazakhstan', N'KZ', 0, 1, 0, 112)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Kenya', N'KE', 0, 1, 0, 113)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Kiribati', N'KI', 0, 1, 0, 114)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Korea North', N'KP', 0, 1, 0, 115)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Korea South', N'KR', 0, 1, 0, 116)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Kuwait', N'KW', 0, 1, 0, 117)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Kyrgyzstan', N'KG', 0, 1, 0, 118)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Laos', N'LA', 0, 1, 0, 119)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Latvia', N'LV', 0, 1, 0, 120)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Lebanon', N'LB', 0, 1, 0, 121)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Lesotho', N'LS', 0, 1, 0, 122)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Liberia', N'LR', 0, 1, 0, 123)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Libya', N'LY', 0, 1, 0, 124)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Liechtenstein', N'LI', 0, 1, 0, 125)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Lithuania', N'LT', 0, 1, 0, 126)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Luxembourg', N'LU', 0, 1, 0, 127)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Macau S.A.R.', N'MO', 0, 1, 0, 128)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Macedonia', N'MK', 0, 1, 0, 129)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Madagascar', N'MG', 0, 1, 0, 130)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Malawi', N'MW', 0, 1, 0, 131)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Malaysia', N'MY', 0, 1, 0, 132)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Maldives', N'MV', 0, 1, 0, 133)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Mali', N'ML', 0, 1, 0, 134)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Malta', N'MT', 0, 1, 0, 135)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Man (Isle of)', N'XM', 0, 1, 0, 136)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Marshall Islands', N'MH', 0, 1, 0, 137)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Martinique', N'MQ', 0, 1, 0, 138)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Mauritania', N'MR', 0, 1, 0, 139)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Mauritius', N'MU', 0, 1, 0, 140)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Mayotte', N'YT', 0, 1, 0, 141)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Mexico', N'MX', 0, 1, 0, 142)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Micronesia', N'FM', 0, 1, 0, 143)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Moldova', N'MD', 0, 1, 0, 144)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Monaco', N'MC', 0, 1, 0, 145)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Mongolia', N'MN', 0, 1, 0, 146)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Montserrat', N'MS', 0, 1, 0, 147)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Morocco', N'MA', 0, 1, 0, 148)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Mozambique', N'MZ', 0, 1, 0, 149)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Myanmar', N'MM', 0, 1, 0, 150)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Namibia', N'NA', 0, 1, 0, 151)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Nauru', N'NR', 0, 1, 0, 152)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Nepal', N'NP', 0, 1, 0, 153)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Netherlands Antilles', N'AN', 0, 1, 0, 154)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Netherlands The', N'NL', 0, 1, 0, 155)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'New Caledonia', N'NC', 0, 1, 0, 156)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'New Zealand', N'NZ', 0, 1, 0, 157)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Nicaragua', N'NI', 0, 1, 0, 158)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Niger', N'NE', 0, 1, 0, 159)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Nigeria', N'NG', 0, 1, 0, 160)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Niue', N'NU', 0, 1, 0, 161)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Norfolk Island', N'NF', 0, 1, 0, 162)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Northern Mariana Islands', N'MP', 0, 1, 0, 163)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Norway', N'NO', 0, 1, 0, 164)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Oman', N'OM', 0, 1, 0, 165)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Pakistan', N'PK', 0, 1, 0, 166)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Palau', N'PW', 0, 1, 0, 167)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Palestinian Territory Occupied', N'PS', 0, 1, 0, 168)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Panama', N'PA', 0, 1, 0, 169)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Papua new Guinea', N'PG', 0, 1, 0, 170)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Paraguay', N'PY', 0, 1, 0, 171)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Peru', N'PE', 0, 1, 0, 172)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Philippines', N'PH', 0, 1, 0, 173)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Pitcairn Island', N'PN', 0, 1, 0, 174)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Poland', N'PL', 0, 1, 0, 175)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Portugal', N'PT', 0, 1, 0, 176)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Puerto Rico', N'PR', 0, 1, 0, 177)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Qatar', N'QA', 0, 1, 0, 178)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Reunion', N'RE', 0, 1, 0, 179)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Romania', N'RO', 0, 1, 0, 180)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Russia', N'RU', 0, 1, 0, 181)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Rwanda', N'RW', 0, 1, 0, 182)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Saint Helena', N'SH', 0, 1, 0, 183)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Saint Kitts And Nevis', N'KN', 0, 1, 0, 184)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Saint Lucia', N'LC', 0, 1, 0, 185)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Saint Pierre and Miquelon', N'PM', 0, 1, 0, 186)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Saint Vincent And The Grenadines', N'VC', 0, 1, 0, 187)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Samoa', N'WS', 0, 1, 0, 188)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'San Marino', N'SM', 0, 1, 0, 189)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Sao Tome and Principe', N'ST', 0, 1, 0, 190)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Saudi Arabia', N'SA', 0, 1, 0, 191)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Senegal', N'SN', 0, 1, 0, 192)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Serbia', N'RS', 0, 1, 0, 193)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Seychelles', N'SC', 0, 1, 0, 194)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Sierra Leone', N'SL', 0, 1, 0, 195)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Singapore', N'SG', 0, 1, 0, 196)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Slovakia', N'SK', 0, 1, 0, 197)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Slovenia', N'SI', 0, 1, 0, 198)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Smaller Territories of the UK', N'XG', 0, 1, 0, 199)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Solomon Islands', N'SB', 0, 1, 0, 200)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Somalia', N'SO', 0, 1, 0, 201)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'South Africa', N'ZA', 0, 1, 0, 202)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'South Georgia', N'GS', 0, 1, 0, 203)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'South Sudan', N'SS', 0, 1, 0, 204)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Spain', N'ES', 0, 1, 0, 205)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Sri Lanka', N'LK', 0, 1, 0, 206)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Sudan', N'SD', 0, 1, 0, 207)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Suriname', N'SR', 0, 1, 0, 208)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Svalbard And Jan Mayen Islands', N'SJ', 0, 1, 0, 209)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Swaziland', N'SZ', 0, 1, 0, 210)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Sweden', N'SE', 0, 1, 0, 211)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Switzerland', N'CH', 0, 1, 0, 212)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Syria', N'SY', 0, 1, 0, 213)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Taiwan', N'TW', 0, 1, 0, 214)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Tajikistan', N'TJ', 0, 1, 0, 215)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Tanzania', N'TZ', 0, 1, 0, 216)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Thailand', N'TH', 0, 1, 0, 217)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Togo', N'TG', 0, 1, 0, 218)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Tokelau', N'TK', 0, 1, 0, 219)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Tonga', N'TO', 0, 1, 0, 220)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Trinidad And Tobago', N'TT', 0, 1, 0, 221)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Tunisia', N'TN', 0, 1, 0, 222)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Turkey', N'TR', 0, 1, 0, 223)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Turkmenistan', N'TM', 0, 1, 0, 224)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Turks And Caicos Islands', N'TC', 0, 1, 0, 225)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Tuvalu', N'TV', 0, 1, 0, 226)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Uganda', N'UG', 0, 1, 0, 227)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Ukraine', N'UA', 0, 1, 0, 228)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'United Arab Emirates', N'AE', 0, 1, 0, 229)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'United Kingdom', N'GB', 0, 1, 0, 230)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'United States', N'US', 1, 1, 0, 231)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'United States Minor Outlying Islands', N'UM', 0, 1, 0, 232)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Uruguay', N'UY', 0, 1, 0, 233)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Uzbekistan', N'UZ', 0, 1, 0, 234)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Vanuatu', N'VU', 0, 1, 0, 235)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Vatican City State (Holy See)', N'VA', 0, 1, 0, 236)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Venezuela', N'VE', 0, 1, 0, 237)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Vietnam', N'VN', 0, 1, 0, 238)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Virgin Islands (British)', N'VG', 0, 1, 0, 239)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Virgin Islands (US)', N'VI', 0, 1, 0, 240)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Wallis And Futuna Islands', N'WF', 0, 1, 0, 241)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Western Sahara', N'EH', 0, 1, 0, 242)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Yemen', N'YE', 0, 1, 0, 243)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Yugoslavia', N'YU', 0, 1, 0, 244)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Zambia', N'ZM', 0, 1, 0, 245)

INSERT [Country] ([Name], [Code], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (N'Zimbabwe', N'ZW', 0, 1, 0, 246)

SET IDENTITY_INSERT [Country] OFF

SET IDENTITY_INSERT [CustomLabel] ON 


INSERT [CustomLabel] ([Text], [DisplayOrder], [Id], [Type]) VALUES (N'I placed the order by mistake', 0, 1, N'cancellationReason')

INSERT [CustomLabel] ([Text], [DisplayOrder], [Id], [Type]) VALUES (N'I bought the item from somewhere else', 0, 2, N'cancellationReason')

INSERT [CustomLabel] ([Text], [DisplayOrder], [Id], [Type]) VALUES (N'The item is taking too long to deliver', 0, 3, N'cancellationReason')

INSERT [CustomLabel] ([Text], [DisplayOrder], [Id], [Type]) VALUES (N'I forgot to apply a coupon that I have', 0, 4, N'cancellationReason')

SET IDENTITY_INSERT [CustomLabel] OFF

SET IDENTITY_INSERT [SeoMeta] ON 


INSERT [SeoMeta] ([PageTitle], [MetaDescription], [MetaKeywords], [EntityId], [EntityName], [Slug], [LanguageCultureCode], [Id]) VALUES (NULL, NULL, NULL, 2, N'Product', N'hp-15-core-i3-7th-gen-156-inch-laptop', N'en-US', 2)

INSERT [SeoMeta] ([PageTitle], [MetaDescription], [MetaKeywords], [EntityId], [EntityName], [Slug], [LanguageCultureCode], [Id]) VALUES (NULL, NULL, NULL, 1, N'Category', N'computers-accessories', N'en-US', 3)

INSERT [SeoMeta] ([PageTitle], [MetaDescription], [MetaKeywords], [EntityId], [EntityName], [Slug], [LanguageCultureCode], [Id]) VALUES (NULL, NULL, NULL, 2, N'Category', N'laptops', N'en-US', 4)

INSERT [SeoMeta] ([PageTitle], [MetaDescription], [MetaKeywords], [EntityId], [EntityName], [Slug], [LanguageCultureCode], [Id]) VALUES (NULL, NULL, NULL, 3, N'Product', N'microsoft-surface-pro-6-1796-2019-123-inch-la', N'en-US', 5)

INSERT [SeoMeta] ([PageTitle], [MetaDescription], [MetaKeywords], [EntityId], [EntityName], [Slug], [LanguageCultureCode], [Id]) VALUES (NULL, NULL, NULL, 4, N'Product', N'apple-iphone-x', N'en-US', 6)

INSERT [SeoMeta] ([PageTitle], [MetaDescription], [MetaKeywords], [EntityId], [EntityName], [Slug], [LanguageCultureCode], [Id]) VALUES (NULL, NULL, NULL, 3, N'Category', N'mobiles-accessories', N'en-US', 7)

INSERT [SeoMeta] ([PageTitle], [MetaDescription], [MetaKeywords], [EntityId], [EntityName], [Slug], [LanguageCultureCode], [Id]) VALUES (NULL, NULL, NULL, 4, N'Category', N'mobiles', N'en-US', 8)

INSERT [SeoMeta] ([PageTitle], [MetaDescription], [MetaKeywords], [EntityId], [EntityName], [Slug], [LanguageCultureCode], [Id]) VALUES (NULL, NULL, NULL, 5, N'Category', N'apple-mobiles', N'en-US', 9)

INSERT [SeoMeta] ([PageTitle], [MetaDescription], [MetaKeywords], [EntityId], [EntityName], [Slug], [LanguageCultureCode], [Id]) VALUES (NULL, NULL, NULL, 5, N'Product', N'canon-pixma-g-3000', N'en-US', 10)

INSERT [SeoMeta] ([PageTitle], [MetaDescription], [MetaKeywords], [EntityId], [EntityName], [Slug], [LanguageCultureCode], [Id]) VALUES (NULL, NULL, NULL, 6, N'Category', N'printers', N'en-US', 11)

INSERT [SeoMeta] ([PageTitle], [MetaDescription], [MetaKeywords], [EntityId], [EntityName], [Slug], [LanguageCultureCode], [Id]) VALUES (NULL, NULL, NULL, 6, N'Product', N'hp-310-all-in-one-ink-tank-color-printer', N'en-US', 12)

INSERT [SeoMeta] ([PageTitle], [MetaDescription], [MetaKeywords], [EntityId], [EntityName], [Slug], [LanguageCultureCode], [Id]) VALUES (NULL, NULL, NULL, 7, N'Product', N'canon-pg-47-ink-cartridge', N'en-US', 13)

INSERT [SeoMeta] ([PageTitle], [MetaDescription], [MetaKeywords], [EntityId], [EntityName], [Slug], [LanguageCultureCode], [Id]) VALUES (NULL, NULL, NULL, 7, N'Category', N'accessories', N'en-US', 14)

INSERT [SeoMeta] ([PageTitle], [MetaDescription], [MetaKeywords], [EntityId], [EntityName], [Slug], [LanguageCultureCode], [Id]) VALUES (NULL, NULL, NULL, 8, N'Product', N'apple-watch-series-4', N'en-US', 15)

INSERT [SeoMeta] ([PageTitle], [MetaDescription], [MetaKeywords], [EntityId], [EntityName], [Slug], [LanguageCultureCode], [Id]) VALUES (NULL, NULL, NULL, 8, N'Category', N'smart-watches', N'en-US', 16)

INSERT [SeoMeta] ([PageTitle], [MetaDescription], [MetaKeywords], [EntityId], [EntityName], [Slug], [LanguageCultureCode], [Id]) VALUES (NULL, NULL, NULL, 9, N'Product', N'wd-elements-portable', N'en-US', 17)

INSERT [SeoMeta] ([PageTitle], [MetaDescription], [MetaKeywords], [EntityId], [EntityName], [Slug], [LanguageCultureCode], [Id]) VALUES (NULL, NULL, NULL, 9, N'Category', N'storage', N'en-US', 18)

INSERT [SeoMeta] ([PageTitle], [MetaDescription], [MetaKeywords], [EntityId], [EntityName], [Slug], [LanguageCultureCode], [Id]) VALUES (NULL, NULL, NULL, 10, N'Category', N'technology', N'en-US', 19)

INSERT [SeoMeta] ([PageTitle], [MetaDescription], [MetaKeywords], [EntityId], [EntityName], [Slug], [LanguageCultureCode], [Id]) VALUES (NULL, NULL, NULL, 10, N'Product', N'my-passport-x', N'en-US', 20)

INSERT [SeoMeta] ([PageTitle], [MetaDescription], [MetaKeywords], [EntityId], [EntityName], [Slug], [LanguageCultureCode], [Id]) VALUES (NULL, NULL, NULL, 11, N'Product', N'samsung-led-smart-tv', N'en-US', 21)

INSERT [SeoMeta] ([PageTitle], [MetaDescription], [MetaKeywords], [EntityId], [EntityName], [Slug], [LanguageCultureCode], [Id]) VALUES (NULL, NULL, NULL, 11, N'Category', N'electronics', N'en-US', 22)

INSERT [SeoMeta] ([PageTitle], [MetaDescription], [MetaKeywords], [EntityId], [EntityName], [Slug], [LanguageCultureCode], [Id]) VALUES (NULL, NULL, NULL, 12, N'Category', N'televisions', N'en-US', 23)

INSERT [SeoMeta] ([PageTitle], [MetaDescription], [MetaKeywords], [EntityId], [EntityName], [Slug], [LanguageCultureCode], [Id]) VALUES (NULL, NULL, NULL, 12, N'Product', N'sony-bravia-full-hd-led-smart-tv', N'en-US', 24)

INSERT [SeoMeta] ([PageTitle], [MetaDescription], [MetaKeywords], [EntityId], [EntityName], [Slug], [LanguageCultureCode], [Id]) VALUES (NULL, NULL, NULL, 13, N'Product', N'kaspersky-internet-security', N'en-US', 25)

INSERT [SeoMeta] ([PageTitle], [MetaDescription], [MetaKeywords], [EntityId], [EntityName], [Slug], [LanguageCultureCode], [Id]) VALUES (NULL, NULL, NULL, 13, N'Category', N'software', N'en-US', 26)

INSERT [SeoMeta] ([PageTitle], [MetaDescription], [MetaKeywords], [EntityId], [EntityName], [Slug], [LanguageCultureCode], [Id]) VALUES (NULL, NULL, NULL, 14, N'Category', N'security', N'en-US', 27)

INSERT [SeoMeta] ([PageTitle], [MetaDescription], [MetaKeywords], [EntityId], [EntityName], [Slug], [LanguageCultureCode], [Id]) VALUES (NULL, NULL, NULL, 14, N'Product', N'microsoft-office-home-and-business-2016', N'en-US', 28)

INSERT [SeoMeta] ([PageTitle], [MetaDescription], [MetaKeywords], [EntityId], [EntityName], [Slug], [LanguageCultureCode], [Id]) VALUES (NULL, NULL, NULL, 15, N'Category', N'utilities', N'en-US', 29)

INSERT [SeoMeta] ([PageTitle], [MetaDescription], [MetaKeywords], [EntityId], [EntityName], [Slug], [LanguageCultureCode], [Id]) VALUES (NULL, NULL, NULL, 1, N'ContentPage', N'about-us', N'en-US', 30)

INSERT [SeoMeta] ([PageTitle], [MetaDescription], [MetaKeywords], [EntityId], [EntityName], [Slug], [LanguageCultureCode], [Id]) VALUES (NULL, NULL, NULL, 2, N'ContentPage', N'contact-us', N'en-US', 31)

INSERT [SeoMeta] ([PageTitle], [MetaDescription], [MetaKeywords], [EntityId], [EntityName], [Slug], [LanguageCultureCode], [Id]) VALUES (NULL, NULL, NULL, 3, N'ContentPage', N'privacy-policy', N'en-US', 32)

INSERT [SeoMeta] ([PageTitle], [MetaDescription], [MetaKeywords], [EntityId], [EntityName], [Slug], [LanguageCultureCode], [Id]) VALUES (NULL, NULL, NULL, 4, N'ContentPage', N'terms-conditions', N'en-US', 33)

INSERT [SeoMeta] ([PageTitle], [MetaDescription], [MetaKeywords], [EntityId], [EntityName], [Slug], [LanguageCultureCode], [Id]) VALUES (NULL, NULL, NULL, 16, N'Product', N'subscription-product', N'en-US', 35)

SET IDENTITY_INSERT [SeoMeta] OFF

SET IDENTITY_INSERT [StateOrProvince] ON 


INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (101, N'Andaman and Nicobar Islands', 1, 1, 0, 1)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (101, N'Andhra Pradesh', 1, 1, 0, 2)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (101, N'Arunachal Pradesh', 1, 1, 0, 3)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (101, N'Assam', 1, 1, 0, 4)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (101, N'Bihar', 1, 1, 0, 5)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (101, N'Chandigarh', 1, 1, 0, 6)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (101, N'Chhattisgarh', 1, 1, 0, 7)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (101, N'Dadra and Nagar Haveli', 1, 1, 0, 8)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (101, N'Daman and Diu', 1, 1, 0, 9)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (101, N'Delhi', 1, 1, 0, 10)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (101, N'Goa', 1, 1, 0, 11)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (101, N'Gujarat', 1, 1, 0, 12)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (101, N'Haryana', 1, 1, 0, 13)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (101, N'Himachal Pradesh', 1, 1, 0, 14)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (101, N'Jammu and Kashmir', 1, 1, 0, 15)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (101, N'Jharkhand', 1, 1, 0, 16)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (101, N'Karnataka', 1, 1, 0, 17)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (101, N'Kerala', 1, 1, 0, 19)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (101, N'Lakshadweep', 1, 1, 0, 20)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (101, N'Madhya Pradesh', 1, 1, 0, 21)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (101, N'Maharashtra', 1, 1, 0, 22)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (101, N'Manipur', 1, 1, 0, 23)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (101, N'Meghalaya', 1, 1, 0, 24)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (101, N'Mizoram', 1, 1, 0, 25)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (101, N'Nagaland', 1, 1, 0, 26)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (101, N'Odisha', 1, 1, 0, 29)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (101, N'Pondicherry', 1, 1, 0, 31)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (101, N'Punjab', 1, 1, 0, 32)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (101, N'Rajasthan', 1, 1, 0, 33)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (101, N'Sikkim', 1, 1, 0, 34)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (101, N'Tamil Nadu', 1, 1, 0, 35)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (101, N'Telangana', 1, 1, 0, 36)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (101, N'Tripura', 1, 1, 0, 37)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (101, N'Uttar Pradesh', 1, 1, 0, 38)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (101, N'Uttarakhand', 1, 1, 0, 39)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (101, N'West Bengal', 1, 1, 0, 41)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (1, N'Badakhshan', 1, 1, 0, 42)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (1, N'Badgis', 1, 1, 0, 43)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (1, N'Baglan', 1, 1, 0, 44)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (1, N'Balkh', 1, 1, 0, 45)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (1, N'Bamiyan', 1, 1, 0, 46)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (1, N'Farah', 1, 1, 0, 47)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (1, N'Faryab', 1, 1, 0, 48)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (1, N'Gawr', 1, 1, 0, 49)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (1, N'Gazni', 1, 1, 0, 50)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (1, N'Herat', 1, 1, 0, 51)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (1, N'Hilmand', 1, 1, 0, 52)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (1, N'Jawzjan', 1, 1, 0, 53)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (1, N'Kabul', 1, 1, 0, 54)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (1, N'Kapisa', 1, 1, 0, 55)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (1, N'Khawst', 1, 1, 0, 56)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (1, N'Kunar', 1, 1, 0, 57)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (1, N'Lagman', 1, 1, 0, 58)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (1, N'Lawghar', 1, 1, 0, 59)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (1, N'Nangarhar', 1, 1, 0, 60)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (1, N'Nimruz', 1, 1, 0, 61)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (1, N'Nuristan', 1, 1, 0, 62)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (1, N'Paktika', 1, 1, 0, 63)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (1, N'Paktiya', 1, 1, 0, 64)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (1, N'Parwan', 1, 1, 0, 65)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (1, N'Qandahar', 1, 1, 0, 66)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (1, N'Qunduz', 1, 1, 0, 67)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (1, N'Samangan', 1, 1, 0, 68)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (1, N'Sar-e Pul', 1, 1, 0, 69)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (1, N'Takhar', 1, 1, 0, 70)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (1, N'Uruzgan', 1, 1, 0, 71)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (1, N'Wardag', 1, 1, 0, 72)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (1, N'Zabul', 1, 1, 0, 73)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (2, N'Berat', 1, 1, 0, 74)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (2, N'Bulqize', 1, 1, 0, 75)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (2, N'Delvine', 1, 1, 0, 76)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (2, N'Devoll', 1, 1, 0, 77)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (2, N'Dibre', 1, 1, 0, 78)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (2, N'Durres', 1, 1, 0, 79)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (2, N'Elbasan', 1, 1, 0, 80)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (2, N'Fier', 1, 1, 0, 81)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (2, N'Gjirokaster', 1, 1, 0, 82)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (2, N'Gramsh', 1, 1, 0, 83)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (2, N'Has', 1, 1, 0, 84)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (2, N'Kavaje', 1, 1, 0, 85)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (2, N'Kolonje', 1, 1, 0, 86)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (2, N'Korce', 1, 1, 0, 87)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (2, N'Kruje', 1, 1, 0, 88)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (2, N'Kucove', 1, 1, 0, 89)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (2, N'Kukes', 1, 1, 0, 90)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (2, N'Kurbin', 1, 1, 0, 91)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (2, N'Lezhe', 1, 1, 0, 92)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (2, N'Librazhd', 1, 1, 0, 93)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (2, N'Lushnje', 1, 1, 0, 94)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (2, N'Mallakaster', 1, 1, 0, 95)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (2, N'Malsi e Madhe', 1, 1, 0, 96)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (2, N'Mat', 1, 1, 0, 97)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (2, N'Mirdite', 1, 1, 0, 98)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (2, N'Peqin', 1, 1, 0, 99)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (2, N'Permet', 1, 1, 0, 100)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (2, N'Pogradec', 1, 1, 0, 101)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (2, N'Puke', 1, 1, 0, 102)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (2, N'Sarande', 1, 1, 0, 103)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (2, N'Shkoder', 1, 1, 0, 104)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (2, N'Skrapar', 1, 1, 0, 105)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (2, N'Tepelene', 1, 1, 0, 106)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (2, N'Tirane', 1, 1, 0, 107)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (2, N'Tropoje', 1, 1, 0, 108)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (2, N'Vlore', 1, 1, 0, 109)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (3, N'Ayn Daflah', 1, 1, 0, 110)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (3, N'Ayn Tamushanat', 1, 1, 0, 111)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (3, N'Adrar', 1, 1, 0, 112)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (3, N'Algiers', 1, 1, 0, 113)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (3, N'Annabah', 1, 1, 0, 114)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (3, N'Bashshar', 1, 1, 0, 115)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (3, N'Batnah', 1, 1, 0, 116)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (3, N'Bijayah', 1, 1, 0, 117)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (3, N'Biskrah', 1, 1, 0, 118)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (3, N'Blidah', 1, 1, 0, 119)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (3, N'Buirah', 1, 1, 0, 120)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (3, N'Bumardas', 1, 1, 0, 121)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (3, N'Burj Bu Arririj', 1, 1, 0, 122)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (3, N'Ghalizan', 1, 1, 0, 123)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (3, N'Ghardayah', 1, 1, 0, 124)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (3, N'Ilizi', 1, 1, 0, 125)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (3, N'Jijili', 1, 1, 0, 126)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (3, N'Jilfah', 1, 1, 0, 127)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (3, N'Khanshalah', 1, 1, 0, 128)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (3, N'Masilah', 1, 1, 0, 129)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (3, N'Midyah', 1, 1, 0, 130)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (3, N'Milah', 1, 1, 0, 131)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (3, N'Muaskar', 1, 1, 0, 132)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (3, N'Mustaghanam', 1, 1, 0, 133)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (3, N'Naama', 1, 1, 0, 134)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (3, N'Oran', 1, 1, 0, 135)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (3, N'Ouargla', 1, 1, 0, 136)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (3, N'Qalmah', 1, 1, 0, 137)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (3, N'Qustantinah', 1, 1, 0, 138)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (3, N'Sakikdah', 1, 1, 0, 139)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (3, N'Satif', 1, 1, 0, 140)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (3, N'Sayda', 1, 1, 0, 141)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (3, N'Sidi ban-al-''''Abbas', 1, 1, 0, 142)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (3, N'Suq Ahras', 1, 1, 0, 143)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (3, N'Tamanghasat', 1, 1, 0, 144)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (3, N'Tibazah', 1, 1, 0, 145)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (3, N'Tibissah', 1, 1, 0, 146)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (3, N'Tilimsan', 1, 1, 0, 147)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (3, N'Tinduf', 1, 1, 0, 148)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (3, N'Tisamsilt', 1, 1, 0, 149)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (3, N'Tiyarat', 1, 1, 0, 150)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (3, N'Tizi Wazu', 1, 1, 0, 151)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (3, N'Umm-al-Bawaghi', 1, 1, 0, 152)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (3, N'Wahran', 1, 1, 0, 153)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (3, N'Warqla', 1, 1, 0, 154)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (3, N'Wilaya d Alger', 1, 1, 0, 155)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (3, N'Wilaya de Bejaia', 1, 1, 0, 156)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (3, N'Wilaya de Constantine', 1, 1, 0, 157)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (3, N'al-Aghwat', 1, 1, 0, 158)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (3, N'al-Bayadh', 1, 1, 0, 159)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (3, N'al-Jaza''''ir', 1, 1, 0, 160)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (3, N'al-Wad', 1, 1, 0, 161)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (3, N'ash-Shalif', 1, 1, 0, 162)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (3, N'at-Tarif', 1, 1, 0, 163)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (4, N'Eastern', 1, 1, 0, 164)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (4, N'Manu''''a', 1, 1, 0, 165)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (4, N'Swains Island', 1, 1, 0, 166)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (4, N'Western', 1, 1, 0, 167)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (5, N'Andorra la Vella', 1, 1, 0, 168)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (5, N'Canillo', 1, 1, 0, 169)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (5, N'Encamp', 1, 1, 0, 170)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (5, N'La Massana', 1, 1, 0, 171)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (5, N'Les Escaldes', 1, 1, 0, 172)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (5, N'Ordino', 1, 1, 0, 173)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (5, N'Sant Julia de Loria', 1, 1, 0, 174)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (6, N'Bengo', 1, 1, 0, 175)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (6, N'Benguela', 1, 1, 0, 176)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (6, N'Bie', 1, 1, 0, 177)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (6, N'Cabinda', 1, 1, 0, 178)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (6, N'Cunene', 1, 1, 0, 179)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (6, N'Huambo', 1, 1, 0, 180)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (6, N'Huila', 1, 1, 0, 181)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (6, N'Kuando-Kubango', 1, 1, 0, 182)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (6, N'Kwanza Norte', 1, 1, 0, 183)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (6, N'Kwanza Sul', 1, 1, 0, 184)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (6, N'Luanda', 1, 1, 0, 185)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (6, N'Lunda Norte', 1, 1, 0, 186)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (6, N'Lunda Sul', 1, 1, 0, 187)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (6, N'Malanje', 1, 1, 0, 188)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (6, N'Moxico', 1, 1, 0, 189)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (6, N'Namibe', 1, 1, 0, 190)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (6, N'Uige', 1, 1, 0, 191)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (6, N'Zaire', 1, 1, 0, 192)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (7, N'Other Provinces', 1, 1, 0, 193)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (8, N'Sector claimed by Argentina/Ch', 1, 1, 0, 194)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (8, N'Sector claimed by Argentina/UK', 1, 1, 0, 195)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (8, N'Sector claimed by Australia', 1, 1, 0, 196)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (8, N'Sector claimed by France', 1, 1, 0, 197)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (8, N'Sector claimed by New Zealand', 1, 1, 0, 198)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (8, N'Sector claimed by Norway', 1, 1, 0, 199)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (8, N'Unclaimed Sector', 1, 1, 0, 200)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (9, N'Barbuda', 1, 1, 0, 201)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (9, N'Saint George', 1, 1, 0, 202)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (9, N'Saint John', 1, 1, 0, 203)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (9, N'Saint Mary', 1, 1, 0, 204)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (9, N'Saint Paul', 1, 1, 0, 205)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (9, N'Saint Peter', 1, 1, 0, 206)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (9, N'Saint Philip', 1, 1, 0, 207)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (10, N'Buenos Aires', 1, 1, 0, 208)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (10, N'Catamarca', 1, 1, 0, 209)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (10, N'Chaco', 1, 1, 0, 210)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (10, N'Chubut', 1, 1, 0, 211)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (10, N'Cordoba', 1, 1, 0, 212)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (10, N'Corrientes', 1, 1, 0, 213)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (10, N'Distrito Federal', 1, 1, 0, 214)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (10, N'Entre Rios', 1, 1, 0, 215)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (10, N'Formosa', 1, 1, 0, 216)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (10, N'Jujuy', 1, 1, 0, 217)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (10, N'La Pampa', 1, 1, 0, 218)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (10, N'La Rioja', 1, 1, 0, 219)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (10, N'Mendoza', 1, 1, 0, 220)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (10, N'Misiones', 1, 1, 0, 221)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (10, N'Neuquen', 1, 1, 0, 222)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (10, N'Rio Negro', 1, 1, 0, 223)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (10, N'Salta', 1, 1, 0, 224)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (10, N'San Juan', 1, 1, 0, 225)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (10, N'San Luis', 1, 1, 0, 226)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (10, N'Santa Cruz', 1, 1, 0, 227)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (10, N'Santa Fe', 1, 1, 0, 228)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (10, N'Santiago del Estero', 1, 1, 0, 229)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (10, N'Tierra del Fuego', 1, 1, 0, 230)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (10, N'Tucuman', 1, 1, 0, 231)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (11, N'Aragatsotn', 1, 1, 0, 232)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (11, N'Ararat', 1, 1, 0, 233)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (11, N'Armavir', 1, 1, 0, 234)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (11, N'Gegharkunik', 1, 1, 0, 235)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (11, N'Kotaik', 1, 1, 0, 236)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (11, N'Lori', 1, 1, 0, 237)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (11, N'Shirak', 1, 1, 0, 238)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (11, N'Stepanakert', 1, 1, 0, 239)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (11, N'Syunik', 1, 1, 0, 240)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (11, N'Tavush', 1, 1, 0, 241)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (11, N'Vayots Dzor', 1, 1, 0, 242)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (11, N'Yerevan', 1, 1, 0, 243)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (12, N'Aruba', 1, 1, 0, 244)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (13, N'Auckland', 1, 1, 0, 245)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (13, N'Australian Capital Territory', 1, 1, 0, 246)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (13, N'Balgowlah', 1, 1, 0, 247)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (13, N'Balmain', 1, 1, 0, 248)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (13, N'Bankstown', 1, 1, 0, 249)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (13, N'Baulkham Hills', 1, 1, 0, 250)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (13, N'Bonnet Bay', 1, 1, 0, 251)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (13, N'Camberwell', 1, 1, 0, 252)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (13, N'Carole Park', 1, 1, 0, 253)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (13, N'Castle Hill', 1, 1, 0, 254)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (13, N'Caulfield', 1, 1, 0, 255)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (13, N'Chatswood', 1, 1, 0, 256)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (13, N'Cheltenham', 1, 1, 0, 257)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (13, N'Cherrybrook', 1, 1, 0, 258)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (13, N'Clayton', 1, 1, 0, 259)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (13, N'Collingwood', 1, 1, 0, 260)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (13, N'Frenchs Forest', 1, 1, 0, 261)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (13, N'Hawthorn', 1, 1, 0, 262)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (13, N'Jannnali', 1, 1, 0, 263)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (13, N'Knoxfield', 1, 1, 0, 264)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (13, N'Melbourne', 1, 1, 0, 265)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (13, N'New South Wales', 1, 1, 0, 266)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (13, N'Northern Territory', 1, 1, 0, 267)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (13, N'Perth', 1, 1, 0, 268)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (13, N'Queensland', 1, 1, 0, 269)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (13, N'South Australia', 1, 1, 0, 270)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (13, N'Tasmania', 1, 1, 0, 271)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (13, N'Templestowe', 1, 1, 0, 272)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (13, N'Victoria', 1, 1, 0, 273)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (13, N'Werribee south', 1, 1, 0, 274)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (13, N'Western Australia', 1, 1, 0, 275)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (13, N'Wheeler', 1, 1, 0, 276)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (14, N'Bundesland Salzburg', 1, 1, 0, 277)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (14, N'Bundesland Steiermark', 1, 1, 0, 278)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (14, N'Bundesland Tirol', 1, 1, 0, 279)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (14, N'Burgenland', 1, 1, 0, 280)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (14, N'Carinthia', 1, 1, 0, 281)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (14, N'Karnten', 1, 1, 0, 282)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (14, N'Liezen', 1, 1, 0, 283)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (14, N'Lower Austria', 1, 1, 0, 284)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (14, N'Niederosterreich', 1, 1, 0, 285)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (14, N'Oberosterreich', 1, 1, 0, 286)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (14, N'Salzburg', 1, 1, 0, 287)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (14, N'Schleswig-Holstein', 1, 1, 0, 288)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (14, N'Steiermark', 1, 1, 0, 289)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (14, N'Styria', 1, 1, 0, 290)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (14, N'Tirol', 1, 1, 0, 291)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (14, N'Upper Austria', 1, 1, 0, 292)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (14, N'Vorarlberg', 1, 1, 0, 293)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (14, N'Wien', 1, 1, 0, 294)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (15, N'Abseron', 1, 1, 0, 295)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (15, N'Baki Sahari', 1, 1, 0, 296)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (15, N'Ganca', 1, 1, 0, 297)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (15, N'Ganja', 1, 1, 0, 298)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (15, N'Kalbacar', 1, 1, 0, 299)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (15, N'Lankaran', 1, 1, 0, 300)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (15, N'Mil-Qarabax', 1, 1, 0, 301)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (15, N'Mugan-Salyan', 1, 1, 0, 302)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (15, N'Nagorni-Qarabax', 1, 1, 0, 303)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (15, N'Naxcivan', 1, 1, 0, 304)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (15, N'Priaraks', 1, 1, 0, 305)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (15, N'Qazax', 1, 1, 0, 306)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (15, N'Saki', 1, 1, 0, 307)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (15, N'Sirvan', 1, 1, 0, 308)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (15, N'Xacmaz', 1, 1, 0, 309)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (16, N'Abaco', 1, 1, 0, 310)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (16, N'Acklins Island', 1, 1, 0, 311)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (16, N'Andros', 1, 1, 0, 312)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (16, N'Berry Islands', 1, 1, 0, 313)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (16, N'Biminis', 1, 1, 0, 314)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (16, N'Cat Island', 1, 1, 0, 315)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (16, N'Crooked Island', 1, 1, 0, 316)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (16, N'Eleuthera', 1, 1, 0, 317)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (16, N'Exuma and Cays', 1, 1, 0, 318)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (16, N'Grand Bahama', 1, 1, 0, 319)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (16, N'Inagua Islands', 1, 1, 0, 320)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (16, N'Long Island', 1, 1, 0, 321)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (16, N'Mayaguana', 1, 1, 0, 322)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (16, N'New Providence', 1, 1, 0, 323)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (16, N'Ragged Island', 1, 1, 0, 324)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (16, N'Rum Cay', 1, 1, 0, 325)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (16, N'San Salvador', 1, 1, 0, 326)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (17, N'Isa', 1, 1, 0, 327)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (17, N'Badiyah', 1, 1, 0, 328)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (17, N'Hidd', 1, 1, 0, 329)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (17, N'Jidd Hafs', 1, 1, 0, 330)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (17, N'Mahama', 1, 1, 0, 331)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (17, N'Manama', 1, 1, 0, 332)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (17, N'Sitrah', 1, 1, 0, 333)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (17, N'al-Manamah', 1, 1, 0, 334)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (17, N'al-Muharraq', 1, 1, 0, 335)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (17, N'ar-Rifa''''a', 1, 1, 0, 336)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (18, N'Bagar Hat', 1, 1, 0, 337)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (18, N'Bandarban', 1, 1, 0, 338)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (18, N'Barguna', 1, 1, 0, 339)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (18, N'Barisal', 1, 1, 0, 340)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (18, N'Bhola', 1, 1, 0, 341)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (18, N'Bogora', 1, 1, 0, 342)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (18, N'Brahman Bariya', 1, 1, 0, 343)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (18, N'Chandpur', 1, 1, 0, 344)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (18, N'Chattagam', 1, 1, 0, 345)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (18, N'Chittagong Division', 1, 1, 0, 346)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (18, N'Chuadanga', 1, 1, 0, 347)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (18, N'Dhaka', 1, 1, 0, 348)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (18, N'Dinajpur', 1, 1, 0, 349)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (18, N'Faridpur', 1, 1, 0, 350)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (18, N'Feni', 1, 1, 0, 351)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (18, N'Gaybanda', 1, 1, 0, 352)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (18, N'Gazipur', 1, 1, 0, 353)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (18, N'Gopalganj', 1, 1, 0, 354)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (18, N'Habiganj', 1, 1, 0, 355)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (18, N'Jaipur Hat', 1, 1, 0, 356)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (18, N'Jamalpur', 1, 1, 0, 357)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (18, N'Jessor', 1, 1, 0, 358)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (18, N'Jhalakati', 1, 1, 0, 359)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (18, N'Jhanaydah', 1, 1, 0, 360)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (18, N'Khagrachhari', 1, 1, 0, 361)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (18, N'Khulna', 1, 1, 0, 362)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (18, N'Kishorganj', 1, 1, 0, 363)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (18, N'Koks Bazar', 1, 1, 0, 364)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (18, N'Komilla', 1, 1, 0, 365)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (18, N'Kurigram', 1, 1, 0, 366)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (18, N'Kushtiya', 1, 1, 0, 367)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (18, N'Lakshmipur', 1, 1, 0, 368)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (18, N'Lalmanir Hat', 1, 1, 0, 369)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (18, N'Madaripur', 1, 1, 0, 370)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (18, N'Magura', 1, 1, 0, 371)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (18, N'Maimansingh', 1, 1, 0, 372)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (18, N'Manikganj', 1, 1, 0, 373)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (18, N'Maulvi Bazar', 1, 1, 0, 374)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (18, N'Meherpur', 1, 1, 0, 375)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (18, N'Munshiganj', 1, 1, 0, 376)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (18, N'Naral', 1, 1, 0, 377)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (18, N'Narayanganj', 1, 1, 0, 378)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (18, N'Narsingdi', 1, 1, 0, 379)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (18, N'Nator', 1, 1, 0, 380)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (18, N'Naugaon', 1, 1, 0, 381)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (18, N'Nawabganj', 1, 1, 0, 382)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (18, N'Netrakona', 1, 1, 0, 383)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (18, N'Nilphamari', 1, 1, 0, 384)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (18, N'Noakhali', 1, 1, 0, 385)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (18, N'Pabna', 1, 1, 0, 386)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (18, N'Panchagarh', 1, 1, 0, 387)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (18, N'Patuakhali', 1, 1, 0, 388)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (18, N'Pirojpur', 1, 1, 0, 389)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (18, N'Rajbari', 1, 1, 0, 390)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (18, N'Rajshahi', 1, 1, 0, 391)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (18, N'Rangamati', 1, 1, 0, 392)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (18, N'Rangpur', 1, 1, 0, 393)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (18, N'Satkhira', 1, 1, 0, 394)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (18, N'Shariatpur', 1, 1, 0, 395)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (18, N'Sherpur', 1, 1, 0, 396)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (18, N'Silhat', 1, 1, 0, 397)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (18, N'Sirajganj', 1, 1, 0, 398)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (18, N'Sunamganj', 1, 1, 0, 399)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (18, N'Tangayal', 1, 1, 0, 400)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (18, N'Thakurgaon', 1, 1, 0, 401)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (19, N'Christ Church', 1, 1, 0, 402)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (19, N'Saint Andrew', 1, 1, 0, 403)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (19, N'Saint George', 1, 1, 0, 404)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (19, N'Saint James', 1, 1, 0, 405)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (19, N'Saint John', 1, 1, 0, 406)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (19, N'Saint Joseph', 1, 1, 0, 407)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (19, N'Saint Lucy', 1, 1, 0, 408)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (19, N'Saint Michael', 1, 1, 0, 409)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (19, N'Saint Peter', 1, 1, 0, 410)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (19, N'Saint Philip', 1, 1, 0, 411)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (19, N'Saint Thomas', 1, 1, 0, 412)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (20, N'Brest', 1, 1, 0, 413)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (20, N'Homjel', 1, 1, 0, 414)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (20, N'Hrodna', 1, 1, 0, 415)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (20, N'Mahiljow', 1, 1, 0, 416)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (20, N'Mahilyowskaya Voblasts', 1, 1, 0, 417)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (20, N'Minsk', 1, 1, 0, 418)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (20, N'Minskaja Voblasts', 1, 1, 0, 419)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (20, N'Petrik', 1, 1, 0, 420)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (20, N'Vicebsk', 1, 1, 0, 421)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (21, N'Antwerpen', 1, 1, 0, 422)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (21, N'Berchem', 1, 1, 0, 423)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (21, N'Brabant', 1, 1, 0, 424)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (21, N'Brabant Wallon', 1, 1, 0, 425)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (21, N'Brussel', 1, 1, 0, 426)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (21, N'East Flanders', 1, 1, 0, 427)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (21, N'Hainaut', 1, 1, 0, 428)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (21, N'Liege', 1, 1, 0, 429)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (21, N'Limburg', 1, 1, 0, 430)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (21, N'Luxembourg', 1, 1, 0, 431)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (21, N'Namur', 1, 1, 0, 432)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (21, N'Ontario', 1, 1, 0, 433)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (21, N'Oost-Vlaanderen', 1, 1, 0, 434)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (21, N'Provincie Brabant', 1, 1, 0, 435)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (21, N'Vlaams-Brabant', 1, 1, 0, 436)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (21, N'Wallonne', 1, 1, 0, 437)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (21, N'West-Vlaanderen', 1, 1, 0, 438)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (22, N'Belize', 1, 1, 0, 439)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (22, N'Cayo', 1, 1, 0, 440)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (22, N'Corozal', 1, 1, 0, 441)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (22, N'Orange Walk', 1, 1, 0, 442)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (22, N'Stann Creek', 1, 1, 0, 443)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (22, N'Toledo', 1, 1, 0, 444)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (23, N'Alibori', 1, 1, 0, 445)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (23, N'Atacora', 1, 1, 0, 446)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (23, N'Atlantique', 1, 1, 0, 447)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (23, N'Borgou', 1, 1, 0, 448)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (23, N'Collines', 1, 1, 0, 449)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (23, N'Couffo', 1, 1, 0, 450)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (23, N'Donga', 1, 1, 0, 451)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (23, N'Littoral', 1, 1, 0, 452)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (23, N'Mono', 1, 1, 0, 453)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (23, N'Oueme', 1, 1, 0, 454)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (23, N'Plateau', 1, 1, 0, 455)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (23, N'Zou', 1, 1, 0, 456)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (24, N'Hamilton', 1, 1, 0, 457)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (24, N'Saint George', 1, 1, 0, 458)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (25, N'Bumthang', 1, 1, 0, 459)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (25, N'Chhukha', 1, 1, 0, 460)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (25, N'Chirang', 1, 1, 0, 461)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (25, N'Daga', 1, 1, 0, 462)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (25, N'Geylegphug', 1, 1, 0, 463)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (25, N'Ha', 1, 1, 0, 464)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (25, N'Lhuntshi', 1, 1, 0, 465)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (25, N'Mongar', 1, 1, 0, 466)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (25, N'Pemagatsel', 1, 1, 0, 467)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (25, N'Punakha', 1, 1, 0, 468)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (25, N'Rinpung', 1, 1, 0, 469)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (25, N'Samchi', 1, 1, 0, 470)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (25, N'Samdrup Jongkhar', 1, 1, 0, 471)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (25, N'Shemgang', 1, 1, 0, 472)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (25, N'Tashigang', 1, 1, 0, 473)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (25, N'Timphu', 1, 1, 0, 474)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (25, N'Tongsa', 1, 1, 0, 475)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (25, N'Wangdiphodrang', 1, 1, 0, 476)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (26, N'Beni', 1, 1, 0, 477)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (26, N'Chuquisaca', 1, 1, 0, 478)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (26, N'Cochabamba', 1, 1, 0, 479)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (26, N'La Paz', 1, 1, 0, 480)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (26, N'Oruro', 1, 1, 0, 481)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (26, N'Pando', 1, 1, 0, 482)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (26, N'Potosi', 1, 1, 0, 483)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (26, N'Santa Cruz', 1, 1, 0, 484)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (26, N'Tarija', 1, 1, 0, 485)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (27, N'Federacija Bosna i Hercegovina', 1, 1, 0, 486)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (27, N'Republika Srpska', 1, 1, 0, 487)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (28, N'Central Bobonong', 1, 1, 0, 488)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (28, N'Central Boteti', 1, 1, 0, 489)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (28, N'Central Mahalapye', 1, 1, 0, 490)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (28, N'Central Serowe-Palapye', 1, 1, 0, 491)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (28, N'Central Tutume', 1, 1, 0, 492)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (28, N'Chobe', 1, 1, 0, 493)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (28, N'Francistown', 1, 1, 0, 494)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (28, N'Gaborone', 1, 1, 0, 495)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (28, N'Ghanzi', 1, 1, 0, 496)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (28, N'Jwaneng', 1, 1, 0, 497)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (28, N'Kgalagadi North', 1, 1, 0, 498)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (28, N'Kgalagadi South', 1, 1, 0, 499)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (28, N'Kgatleng', 1, 1, 0, 500)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (28, N'Kweneng', 1, 1, 0, 501)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (28, N'Lobatse', 1, 1, 0, 502)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (28, N'Ngamiland', 1, 1, 0, 503)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (28, N'Ngwaketse', 1, 1, 0, 504)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (28, N'North East', 1, 1, 0, 505)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (28, N'Okavango', 1, 1, 0, 506)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (28, N'Orapa', 1, 1, 0, 507)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (28, N'Selibe Phikwe', 1, 1, 0, 508)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (28, N'South East', 1, 1, 0, 509)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (28, N'Sowa', 1, 1, 0, 510)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (29, N'Bouvet Island', 1, 1, 0, 511)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (30, N'Acre', 1, 1, 0, 512)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (30, N'Alagoas', 1, 1, 0, 513)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (30, N'Amapa', 1, 1, 0, 514)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (30, N'Amazonas', 1, 1, 0, 515)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (30, N'Bahia', 1, 1, 0, 516)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (30, N'Ceara', 1, 1, 0, 517)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (30, N'Distrito Federal', 1, 1, 0, 518)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (30, N'Espirito Santo', 1, 1, 0, 519)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (30, N'Estado de Sao Paulo', 1, 1, 0, 520)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (30, N'Goias', 1, 1, 0, 521)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (30, N'Maranhao', 1, 1, 0, 522)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (30, N'Mato Grosso', 1, 1, 0, 523)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (30, N'Mato Grosso do Sul', 1, 1, 0, 524)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (30, N'Minas Gerais', 1, 1, 0, 525)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (30, N'Para', 1, 1, 0, 526)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (30, N'Paraiba', 1, 1, 0, 527)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (30, N'Parana', 1, 1, 0, 528)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (30, N'Pernambuco', 1, 1, 0, 529)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (30, N'Piaui', 1, 1, 0, 530)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (30, N'Rio Grande do Norte', 1, 1, 0, 531)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (30, N'Rio Grande do Sul', 1, 1, 0, 532)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (30, N'Rio de Janeiro', 1, 1, 0, 533)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (30, N'Rondonia', 1, 1, 0, 534)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (30, N'Roraima', 1, 1, 0, 535)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (30, N'Santa Catarina', 1, 1, 0, 536)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (30, N'Sao Paulo', 1, 1, 0, 537)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (30, N'Sergipe', 1, 1, 0, 538)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (30, N'Tocantins', 1, 1, 0, 539)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (31, N'British Indian Ocean Territory', 1, 1, 0, 540)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (32, N'Belait', 1, 1, 0, 541)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (32, N'Brunei-Muara', 1, 1, 0, 542)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (32, N'Temburong', 1, 1, 0, 543)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (32, N'Tutong', 1, 1, 0, 544)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (33, N'Blagoevgrad', 1, 1, 0, 545)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (33, N'Burgas', 1, 1, 0, 546)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (33, N'Dobrich', 1, 1, 0, 547)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (33, N'Gabrovo', 1, 1, 0, 548)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (33, N'Haskovo', 1, 1, 0, 549)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (33, N'Jambol', 1, 1, 0, 550)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (33, N'Kardzhali', 1, 1, 0, 551)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (33, N'Kjustendil', 1, 1, 0, 552)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (33, N'Lovech', 1, 1, 0, 553)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (33, N'Montana', 1, 1, 0, 554)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (33, N'Oblast Sofiya-Grad', 1, 1, 0, 555)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (33, N'Pazardzhik', 1, 1, 0, 556)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (33, N'Pernik', 1, 1, 0, 557)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (33, N'Pleven', 1, 1, 0, 558)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (33, N'Plovdiv', 1, 1, 0, 559)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (33, N'Razgrad', 1, 1, 0, 560)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (33, N'Ruse', 1, 1, 0, 561)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (33, N'Shumen', 1, 1, 0, 562)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (33, N'Silistra', 1, 1, 0, 563)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (33, N'Sliven', 1, 1, 0, 564)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (33, N'Smoljan', 1, 1, 0, 565)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (33, N'Sofija grad', 1, 1, 0, 566)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (33, N'Sofijska oblast', 1, 1, 0, 567)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (33, N'Stara Zagora', 1, 1, 0, 568)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (33, N'Targovishte', 1, 1, 0, 569)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (33, N'Varna', 1, 1, 0, 570)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (33, N'Veliko Tarnovo', 1, 1, 0, 571)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (33, N'Vidin', 1, 1, 0, 572)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (33, N'Vraca', 1, 1, 0, 573)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (33, N'Yablaniza', 1, 1, 0, 574)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (34, N'Bale', 1, 1, 0, 575)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (34, N'Bam', 1, 1, 0, 576)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (34, N'Bazega', 1, 1, 0, 577)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (34, N'Bougouriba', 1, 1, 0, 578)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (34, N'Boulgou', 1, 1, 0, 579)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (34, N'Boulkiemde', 1, 1, 0, 580)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (34, N'Comoe', 1, 1, 0, 581)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (34, N'Ganzourgou', 1, 1, 0, 582)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (34, N'Gnagna', 1, 1, 0, 583)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (34, N'Gourma', 1, 1, 0, 584)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (34, N'Houet', 1, 1, 0, 585)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (34, N'Ioba', 1, 1, 0, 586)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (34, N'Kadiogo', 1, 1, 0, 587)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (34, N'Kenedougou', 1, 1, 0, 588)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (34, N'Komandjari', 1, 1, 0, 589)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (34, N'Kompienga', 1, 1, 0, 590)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (34, N'Kossi', 1, 1, 0, 591)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (34, N'Kouritenga', 1, 1, 0, 592)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (34, N'Kourweogo', 1, 1, 0, 593)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (34, N'Leraba', 1, 1, 0, 594)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (34, N'Mouhoun', 1, 1, 0, 595)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (34, N'Nahouri', 1, 1, 0, 596)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (34, N'Namentenga', 1, 1, 0, 597)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (34, N'Noumbiel', 1, 1, 0, 598)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (34, N'Oubritenga', 1, 1, 0, 599)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (34, N'Oudalan', 1, 1, 0, 600)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (34, N'Passore', 1, 1, 0, 601)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (34, N'Poni', 1, 1, 0, 602)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (34, N'Sanguie', 1, 1, 0, 603)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (34, N'Sanmatenga', 1, 1, 0, 604)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (34, N'Seno', 1, 1, 0, 605)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (34, N'Sissili', 1, 1, 0, 606)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (34, N'Soum', 1, 1, 0, 607)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (34, N'Sourou', 1, 1, 0, 608)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (34, N'Tapoa', 1, 1, 0, 609)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (34, N'Tuy', 1, 1, 0, 610)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (34, N'Yatenga', 1, 1, 0, 611)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (34, N'Zondoma', 1, 1, 0, 612)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (34, N'Zoundweogo', 1, 1, 0, 613)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (35, N'Bubanza', 1, 1, 0, 614)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (35, N'Bujumbura', 1, 1, 0, 615)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (35, N'Bururi', 1, 1, 0, 616)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (35, N'Cankuzo', 1, 1, 0, 617)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (35, N'Cibitoke', 1, 1, 0, 618)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (35, N'Gitega', 1, 1, 0, 619)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (35, N'Karuzi', 1, 1, 0, 620)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (35, N'Kayanza', 1, 1, 0, 621)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (35, N'Kirundo', 1, 1, 0, 622)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (35, N'Makamba', 1, 1, 0, 623)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (35, N'Muramvya', 1, 1, 0, 624)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (35, N'Muyinga', 1, 1, 0, 625)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (35, N'Ngozi', 1, 1, 0, 626)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (35, N'Rutana', 1, 1, 0, 627)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (35, N'Ruyigi', 1, 1, 0, 628)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (36, N'Banteay Mean Chey', 1, 1, 0, 629)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (36, N'Bat Dambang', 1, 1, 0, 630)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (36, N'Kampong Cham', 1, 1, 0, 631)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (36, N'Kampong Chhnang', 1, 1, 0, 632)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (36, N'Kampong Spoeu', 1, 1, 0, 633)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (36, N'Kampong Thum', 1, 1, 0, 634)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (36, N'Kampot', 1, 1, 0, 635)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (36, N'Kandal', 1, 1, 0, 636)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (36, N'Kaoh Kong', 1, 1, 0, 637)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (36, N'Kracheh', 1, 1, 0, 638)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (36, N'Krong Kaeb', 1, 1, 0, 639)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (36, N'Krong Pailin', 1, 1, 0, 640)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (36, N'Krong Preah Sihanouk', 1, 1, 0, 641)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (36, N'Mondol Kiri', 1, 1, 0, 642)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (36, N'Otdar Mean Chey', 1, 1, 0, 643)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (36, N'Phnum Penh', 1, 1, 0, 644)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (36, N'Pousat', 1, 1, 0, 645)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (36, N'Preah Vihear', 1, 1, 0, 646)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (36, N'Prey Veaeng', 1, 1, 0, 647)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (36, N'Rotanak Kiri', 1, 1, 0, 648)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (36, N'Siem Reab', 1, 1, 0, 649)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (36, N'Stueng Traeng', 1, 1, 0, 650)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (36, N'Svay Rieng', 1, 1, 0, 651)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (36, N'Takaev', 1, 1, 0, 652)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (37, N'Adamaoua', 1, 1, 0, 653)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (37, N'Centre', 1, 1, 0, 654)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (37, N'Est', 1, 1, 0, 655)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (37, N'Littoral', 1, 1, 0, 656)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (37, N'Nord', 1, 1, 0, 657)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (37, N'Nord Extreme', 1, 1, 0, 658)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (37, N'Nordouest', 1, 1, 0, 659)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (37, N'Ouest', 1, 1, 0, 660)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (37, N'Sud', 1, 1, 0, 661)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (37, N'Sudouest', 1, 1, 0, 662)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (38, N'Alberta', 1, 1, 0, 663)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (38, N'British Columbia', 1, 1, 0, 664)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (38, N'Manitoba', 1, 1, 0, 665)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (38, N'New Brunswick', 1, 1, 0, 666)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (38, N'Newfoundland and Labrador', 1, 1, 0, 667)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (38, N'Northwest Territories', 1, 1, 0, 668)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (38, N'Nova Scotia', 1, 1, 0, 669)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (38, N'Nunavut', 1, 1, 0, 670)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (38, N'Ontario', 1, 1, 0, 671)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (38, N'Prince Edward Island', 1, 1, 0, 672)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (38, N'Quebec', 1, 1, 0, 673)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (38, N'Saskatchewan', 1, 1, 0, 674)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (38, N'Yukon', 1, 1, 0, 675)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (39, N'Boavista', 1, 1, 0, 676)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (39, N'Brava', 1, 1, 0, 677)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (39, N'Fogo', 1, 1, 0, 678)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (39, N'Maio', 1, 1, 0, 679)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (39, N'Sal', 1, 1, 0, 680)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (39, N'Santo Antao', 1, 1, 0, 681)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (39, N'Sao Nicolau', 1, 1, 0, 682)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (39, N'Sao Tiago', 1, 1, 0, 683)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (39, N'Sao Vicente', 1, 1, 0, 684)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (40, N'Grand Cayman', 1, 1, 0, 685)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (41, N'Bamingui-Bangoran', 1, 1, 0, 686)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (41, N'Bangui', 1, 1, 0, 687)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (41, N'Basse-Kotto', 1, 1, 0, 688)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (41, N'Haut-Mbomou', 1, 1, 0, 689)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (41, N'Haute-Kotto', 1, 1, 0, 690)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (41, N'Kemo', 1, 1, 0, 691)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (41, N'Lobaye', 1, 1, 0, 692)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (41, N'Mambere-Kadei', 1, 1, 0, 693)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (41, N'Mbomou', 1, 1, 0, 694)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (41, N'Nana-Gribizi', 1, 1, 0, 695)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (41, N'Nana-Mambere', 1, 1, 0, 696)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (41, N'Ombella Mpoko', 1, 1, 0, 697)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (41, N'Ouaka', 1, 1, 0, 698)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (41, N'Ouham', 1, 1, 0, 699)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (41, N'Ouham-Pende', 1, 1, 0, 700)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (41, N'Sangha-Mbaere', 1, 1, 0, 701)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (41, N'Vakaga', 1, 1, 0, 702)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (42, N'Batha', 1, 1, 0, 703)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (42, N'Biltine', 1, 1, 0, 704)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (42, N'Bourkou-Ennedi-Tibesti', 1, 1, 0, 705)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (42, N'Chari-Baguirmi', 1, 1, 0, 706)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (42, N'Guera', 1, 1, 0, 707)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (42, N'Kanem', 1, 1, 0, 708)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (42, N'Lac', 1, 1, 0, 709)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (42, N'Logone Occidental', 1, 1, 0, 710)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (42, N'Logone Oriental', 1, 1, 0, 711)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (42, N'Mayo-Kebbi', 1, 1, 0, 712)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (42, N'Moyen-Chari', 1, 1, 0, 713)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (42, N'Ouaddai', 1, 1, 0, 714)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (42, N'Salamat', 1, 1, 0, 715)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (42, N'Tandjile', 1, 1, 0, 716)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (43, N'Aisen', 1, 1, 0, 717)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (43, N'Antofagasta', 1, 1, 0, 718)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (43, N'Araucania', 1, 1, 0, 719)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (43, N'Atacama', 1, 1, 0, 720)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (43, N'Bio Bio', 1, 1, 0, 721)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (43, N'Coquimbo', 1, 1, 0, 722)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (43, N'Libertador General Bernardo O', 1, 1, 0, 723)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (43, N'Los Lagos', 1, 1, 0, 724)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (43, N'Magellanes', 1, 1, 0, 725)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (43, N'Maule', 1, 1, 0, 726)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (43, N'Metropolitana', 1, 1, 0, 727)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (43, N'Metropolitana de Santiago', 1, 1, 0, 728)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (43, N'Tarapaca', 1, 1, 0, 729)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (43, N'Valparaiso', 1, 1, 0, 730)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (44, N'Anhui', 1, 1, 0, 731)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (44, N'Anhui Province', 1, 1, 0, 732)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (44, N'Anhui Sheng', 1, 1, 0, 733)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (44, N'Aomen', 1, 1, 0, 734)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (44, N'Beijing', 1, 1, 0, 735)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (44, N'Beijing Shi', 1, 1, 0, 736)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (44, N'Chongqing', 1, 1, 0, 737)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (44, N'Fujian', 1, 1, 0, 738)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (44, N'Fujian Sheng', 1, 1, 0, 739)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (44, N'Gansu', 1, 1, 0, 740)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (44, N'Guangdong', 1, 1, 0, 741)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (44, N'Guangdong Sheng', 1, 1, 0, 742)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (44, N'Guangxi', 1, 1, 0, 743)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (44, N'Guizhou', 1, 1, 0, 744)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (44, N'Hainan', 1, 1, 0, 745)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (44, N'Hebei', 1, 1, 0, 746)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (44, N'Heilongjiang', 1, 1, 0, 747)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (44, N'Henan', 1, 1, 0, 748)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (44, N'Hubei', 1, 1, 0, 749)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (44, N'Hunan', 1, 1, 0, 750)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (44, N'Jiangsu', 1, 1, 0, 751)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (44, N'Jiangsu Sheng', 1, 1, 0, 752)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (44, N'Jiangxi', 1, 1, 0, 753)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (44, N'Jilin', 1, 1, 0, 754)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (44, N'Liaoning', 1, 1, 0, 755)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (44, N'Liaoning Sheng', 1, 1, 0, 756)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (44, N'Nei Monggol', 1, 1, 0, 757)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (44, N'Ningxia Hui', 1, 1, 0, 758)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (44, N'Qinghai', 1, 1, 0, 759)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (44, N'Shaanxi', 1, 1, 0, 760)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (44, N'Shandong', 1, 1, 0, 761)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (44, N'Shandong Sheng', 1, 1, 0, 762)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (44, N'Shanghai', 1, 1, 0, 763)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (44, N'Shanxi', 1, 1, 0, 764)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (44, N'Sichuan', 1, 1, 0, 765)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (44, N'Tianjin', 1, 1, 0, 766)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (44, N'Xianggang', 1, 1, 0, 767)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (44, N'Xinjiang', 1, 1, 0, 768)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (44, N'Xizang', 1, 1, 0, 769)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (44, N'Yunnan', 1, 1, 0, 770)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (44, N'Zhejiang', 1, 1, 0, 771)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (44, N'Zhejiang Sheng', 1, 1, 0, 772)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (45, N'Christmas Island', 1, 1, 0, 773)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (46, N'Cocos (Keeling) Islands', 1, 1, 0, 774)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (47, N'Amazonas', 1, 1, 0, 775)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (47, N'Antioquia', 1, 1, 0, 776)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (47, N'Arauca', 1, 1, 0, 777)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (47, N'Atlantico', 1, 1, 0, 778)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (47, N'Bogota', 1, 1, 0, 779)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (47, N'Bolivar', 1, 1, 0, 780)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (47, N'Boyaca', 1, 1, 0, 781)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (47, N'Caldas', 1, 1, 0, 782)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (47, N'Caqueta', 1, 1, 0, 783)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (47, N'Casanare', 1, 1, 0, 784)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (47, N'Cauca', 1, 1, 0, 785)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (47, N'Cesar', 1, 1, 0, 786)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (47, N'Choco', 1, 1, 0, 787)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (47, N'Cordoba', 1, 1, 0, 788)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (47, N'Cundinamarca', 1, 1, 0, 789)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (47, N'Guainia', 1, 1, 0, 790)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (47, N'Guaviare', 1, 1, 0, 791)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (47, N'Huila', 1, 1, 0, 792)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (47, N'La Guajira', 1, 1, 0, 793)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (47, N'Magdalena', 1, 1, 0, 794)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (47, N'Meta', 1, 1, 0, 795)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (47, N'Narino', 1, 1, 0, 796)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (47, N'Norte de Santander', 1, 1, 0, 797)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (47, N'Putumayo', 1, 1, 0, 798)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (47, N'Quindio', 1, 1, 0, 799)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (47, N'Risaralda', 1, 1, 0, 800)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (47, N'San Andres y Providencia', 1, 1, 0, 801)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (47, N'Santander', 1, 1, 0, 802)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (47, N'Sucre', 1, 1, 0, 803)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (47, N'Tolima', 1, 1, 0, 804)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (47, N'Valle del Cauca', 1, 1, 0, 805)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (47, N'Vaupes', 1, 1, 0, 806)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (47, N'Vichada', 1, 1, 0, 807)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (48, N'Mwali', 1, 1, 0, 808)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (48, N'Njazidja', 1, 1, 0, 809)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (48, N'Nzwani', 1, 1, 0, 810)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (49, N'Bouenza', 1, 1, 0, 811)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (49, N'Brazzaville', 1, 1, 0, 812)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (49, N'Cuvette', 1, 1, 0, 813)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (49, N'Kouilou', 1, 1, 0, 814)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (49, N'Lekoumou', 1, 1, 0, 815)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (49, N'Likouala', 1, 1, 0, 816)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (49, N'Niari', 1, 1, 0, 817)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (49, N'Plateaux', 1, 1, 0, 818)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (49, N'Pool', 1, 1, 0, 819)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (49, N'Sangha', 1, 1, 0, 820)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (50, N'Bandundu', 1, 1, 0, 821)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (50, N'Bas-Congo', 1, 1, 0, 822)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (50, N'Equateur', 1, 1, 0, 823)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (50, N'Haut-Congo', 1, 1, 0, 824)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (50, N'Kasai-Occidental', 1, 1, 0, 825)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (50, N'Kasai-Oriental', 1, 1, 0, 826)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (50, N'Katanga', 1, 1, 0, 827)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (50, N'Kinshasa', 1, 1, 0, 828)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (50, N'Maniema', 1, 1, 0, 829)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (50, N'Nord-Kivu', 1, 1, 0, 830)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (50, N'Sud-Kivu', 1, 1, 0, 831)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (51, N'Aitutaki', 1, 1, 0, 832)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (51, N'Atiu', 1, 1, 0, 833)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (51, N'Mangaia', 1, 1, 0, 834)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (51, N'Manihiki', 1, 1, 0, 835)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (51, N'Mauke', 1, 1, 0, 836)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (51, N'Mitiaro', 1, 1, 0, 837)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (51, N'Nassau', 1, 1, 0, 838)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (51, N'Pukapuka', 1, 1, 0, 839)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (51, N'Rakahanga', 1, 1, 0, 840)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (51, N'Rarotonga', 1, 1, 0, 841)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (51, N'Tongareva', 1, 1, 0, 842)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (52, N'Alajuela', 1, 1, 0, 843)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (52, N'Cartago', 1, 1, 0, 844)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (52, N'Guanacaste', 1, 1, 0, 845)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (52, N'Heredia', 1, 1, 0, 846)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (52, N'Limon', 1, 1, 0, 847)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (52, N'Puntarenas', 1, 1, 0, 848)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (52, N'San Jose', 1, 1, 0, 849)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (53, N'Abidjan', 1, 1, 0, 850)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (53, N'Agneby', 1, 1, 0, 851)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (53, N'Bafing', 1, 1, 0, 852)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (53, N'Denguele', 1, 1, 0, 853)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (53, N'Dix-huit Montagnes', 1, 1, 0, 854)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (53, N'Fromager', 1, 1, 0, 855)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (53, N'Haut-Sassandra', 1, 1, 0, 856)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (53, N'Lacs', 1, 1, 0, 857)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (53, N'Lagunes', 1, 1, 0, 858)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (53, N'Marahoue', 1, 1, 0, 859)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (53, N'Moyen-Cavally', 1, 1, 0, 860)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (53, N'Moyen-Comoe', 1, 1, 0, 861)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (53, N'N''''zi-Comoe', 1, 1, 0, 862)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (53, N'Sassandra', 1, 1, 0, 863)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (53, N'Savanes', 1, 1, 0, 864)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (53, N'Sud-Bandama', 1, 1, 0, 865)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (53, N'Sud-Comoe', 1, 1, 0, 866)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (53, N'Vallee du Bandama', 1, 1, 0, 867)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (53, N'Worodougou', 1, 1, 0, 868)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (53, N'Zanzan', 1, 1, 0, 869)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (54, N'Bjelovar-Bilogora', 1, 1, 0, 870)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (54, N'Dubrovnik-Neretva', 1, 1, 0, 871)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (54, N'Grad Zagreb', 1, 1, 0, 872)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (54, N'Istra', 1, 1, 0, 873)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (54, N'Karlovac', 1, 1, 0, 874)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (54, N'Koprivnica-Krizhevci', 1, 1, 0, 875)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (54, N'Krapina-Zagorje', 1, 1, 0, 876)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (54, N'Lika-Senj', 1, 1, 0, 877)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (54, N'Medhimurje', 1, 1, 0, 878)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (54, N'Medimurska Zupanija', 1, 1, 0, 879)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (54, N'Osijek-Baranja', 1, 1, 0, 880)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (54, N'Osjecko-Baranjska Zupanija', 1, 1, 0, 881)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (54, N'Pozhega-Slavonija', 1, 1, 0, 882)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (54, N'Primorje-Gorski Kotar', 1, 1, 0, 883)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (54, N'Shibenik-Knin', 1, 1, 0, 884)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (54, N'Sisak-Moslavina', 1, 1, 0, 885)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (54, N'Slavonski Brod-Posavina', 1, 1, 0, 886)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (54, N'Split-Dalmacija', 1, 1, 0, 887)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (54, N'Varazhdin', 1, 1, 0, 888)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (54, N'Virovitica-Podravina', 1, 1, 0, 889)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (54, N'Vukovar-Srijem', 1, 1, 0, 890)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (54, N'Zadar', 1, 1, 0, 891)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (54, N'Zagreb', 1, 1, 0, 892)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (55, N'Camaguey', 1, 1, 0, 893)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (55, N'Ciego de Avila', 1, 1, 0, 894)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (55, N'Cienfuegos', 1, 1, 0, 895)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (55, N'Ciudad de la Habana', 1, 1, 0, 896)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (55, N'Granma', 1, 1, 0, 897)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (55, N'Guantanamo', 1, 1, 0, 898)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (55, N'Habana', 1, 1, 0, 899)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (55, N'Holguin', 1, 1, 0, 900)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (55, N'Isla de la Juventud', 1, 1, 0, 901)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (55, N'La Habana', 1, 1, 0, 902)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (55, N'Las Tunas', 1, 1, 0, 903)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (55, N'Matanzas', 1, 1, 0, 904)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (55, N'Pinar del Rio', 1, 1, 0, 905)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (55, N'Sancti Spiritus', 1, 1, 0, 906)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (55, N'Santiago de Cuba', 1, 1, 0, 907)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (55, N'Villa Clara', 1, 1, 0, 908)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (56, N'Government controlled area', 1, 1, 0, 909)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (56, N'Limassol', 1, 1, 0, 910)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (56, N'Nicosia District', 1, 1, 0, 911)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (56, N'Paphos', 1, 1, 0, 912)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (56, N'Turkish controlled area', 1, 1, 0, 913)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (57, N'Central Bohemian', 1, 1, 0, 914)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (57, N'Frycovice', 1, 1, 0, 915)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (57, N'Jihocesky Kraj', 1, 1, 0, 916)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (57, N'Jihochesky', 1, 1, 0, 917)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (57, N'Jihomoravsky', 1, 1, 0, 918)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (57, N'Karlovarsky', 1, 1, 0, 919)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (57, N'Klecany', 1, 1, 0, 920)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (57, N'Kralovehradecky', 1, 1, 0, 921)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (57, N'Liberecky', 1, 1, 0, 922)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (57, N'Lipov', 1, 1, 0, 923)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (57, N'Moravskoslezsky', 1, 1, 0, 924)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (57, N'Olomoucky', 1, 1, 0, 925)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (57, N'Olomoucky Kraj', 1, 1, 0, 926)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (57, N'Pardubicky', 1, 1, 0, 927)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (57, N'Plzensky', 1, 1, 0, 928)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (57, N'Praha', 1, 1, 0, 929)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (57, N'Rajhrad', 1, 1, 0, 930)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (57, N'Smirice', 1, 1, 0, 931)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (57, N'South Moravian', 1, 1, 0, 932)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (57, N'Straz nad Nisou', 1, 1, 0, 933)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (57, N'Stredochesky', 1, 1, 0, 934)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (57, N'Unicov', 1, 1, 0, 935)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (57, N'Ustecky', 1, 1, 0, 936)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (57, N'Valletta', 1, 1, 0, 937)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (57, N'Velesin', 1, 1, 0, 938)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (57, N'Vysochina', 1, 1, 0, 939)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (57, N'Zlinsky', 1, 1, 0, 940)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (58, N'Arhus', 1, 1, 0, 941)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (58, N'Bornholm', 1, 1, 0, 942)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (58, N'Frederiksborg', 1, 1, 0, 943)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (58, N'Fyn', 1, 1, 0, 944)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (58, N'Hovedstaden', 1, 1, 0, 945)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (58, N'Kobenhavn', 1, 1, 0, 946)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (58, N'Kobenhavns Amt', 1, 1, 0, 947)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (58, N'Kobenhavns Kommune', 1, 1, 0, 948)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (58, N'Nordjylland', 1, 1, 0, 949)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (58, N'Ribe', 1, 1, 0, 950)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (58, N'Ringkobing', 1, 1, 0, 951)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (58, N'Roervig', 1, 1, 0, 952)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (58, N'Roskilde', 1, 1, 0, 953)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (58, N'Roslev', 1, 1, 0, 954)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (58, N'Sjaelland', 1, 1, 0, 955)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (58, N'Soeborg', 1, 1, 0, 956)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (58, N'Sonderjylland', 1, 1, 0, 957)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (58, N'Storstrom', 1, 1, 0, 958)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (58, N'Syddanmark', 1, 1, 0, 959)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (58, N'Toelloese', 1, 1, 0, 960)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (58, N'Vejle', 1, 1, 0, 961)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (58, N'Vestsjalland', 1, 1, 0, 962)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (58, N'Viborg', 1, 1, 0, 963)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (59, N'Ali Sabih', 1, 1, 0, 964)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (59, N'Dikhil', 1, 1, 0, 965)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (59, N'Jibuti', 1, 1, 0, 966)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (59, N'Tajurah', 1, 1, 0, 967)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (59, N'Ubuk', 1, 1, 0, 968)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (60, N'Saint Andrew', 1, 1, 0, 969)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (60, N'Saint David', 1, 1, 0, 970)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (60, N'Saint George', 1, 1, 0, 971)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (60, N'Saint John', 1, 1, 0, 972)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (60, N'Saint Joseph', 1, 1, 0, 973)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (60, N'Saint Luke', 1, 1, 0, 974)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (60, N'Saint Mark', 1, 1, 0, 975)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (60, N'Saint Patrick', 1, 1, 0, 976)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (60, N'Saint Paul', 1, 1, 0, 977)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (60, N'Saint Peter', 1, 1, 0, 978)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (61, N'Azua', 1, 1, 0, 979)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (61, N'Bahoruco', 1, 1, 0, 980)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (61, N'Barahona', 1, 1, 0, 981)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (61, N'Dajabon', 1, 1, 0, 982)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (61, N'Distrito Nacional', 1, 1, 0, 983)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (61, N'Duarte', 1, 1, 0, 984)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (61, N'El Seybo', 1, 1, 0, 985)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (61, N'Elias Pina', 1, 1, 0, 986)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (61, N'Espaillat', 1, 1, 0, 987)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (61, N'Hato Mayor', 1, 1, 0, 988)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (61, N'Independencia', 1, 1, 0, 989)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (61, N'La Altagracia', 1, 1, 0, 990)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (61, N'La Romana', 1, 1, 0, 991)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (61, N'La Vega', 1, 1, 0, 992)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (61, N'Maria Trinidad Sanchez', 1, 1, 0, 993)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (61, N'Monsenor Nouel', 1, 1, 0, 994)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (61, N'Monte Cristi', 1, 1, 0, 995)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (61, N'Monte Plata', 1, 1, 0, 996)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (61, N'Pedernales', 1, 1, 0, 997)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (61, N'Peravia', 1, 1, 0, 998)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (61, N'Puerto Plata', 1, 1, 0, 999)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (61, N'Salcedo', 1, 1, 0, 1000)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (61, N'Samana', 1, 1, 0, 1001)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (61, N'San Cristobal', 1, 1, 0, 1002)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (61, N'San Juan', 1, 1, 0, 1003)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (61, N'San Pedro de Macoris', 1, 1, 0, 1004)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (61, N'Sanchez Ramirez', 1, 1, 0, 1005)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (61, N'Santiago', 1, 1, 0, 1006)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (61, N'Santiago Rodriguez', 1, 1, 0, 1007)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (61, N'Valverde', 1, 1, 0, 1008)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (62, N'Aileu', 1, 1, 0, 1009)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (62, N'Ainaro', 1, 1, 0, 1010)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (62, N'Ambeno', 1, 1, 0, 1011)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (62, N'Baucau', 1, 1, 0, 1012)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (62, N'Bobonaro', 1, 1, 0, 1013)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (62, N'Cova Lima', 1, 1, 0, 1014)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (62, N'Dili', 1, 1, 0, 1015)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (62, N'Ermera', 1, 1, 0, 1016)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (62, N'Lautem', 1, 1, 0, 1017)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (62, N'Liquica', 1, 1, 0, 1018)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (62, N'Manatuto', 1, 1, 0, 1019)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (62, N'Manufahi', 1, 1, 0, 1020)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (62, N'Viqueque', 1, 1, 0, 1021)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (63, N'Azuay', 1, 1, 0, 1022)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (63, N'Bolivar', 1, 1, 0, 1023)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (63, N'Canar', 1, 1, 0, 1024)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (63, N'Carchi', 1, 1, 0, 1025)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (63, N'Chimborazo', 1, 1, 0, 1026)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (63, N'Cotopaxi', 1, 1, 0, 1027)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (63, N'El Oro', 1, 1, 0, 1028)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (63, N'Esmeraldas', 1, 1, 0, 1029)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (63, N'Galapagos', 1, 1, 0, 1030)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (63, N'Guayas', 1, 1, 0, 1031)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (63, N'Imbabura', 1, 1, 0, 1032)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (63, N'Loja', 1, 1, 0, 1033)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (63, N'Los Rios', 1, 1, 0, 1034)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (63, N'Manabi', 1, 1, 0, 1035)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (63, N'Morona Santiago', 1, 1, 0, 1036)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (63, N'Napo', 1, 1, 0, 1037)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (63, N'Orellana', 1, 1, 0, 1038)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (63, N'Pastaza', 1, 1, 0, 1039)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (63, N'Pichincha', 1, 1, 0, 1040)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (63, N'Sucumbios', 1, 1, 0, 1041)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (63, N'Tungurahua', 1, 1, 0, 1042)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (63, N'Zamora Chinchipe', 1, 1, 0, 1043)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (64, N'Aswan', 1, 1, 0, 1044)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (64, N'Asyut', 1, 1, 0, 1045)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (64, N'Bani Suwayf', 1, 1, 0, 1046)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (64, N'Bur Sa''''id', 1, 1, 0, 1047)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (64, N'Cairo', 1, 1, 0, 1048)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (64, N'Dumyat', 1, 1, 0, 1049)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (64, N'Kafr-ash-Shaykh', 1, 1, 0, 1050)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (64, N'Matruh', 1, 1, 0, 1051)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (64, N'Muhafazat ad Daqahliyah', 1, 1, 0, 1052)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (64, N'Muhafazat al Fayyum', 1, 1, 0, 1053)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (64, N'Muhafazat al Gharbiyah', 1, 1, 0, 1054)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (64, N'Muhafazat al Iskandariyah', 1, 1, 0, 1055)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (64, N'Muhafazat al Qahirah', 1, 1, 0, 1056)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (64, N'Qina', 1, 1, 0, 1057)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (64, N'Sawhaj', 1, 1, 0, 1058)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (64, N'Sina al-Janubiyah', 1, 1, 0, 1059)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (64, N'Sina ash-Shamaliyah', 1, 1, 0, 1060)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (64, N'ad-Daqahliyah', 1, 1, 0, 1061)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (64, N'al-Bahr-al-Ahmar', 1, 1, 0, 1062)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (64, N'al-Buhayrah', 1, 1, 0, 1063)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (64, N'al-Fayyum', 1, 1, 0, 1064)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (64, N'al-Gharbiyah', 1, 1, 0, 1065)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (64, N'al-Iskandariyah', 1, 1, 0, 1066)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (64, N'al-Ismailiyah', 1, 1, 0, 1067)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (64, N'al-Jizah', 1, 1, 0, 1068)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (64, N'al-Minufiyah', 1, 1, 0, 1069)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (64, N'al-Minya', 1, 1, 0, 1070)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (64, N'al-Qahira', 1, 1, 0, 1071)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (64, N'al-Qalyubiyah', 1, 1, 0, 1072)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (64, N'al-Uqsur', 1, 1, 0, 1073)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (64, N'al-Wadi al-Jadid', 1, 1, 0, 1074)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (64, N'as-Suways', 1, 1, 0, 1075)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (64, N'ash-Sharqiyah', 1, 1, 0, 1076)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (65, N'Ahuachapan', 1, 1, 0, 1077)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (65, N'Cabanas', 1, 1, 0, 1078)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (65, N'Chalatenango', 1, 1, 0, 1079)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (65, N'Cuscatlan', 1, 1, 0, 1080)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (65, N'La Libertad', 1, 1, 0, 1081)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (65, N'La Paz', 1, 1, 0, 1082)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (65, N'La Union', 1, 1, 0, 1083)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (65, N'Morazan', 1, 1, 0, 1084)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (65, N'San Miguel', 1, 1, 0, 1085)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (65, N'San Salvador', 1, 1, 0, 1086)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (65, N'San Vicente', 1, 1, 0, 1087)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (65, N'Santa Ana', 1, 1, 0, 1088)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (65, N'Sonsonate', 1, 1, 0, 1089)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (65, N'Usulutan', 1, 1, 0, 1090)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (66, N'Annobon', 1, 1, 0, 1091)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (66, N'Bioko Norte', 1, 1, 0, 1092)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (66, N'Bioko Sur', 1, 1, 0, 1093)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (66, N'Centro Sur', 1, 1, 0, 1094)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (66, N'Kie-Ntem', 1, 1, 0, 1095)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (66, N'Litoral', 1, 1, 0, 1096)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (66, N'Wele-Nzas', 1, 1, 0, 1097)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (67, N'Anseba', 1, 1, 0, 1098)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (67, N'Debub', 1, 1, 0, 1099)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (67, N'Debub-Keih-Bahri', 1, 1, 0, 1100)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (67, N'Gash-Barka', 1, 1, 0, 1101)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (67, N'Maekel', 1, 1, 0, 1102)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (67, N'Semien-Keih-Bahri', 1, 1, 0, 1103)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (68, N'Harju', 1, 1, 0, 1104)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (68, N'Hiiu', 1, 1, 0, 1105)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (68, N'Ida-Viru', 1, 1, 0, 1106)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (68, N'Jarva', 1, 1, 0, 1107)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (68, N'Jogeva', 1, 1, 0, 1108)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (68, N'Laane', 1, 1, 0, 1109)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (68, N'Laane-Viru', 1, 1, 0, 1110)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (68, N'Parnu', 1, 1, 0, 1111)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (68, N'Polva', 1, 1, 0, 1112)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (68, N'Rapla', 1, 1, 0, 1113)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (68, N'Saare', 1, 1, 0, 1114)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (68, N'Tartu', 1, 1, 0, 1115)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (68, N'Valga', 1, 1, 0, 1116)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (68, N'Viljandi', 1, 1, 0, 1117)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (68, N'Voru', 1, 1, 0, 1118)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (69, N'Addis Abeba', 1, 1, 0, 1119)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (69, N'Afar', 1, 1, 0, 1120)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (69, N'Amhara', 1, 1, 0, 1121)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (69, N'Benishangul', 1, 1, 0, 1122)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (69, N'Diredawa', 1, 1, 0, 1123)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (69, N'Gambella', 1, 1, 0, 1124)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (69, N'Harar', 1, 1, 0, 1125)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (69, N'Jigjiga', 1, 1, 0, 1126)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (69, N'Mekele', 1, 1, 0, 1127)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (69, N'Oromia', 1, 1, 0, 1128)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (69, N'Somali', 1, 1, 0, 1129)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (69, N'Southern', 1, 1, 0, 1130)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (69, N'Tigray', 1, 1, 0, 1131)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (70, N'Christmas Island', 1, 1, 0, 1132)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (70, N'Cocos Islands', 1, 1, 0, 1133)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (70, N'Coral Sea Islands', 1, 1, 0, 1134)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (71, N'Falkland Islands', 1, 1, 0, 1135)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (71, N'South Georgia', 1, 1, 0, 1136)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (72, N'Klaksvik', 1, 1, 0, 1137)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (72, N'Nor ara Eysturoy', 1, 1, 0, 1138)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (72, N'Nor oy', 1, 1, 0, 1139)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (72, N'Sandoy', 1, 1, 0, 1140)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (72, N'Streymoy', 1, 1, 0, 1141)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (72, N'Su uroy', 1, 1, 0, 1142)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (72, N'Sy ra Eysturoy', 1, 1, 0, 1143)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (72, N'Torshavn', 1, 1, 0, 1144)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (72, N'Vaga', 1, 1, 0, 1145)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (73, N'Central', 1, 1, 0, 1146)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (73, N'Eastern', 1, 1, 0, 1147)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (73, N'Northern', 1, 1, 0, 1148)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (73, N'South Pacific', 1, 1, 0, 1149)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (73, N'Western', 1, 1, 0, 1150)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (74, N'Ahvenanmaa', 1, 1, 0, 1151)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (74, N'Etela-Karjala', 1, 1, 0, 1152)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (74, N'Etela-Pohjanmaa', 1, 1, 0, 1153)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (74, N'Etela-Savo', 1, 1, 0, 1154)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (74, N'Etela-Suomen Laani', 1, 1, 0, 1155)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (74, N'Ita-Suomen Laani', 1, 1, 0, 1156)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (74, N'Ita-Uusimaa', 1, 1, 0, 1157)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (74, N'Kainuu', 1, 1, 0, 1158)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (74, N'Kanta-Hame', 1, 1, 0, 1159)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (74, N'Keski-Pohjanmaa', 1, 1, 0, 1160)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (74, N'Keski-Suomi', 1, 1, 0, 1161)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (74, N'Kymenlaakso', 1, 1, 0, 1162)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (74, N'Lansi-Suomen Laani', 1, 1, 0, 1163)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (74, N'Lappi', 1, 1, 0, 1164)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (74, N'Northern Savonia', 1, 1, 0, 1165)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (74, N'Ostrobothnia', 1, 1, 0, 1166)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (74, N'Oulun Laani', 1, 1, 0, 1167)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (74, N'Paijat-Hame', 1, 1, 0, 1168)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (74, N'Pirkanmaa', 1, 1, 0, 1169)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (74, N'Pohjanmaa', 1, 1, 0, 1170)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (74, N'Pohjois-Karjala', 1, 1, 0, 1171)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (74, N'Pohjois-Pohjanmaa', 1, 1, 0, 1172)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (74, N'Pohjois-Savo', 1, 1, 0, 1173)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (74, N'Saarijarvi', 1, 1, 0, 1174)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (74, N'Satakunta', 1, 1, 0, 1175)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (74, N'Southern Savonia', 1, 1, 0, 1176)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (74, N'Tavastia Proper', 1, 1, 0, 1177)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (74, N'Uleaborgs Lan', 1, 1, 0, 1178)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (74, N'Uusimaa', 1, 1, 0, 1179)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (74, N'Varsinais-Suomi', 1, 1, 0, 1180)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Ain', 1, 1, 0, 1181)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Aisne', 1, 1, 0, 1182)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Albi Le Sequestre', 1, 1, 0, 1183)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Allier', 1, 1, 0, 1184)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Alpes-Cote dAzur', 1, 1, 0, 1185)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Alpes-Maritimes', 1, 1, 0, 1186)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Alpes-de-Haute-Provence', 1, 1, 0, 1187)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Alsace', 1, 1, 0, 1188)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Aquitaine', 1, 1, 0, 1189)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Ardeche', 1, 1, 0, 1190)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Ardennes', 1, 1, 0, 1191)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Ariege', 1, 1, 0, 1192)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Aube', 1, 1, 0, 1193)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Aude', 1, 1, 0, 1194)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Auvergne', 1, 1, 0, 1195)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Aveyron', 1, 1, 0, 1196)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Bas-Rhin', 1, 1, 0, 1197)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Basse-Normandie', 1, 1, 0, 1198)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Bouches-du-Rhone', 1, 1, 0, 1199)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Bourgogne', 1, 1, 0, 1200)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Bretagne', 1, 1, 0, 1201)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Brittany', 1, 1, 0, 1202)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Burgundy', 1, 1, 0, 1203)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Calvados', 1, 1, 0, 1204)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Cantal', 1, 1, 0, 1205)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Cedex', 1, 1, 0, 1206)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Centre', 1, 1, 0, 1207)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Charente', 1, 1, 0, 1208)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Charente-Maritime', 1, 1, 0, 1209)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Cher', 1, 1, 0, 1210)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Correze', 1, 1, 0, 1211)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Corse-du-Sud', 1, 1, 0, 1212)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Cote-d''''Or', 1, 1, 0, 1213)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Cotes-d''''Armor', 1, 1, 0, 1214)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Creuse', 1, 1, 0, 1215)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Crolles', 1, 1, 0, 1216)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Deux-Sevres', 1, 1, 0, 1217)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Dordogne', 1, 1, 0, 1218)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Doubs', 1, 1, 0, 1219)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Drome', 1, 1, 0, 1220)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Essonne', 1, 1, 0, 1221)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Eure', 1, 1, 0, 1222)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Eure-et-Loir', 1, 1, 0, 1223)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Feucherolles', 1, 1, 0, 1224)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Finistere', 1, 1, 0, 1225)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Franche-Comte', 1, 1, 0, 1226)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Gard', 1, 1, 0, 1227)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Gers', 1, 1, 0, 1228)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Gironde', 1, 1, 0, 1229)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Haut-Rhin', 1, 1, 0, 1230)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Haute-Corse', 1, 1, 0, 1231)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Haute-Garonne', 1, 1, 0, 1232)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Haute-Loire', 1, 1, 0, 1233)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Haute-Marne', 1, 1, 0, 1234)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Haute-Saone', 1, 1, 0, 1235)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Haute-Savoie', 1, 1, 0, 1236)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Haute-Vienne', 1, 1, 0, 1237)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Hautes-Alpes', 1, 1, 0, 1238)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Hautes-Pyrenees', 1, 1, 0, 1239)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Hauts-de-Seine', 1, 1, 0, 1240)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Herault', 1, 1, 0, 1241)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Ile-de-France', 1, 1, 0, 1242)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Ille-et-Vilaine', 1, 1, 0, 1243)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Indre', 1, 1, 0, 1244)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Indre-et-Loire', 1, 1, 0, 1245)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Isere', 1, 1, 0, 1246)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Jura', 1, 1, 0, 1247)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Klagenfurt', 1, 1, 0, 1248)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Landes', 1, 1, 0, 1249)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Languedoc-Roussillon', 1, 1, 0, 1250)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Larcay', 1, 1, 0, 1251)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Le Castellet', 1, 1, 0, 1252)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Le Creusot', 1, 1, 0, 1253)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Limousin', 1, 1, 0, 1254)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Loir-et-Cher', 1, 1, 0, 1255)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Loire', 1, 1, 0, 1256)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Loire-Atlantique', 1, 1, 0, 1257)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Loiret', 1, 1, 0, 1258)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Lorraine', 1, 1, 0, 1259)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Lot', 1, 1, 0, 1260)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Lot-et-Garonne', 1, 1, 0, 1261)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Lower Normandy', 1, 1, 0, 1262)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Lozere', 1, 1, 0, 1263)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Maine-et-Loire', 1, 1, 0, 1264)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Manche', 1, 1, 0, 1265)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Marne', 1, 1, 0, 1266)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Mayenne', 1, 1, 0, 1267)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Meurthe-et-Moselle', 1, 1, 0, 1268)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Meuse', 1, 1, 0, 1269)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Midi-Pyrenees', 1, 1, 0, 1270)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Morbihan', 1, 1, 0, 1271)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Moselle', 1, 1, 0, 1272)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Nievre', 1, 1, 0, 1273)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Nord', 1, 1, 0, 1274)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Nord-Pas-de-Calais', 1, 1, 0, 1275)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Oise', 1, 1, 0, 1276)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Orne', 1, 1, 0, 1277)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Paris', 1, 1, 0, 1278)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Pas-de-Calais', 1, 1, 0, 1279)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Pays de la Loire', 1, 1, 0, 1280)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Pays-de-la-Loire', 1, 1, 0, 1281)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Picardy', 1, 1, 0, 1282)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Puy-de-Dome', 1, 1, 0, 1283)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Pyrenees-Atlantiques', 1, 1, 0, 1284)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Pyrenees-Orientales', 1, 1, 0, 1285)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Quelmes', 1, 1, 0, 1286)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Rhone', 1, 1, 0, 1287)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Rhone-Alpes', 1, 1, 0, 1288)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Saint Ouen', 1, 1, 0, 1289)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Saint Viatre', 1, 1, 0, 1290)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Saone-et-Loire', 1, 1, 0, 1291)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Sarthe', 1, 1, 0, 1292)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Savoie', 1, 1, 0, 1293)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Seine-Maritime', 1, 1, 0, 1294)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Seine-Saint-Denis', 1, 1, 0, 1295)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Seine-et-Marne', 1, 1, 0, 1296)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Somme', 1, 1, 0, 1297)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Sophia Antipolis', 1, 1, 0, 1298)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Souvans', 1, 1, 0, 1299)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Tarn', 1, 1, 0, 1300)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Tarn-et-Garonne', 1, 1, 0, 1301)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Territoire de Belfort', 1, 1, 0, 1302)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Treignac', 1, 1, 0, 1303)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Upper Normandy', 1, 1, 0, 1304)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Val-d''''Oise', 1, 1, 0, 1305)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Val-de-Marne', 1, 1, 0, 1306)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Var', 1, 1, 0, 1307)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Vaucluse', 1, 1, 0, 1308)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Vellise', 1, 1, 0, 1309)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Vendee', 1, 1, 0, 1310)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Vienne', 1, 1, 0, 1311)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Vosges', 1, 1, 0, 1312)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Yonne', 1, 1, 0, 1313)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (75, N'Yvelines', 1, 1, 0, 1314)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (76, N'Cayenne', 1, 1, 0, 1315)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (76, N'Saint-Laurent-du-Maroni', 1, 1, 0, 1316)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (77, N'Iles du Vent', 1, 1, 0, 1317)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (77, N'Iles sous le Vent', 1, 1, 0, 1318)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (77, N'Marquesas', 1, 1, 0, 1319)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (77, N'Tuamotu', 1, 1, 0, 1320)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (77, N'Tubuai', 1, 1, 0, 1321)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (78, N'Amsterdam', 1, 1, 0, 1322)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (78, N'Crozet Islands', 1, 1, 0, 1323)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (78, N'Kerguelen', 1, 1, 0, 1324)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (79, N'Estuaire', 1, 1, 0, 1325)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (79, N'Haut-Ogooue', 1, 1, 0, 1326)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (79, N'Moyen-Ogooue', 1, 1, 0, 1327)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (79, N'Ngounie', 1, 1, 0, 1328)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (79, N'Nyanga', 1, 1, 0, 1329)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (79, N'Ogooue-Ivindo', 1, 1, 0, 1330)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (79, N'Ogooue-Lolo', 1, 1, 0, 1331)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (79, N'Ogooue-Maritime', 1, 1, 0, 1332)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (79, N'Woleu-Ntem', 1, 1, 0, 1333)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (80, N'Banjul', 1, 1, 0, 1334)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (80, N'Basse', 1, 1, 0, 1335)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (80, N'Brikama', 1, 1, 0, 1336)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (80, N'Janjanbureh', 1, 1, 0, 1337)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (80, N'Kanifing', 1, 1, 0, 1338)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (80, N'Kerewan', 1, 1, 0, 1339)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (80, N'Kuntaur', 1, 1, 0, 1340)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (80, N'Mansakonko', 1, 1, 0, 1341)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (81, N'Abhasia', 1, 1, 0, 1342)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (81, N'Ajaria', 1, 1, 0, 1343)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (81, N'Guria', 1, 1, 0, 1344)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (81, N'Imereti', 1, 1, 0, 1345)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (81, N'Kaheti', 1, 1, 0, 1346)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (81, N'Kvemo Kartli', 1, 1, 0, 1347)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (81, N'Mcheta-Mtianeti', 1, 1, 0, 1348)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (81, N'Racha', 1, 1, 0, 1349)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (81, N'Samagrelo-Zemo Svaneti', 1, 1, 0, 1350)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (81, N'Samche-Zhavaheti', 1, 1, 0, 1351)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (81, N'Shida Kartli', 1, 1, 0, 1352)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (81, N'Tbilisi', 1, 1, 0, 1353)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (82, N'Auvergne', 1, 1, 0, 1354)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (82, N'Baden-Wurttemberg', 1, 1, 0, 1355)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (82, N'Bavaria', 1, 1, 0, 1356)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (82, N'Bayern', 1, 1, 0, 1357)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (82, N'Beilstein Wurtt', 1, 1, 0, 1358)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (82, N'Berlin', 1, 1, 0, 1359)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (82, N'Brandenburg', 1, 1, 0, 1360)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (82, N'Bremen', 1, 1, 0, 1361)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (82, N'Dreisbach', 1, 1, 0, 1362)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (82, N'Freistaat Bayern', 1, 1, 0, 1363)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (82, N'Hamburg', 1, 1, 0, 1364)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (82, N'Hannover', 1, 1, 0, 1365)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (82, N'Heroldstatt', 1, 1, 0, 1366)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (82, N'Hessen', 1, 1, 0, 1367)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (82, N'Kortenberg', 1, 1, 0, 1368)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (82, N'Laasdorf', 1, 1, 0, 1369)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (82, N'Land Baden-Wurttemberg', 1, 1, 0, 1370)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (82, N'Land Bayern', 1, 1, 0, 1371)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (82, N'Land Brandenburg', 1, 1, 0, 1372)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (82, N'Land Hessen', 1, 1, 0, 1373)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (82, N'Land Mecklenburg-Vorpommern', 1, 1, 0, 1374)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (82, N'Land Nordrhein-Westfalen', 1, 1, 0, 1375)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (82, N'Land Rheinland-Pfalz', 1, 1, 0, 1376)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (82, N'Land Sachsen', 1, 1, 0, 1377)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (82, N'Land Sachsen-Anhalt', 1, 1, 0, 1378)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (82, N'Land Thuringen', 1, 1, 0, 1379)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (82, N'Lower Saxony', 1, 1, 0, 1380)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (82, N'Mecklenburg-Vorpommern', 1, 1, 0, 1381)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (82, N'Mulfingen', 1, 1, 0, 1382)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (82, N'Munich', 1, 1, 0, 1383)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (82, N'Neubeuern', 1, 1, 0, 1384)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (82, N'Niedersachsen', 1, 1, 0, 1385)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (82, N'Noord-Holland', 1, 1, 0, 1386)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (82, N'Nordrhein-Westfalen', 1, 1, 0, 1387)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (82, N'North Rhine-Westphalia', 1, 1, 0, 1388)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (82, N'Osterode', 1, 1, 0, 1389)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (82, N'Rheinland-Pfalz', 1, 1, 0, 1390)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (82, N'Rhineland-Palatinate', 1, 1, 0, 1391)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (82, N'Saarland', 1, 1, 0, 1392)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (82, N'Sachsen', 1, 1, 0, 1393)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (82, N'Sachsen-Anhalt', 1, 1, 0, 1394)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (82, N'Saxony', 1, 1, 0, 1395)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (82, N'Schleswig-Holstein', 1, 1, 0, 1396)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (82, N'Thuringia', 1, 1, 0, 1397)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (82, N'Webling', 1, 1, 0, 1398)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (82, N'Weinstrabe', 1, 1, 0, 1399)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (82, N'schlobborn', 1, 1, 0, 1400)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (83, N'Ashanti', 1, 1, 0, 1401)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (83, N'Brong-Ahafo', 1, 1, 0, 1402)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (83, N'Central', 1, 1, 0, 1403)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (83, N'Eastern', 1, 1, 0, 1404)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (83, N'Greater Accra', 1, 1, 0, 1405)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (83, N'Northern', 1, 1, 0, 1406)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (83, N'Upper East', 1, 1, 0, 1407)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (83, N'Upper West', 1, 1, 0, 1408)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (83, N'Volta', 1, 1, 0, 1409)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (83, N'Western', 1, 1, 0, 1410)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (84, N'Gibraltar', 1, 1, 0, 1411)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (85, N'Acharnes', 1, 1, 0, 1412)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (85, N'Ahaia', 1, 1, 0, 1413)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (85, N'Aitolia kai Akarnania', 1, 1, 0, 1414)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (85, N'Argolis', 1, 1, 0, 1415)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (85, N'Arkadia', 1, 1, 0, 1416)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (85, N'Arta', 1, 1, 0, 1417)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (85, N'Attica', 1, 1, 0, 1418)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (85, N'Attiki', 1, 1, 0, 1419)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (85, N'Ayion Oros', 1, 1, 0, 1420)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (85, N'Crete', 1, 1, 0, 1421)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (85, N'Dodekanisos', 1, 1, 0, 1422)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (85, N'Drama', 1, 1, 0, 1423)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (85, N'Evia', 1, 1, 0, 1424)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (85, N'Evritania', 1, 1, 0, 1425)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (85, N'Evros', 1, 1, 0, 1426)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (85, N'Evvoia', 1, 1, 0, 1427)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (85, N'Florina', 1, 1, 0, 1428)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (85, N'Fokis', 1, 1, 0, 1429)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (85, N'Fthiotis', 1, 1, 0, 1430)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (85, N'Grevena', 1, 1, 0, 1431)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (85, N'Halandri', 1, 1, 0, 1432)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (85, N'Halkidiki', 1, 1, 0, 1433)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (85, N'Hania', 1, 1, 0, 1434)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (85, N'Heraklion', 1, 1, 0, 1435)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (85, N'Hios', 1, 1, 0, 1436)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (85, N'Ilia', 1, 1, 0, 1437)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (85, N'Imathia', 1, 1, 0, 1438)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (85, N'Ioannina', 1, 1, 0, 1439)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (85, N'Iraklion', 1, 1, 0, 1440)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (85, N'Karditsa', 1, 1, 0, 1441)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (85, N'Kastoria', 1, 1, 0, 1442)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (85, N'Kavala', 1, 1, 0, 1443)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (85, N'Kefallinia', 1, 1, 0, 1444)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (85, N'Kerkira', 1, 1, 0, 1445)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (85, N'Kiklades', 1, 1, 0, 1446)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (85, N'Kilkis', 1, 1, 0, 1447)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (85, N'Korinthia', 1, 1, 0, 1448)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (85, N'Kozani', 1, 1, 0, 1449)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (85, N'Lakonia', 1, 1, 0, 1450)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (85, N'Larisa', 1, 1, 0, 1451)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (85, N'Lasithi', 1, 1, 0, 1452)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (85, N'Lesvos', 1, 1, 0, 1453)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (85, N'Levkas', 1, 1, 0, 1454)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (85, N'Magnisia', 1, 1, 0, 1455)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (85, N'Messinia', 1, 1, 0, 1456)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (85, N'Nomos Attikis', 1, 1, 0, 1457)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (85, N'Nomos Zakynthou', 1, 1, 0, 1458)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (85, N'Pella', 1, 1, 0, 1459)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (85, N'Pieria', 1, 1, 0, 1460)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (85, N'Piraios', 1, 1, 0, 1461)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (85, N'Preveza', 1, 1, 0, 1462)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (85, N'Rethimni', 1, 1, 0, 1463)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (85, N'Rodopi', 1, 1, 0, 1464)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (85, N'Samos', 1, 1, 0, 1465)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (85, N'Serrai', 1, 1, 0, 1466)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (85, N'Thesprotia', 1, 1, 0, 1467)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (85, N'Thessaloniki', 1, 1, 0, 1468)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (85, N'Trikala', 1, 1, 0, 1469)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (85, N'Voiotia', 1, 1, 0, 1470)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (85, N'West Greece', 1, 1, 0, 1471)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (85, N'Xanthi', 1, 1, 0, 1472)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (85, N'Zakinthos', 1, 1, 0, 1473)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (86, N'Aasiaat', 1, 1, 0, 1474)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (86, N'Ammassalik', 1, 1, 0, 1475)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (86, N'Illoqqortoormiut', 1, 1, 0, 1476)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (86, N'Ilulissat', 1, 1, 0, 1477)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (86, N'Ivittuut', 1, 1, 0, 1478)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (86, N'Kangaatsiaq', 1, 1, 0, 1479)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (86, N'Maniitsoq', 1, 1, 0, 1480)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (86, N'Nanortalik', 1, 1, 0, 1481)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (86, N'Narsaq', 1, 1, 0, 1482)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (86, N'Nuuk', 1, 1, 0, 1483)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (86, N'Paamiut', 1, 1, 0, 1484)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (86, N'Qaanaaq', 1, 1, 0, 1485)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (86, N'Qaqortoq', 1, 1, 0, 1486)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (86, N'Qasigiannguit', 1, 1, 0, 1487)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (86, N'Qeqertarsuaq', 1, 1, 0, 1488)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (86, N'Sisimiut', 1, 1, 0, 1489)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (86, N'Udenfor kommunal inddeling', 1, 1, 0, 1490)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (86, N'Upernavik', 1, 1, 0, 1491)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (86, N'Uummannaq', 1, 1, 0, 1492)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (87, N'Carriacou-Petite Martinique', 1, 1, 0, 1493)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (87, N'Saint Andrew', 1, 1, 0, 1494)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (87, N'Saint Davids', 1, 1, 0, 1495)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (87, N'Saint George''''s', 1, 1, 0, 1496)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (87, N'Saint John', 1, 1, 0, 1497)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (87, N'Saint Mark', 1, 1, 0, 1498)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (87, N'Saint Patrick', 1, 1, 0, 1499)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (88, N'Basse-Terre', 1, 1, 0, 1500)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (88, N'Grande-Terre', 1, 1, 0, 1501)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (88, N'Iles des Saintes', 1, 1, 0, 1502)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (88, N'La Desirade', 1, 1, 0, 1503)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (88, N'Marie-Galante', 1, 1, 0, 1504)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (88, N'Saint Barthelemy', 1, 1, 0, 1505)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (88, N'Saint Martin', 1, 1, 0, 1506)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (89, N'Agana Heights', 1, 1, 0, 1507)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (89, N'Agat', 1, 1, 0, 1508)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (89, N'Barrigada', 1, 1, 0, 1509)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (89, N'Chalan-Pago-Ordot', 1, 1, 0, 1510)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (89, N'Dededo', 1, 1, 0, 1511)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (89, N'Hagatna', 1, 1, 0, 1512)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (89, N'Inarajan', 1, 1, 0, 1513)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (89, N'Mangilao', 1, 1, 0, 1514)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (89, N'Merizo', 1, 1, 0, 1515)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (89, N'Mongmong-Toto-Maite', 1, 1, 0, 1516)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (89, N'Santa Rita', 1, 1, 0, 1517)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (89, N'Sinajana', 1, 1, 0, 1518)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (89, N'Talofofo', 1, 1, 0, 1519)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (89, N'Tamuning', 1, 1, 0, 1520)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (89, N'Yigo', 1, 1, 0, 1521)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (89, N'Yona', 1, 1, 0, 1522)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (90, N'Alta Verapaz', 1, 1, 0, 1523)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (90, N'Baja Verapaz', 1, 1, 0, 1524)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (90, N'Chimaltenango', 1, 1, 0, 1525)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (90, N'Chiquimula', 1, 1, 0, 1526)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (90, N'El Progreso', 1, 1, 0, 1527)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (90, N'Escuintla', 1, 1, 0, 1528)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (90, N'Guatemala', 1, 1, 0, 1529)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (90, N'Huehuetenango', 1, 1, 0, 1530)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (90, N'Izabal', 1, 1, 0, 1531)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (90, N'Jalapa', 1, 1, 0, 1532)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (90, N'Jutiapa', 1, 1, 0, 1533)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (90, N'Peten', 1, 1, 0, 1534)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (90, N'Quezaltenango', 1, 1, 0, 1535)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (90, N'Quiche', 1, 1, 0, 1536)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (90, N'Retalhuleu', 1, 1, 0, 1537)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (90, N'Sacatepequez', 1, 1, 0, 1538)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (90, N'San Marcos', 1, 1, 0, 1539)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (90, N'Santa Rosa', 1, 1, 0, 1540)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (90, N'Solola', 1, 1, 0, 1541)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (90, N'Suchitepequez', 1, 1, 0, 1542)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (90, N'Totonicapan', 1, 1, 0, 1543)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (90, N'Zacapa', 1, 1, 0, 1544)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (91, N'Alderney', 1, 1, 0, 1545)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (91, N'Castel', 1, 1, 0, 1546)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (91, N'Forest', 1, 1, 0, 1547)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (91, N'Saint Andrew', 1, 1, 0, 1548)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (91, N'Saint Martin', 1, 1, 0, 1549)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (91, N'Saint Peter Port', 1, 1, 0, 1550)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (91, N'Saint Pierre du Bois', 1, 1, 0, 1551)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (91, N'Saint Sampson', 1, 1, 0, 1552)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (91, N'Saint Saviour', 1, 1, 0, 1553)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (91, N'Sark', 1, 1, 0, 1554)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (91, N'Torteval', 1, 1, 0, 1555)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (91, N'Vale', 1, 1, 0, 1556)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (92, N'Beyla', 1, 1, 0, 1557)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (92, N'Boffa', 1, 1, 0, 1558)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (92, N'Boke', 1, 1, 0, 1559)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (92, N'Conakry', 1, 1, 0, 1560)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (92, N'Coyah', 1, 1, 0, 1561)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (92, N'Dabola', 1, 1, 0, 1562)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (92, N'Dalaba', 1, 1, 0, 1563)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (92, N'Dinguiraye', 1, 1, 0, 1564)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (92, N'Faranah', 1, 1, 0, 1565)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (92, N'Forecariah', 1, 1, 0, 1566)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (92, N'Fria', 1, 1, 0, 1567)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (92, N'Gaoual', 1, 1, 0, 1568)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (92, N'Gueckedou', 1, 1, 0, 1569)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (92, N'Kankan', 1, 1, 0, 1570)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (92, N'Kerouane', 1, 1, 0, 1571)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (92, N'Kindia', 1, 1, 0, 1572)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (92, N'Kissidougou', 1, 1, 0, 1573)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (92, N'Koubia', 1, 1, 0, 1574)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (92, N'Koundara', 1, 1, 0, 1575)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (92, N'Kouroussa', 1, 1, 0, 1576)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (92, N'Labe', 1, 1, 0, 1577)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (92, N'Lola', 1, 1, 0, 1578)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (92, N'Macenta', 1, 1, 0, 1579)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (92, N'Mali', 1, 1, 0, 1580)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (92, N'Mamou', 1, 1, 0, 1581)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (92, N'Mandiana', 1, 1, 0, 1582)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (92, N'Nzerekore', 1, 1, 0, 1583)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (92, N'Pita', 1, 1, 0, 1584)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (92, N'Siguiri', 1, 1, 0, 1585)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (92, N'Telimele', 1, 1, 0, 1586)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (92, N'Tougue', 1, 1, 0, 1587)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (92, N'Yomou', 1, 1, 0, 1588)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (93, N'Bafata', 1, 1, 0, 1589)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (93, N'Bissau', 1, 1, 0, 1590)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (93, N'Bolama', 1, 1, 0, 1591)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (93, N'Cacheu', 1, 1, 0, 1592)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (93, N'Gabu', 1, 1, 0, 1593)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (93, N'Oio', 1, 1, 0, 1594)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (93, N'Quinara', 1, 1, 0, 1595)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (93, N'Tombali', 1, 1, 0, 1596)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (94, N'Barima-Waini', 1, 1, 0, 1597)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (94, N'Cuyuni-Mazaruni', 1, 1, 0, 1598)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (94, N'Demerara-Mahaica', 1, 1, 0, 1599)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (94, N'East Berbice-Corentyne', 1, 1, 0, 1600)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (94, N'Essequibo Islands-West Demerar', 1, 1, 0, 1601)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (94, N'Mahaica-Berbice', 1, 1, 0, 1602)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (94, N'Pomeroon-Supenaam', 1, 1, 0, 1603)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (94, N'Potaro-Siparuni', 1, 1, 0, 1604)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (94, N'Upper Demerara-Berbice', 1, 1, 0, 1605)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (94, N'Upper Takutu-Upper Essequibo', 1, 1, 0, 1606)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (95, N'Artibonite', 1, 1, 0, 1607)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (95, N'Centre', 1, 1, 0, 1608)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (95, N'Grand''''Anse', 1, 1, 0, 1609)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (95, N'Nord', 1, 1, 0, 1610)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (95, N'Nord-Est', 1, 1, 0, 1611)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (95, N'Nord-Ouest', 1, 1, 0, 1612)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (95, N'Ouest', 1, 1, 0, 1613)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (95, N'Sud', 1, 1, 0, 1614)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (95, N'Sud-Est', 1, 1, 0, 1615)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (96, N'Heard and McDonald Islands', 1, 1, 0, 1616)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (97, N'Atlantida', 1, 1, 0, 1617)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (97, N'Choluteca', 1, 1, 0, 1618)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (97, N'Colon', 1, 1, 0, 1619)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (97, N'Comayagua', 1, 1, 0, 1620)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (97, N'Copan', 1, 1, 0, 1621)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (97, N'Cortes', 1, 1, 0, 1622)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (97, N'Distrito Central', 1, 1, 0, 1623)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (97, N'El Paraiso', 1, 1, 0, 1624)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (97, N'Francisco Morazan', 1, 1, 0, 1625)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (97, N'Gracias a Dios', 1, 1, 0, 1626)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (97, N'Intibuca', 1, 1, 0, 1627)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (97, N'Islas de la Bahia', 1, 1, 0, 1628)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (97, N'La Paz', 1, 1, 0, 1629)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (97, N'Lempira', 1, 1, 0, 1630)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (97, N'Ocotepeque', 1, 1, 0, 1631)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (97, N'Olancho', 1, 1, 0, 1632)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (97, N'Santa Barbara', 1, 1, 0, 1633)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (97, N'Valle', 1, 1, 0, 1634)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (97, N'Yoro', 1, 1, 0, 1635)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (98, N'Hong Kong', 1, 1, 0, 1636)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (99, N'Bacs-Kiskun', 1, 1, 0, 1637)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (99, N'Baranya', 1, 1, 0, 1638)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (99, N'Bekes', 1, 1, 0, 1639)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (99, N'Borsod-Abauj-Zemplen', 1, 1, 0, 1640)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (99, N'Budapest', 1, 1, 0, 1641)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (99, N'Csongrad', 1, 1, 0, 1642)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (99, N'Fejer', 1, 1, 0, 1643)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (99, N'Gyor-Moson-Sopron', 1, 1, 0, 1644)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (99, N'Hajdu-Bihar', 1, 1, 0, 1645)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (99, N'Heves', 1, 1, 0, 1646)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (99, N'Jasz-Nagykun-Szolnok', 1, 1, 0, 1647)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (99, N'Komarom-Esztergom', 1, 1, 0, 1648)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (99, N'Nograd', 1, 1, 0, 1649)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (99, N'Pest', 1, 1, 0, 1650)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (99, N'Somogy', 1, 1, 0, 1651)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (99, N'Szabolcs-Szatmar-Bereg', 1, 1, 0, 1652)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (99, N'Tolna', 1, 1, 0, 1653)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (99, N'Vas', 1, 1, 0, 1654)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (99, N'Veszprem', 1, 1, 0, 1655)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (99, N'Zala', 1, 1, 0, 1656)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (100, N'Austurland', 1, 1, 0, 1657)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (100, N'Gullbringusysla', 1, 1, 0, 1658)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (100, N'Hofu borgarsva i', 1, 1, 0, 1659)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (100, N'Nor urland eystra', 1, 1, 0, 1660)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (100, N'Nor urland vestra', 1, 1, 0, 1661)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (100, N'Su urland', 1, 1, 0, 1662)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (100, N'Su urnes', 1, 1, 0, 1663)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (100, N'Vestfir ir', 1, 1, 0, 1664)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (100, N'Vesturland', 1, 1, 0, 1665)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (102, N'Aceh', 1, 1, 0, 1666)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (102, N'Bali', 1, 1, 0, 1667)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (102, N'Bangka-Belitung', 1, 1, 0, 1668)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (102, N'Banten', 1, 1, 0, 1669)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (102, N'Bengkulu', 1, 1, 0, 1670)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (102, N'Gandaria', 1, 1, 0, 1671)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (102, N'Gorontalo', 1, 1, 0, 1672)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (102, N'Jakarta', 1, 1, 0, 1673)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (102, N'Jambi', 1, 1, 0, 1674)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (102, N'Jawa Barat', 1, 1, 0, 1675)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (102, N'Jawa Tengah', 1, 1, 0, 1676)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (102, N'Jawa Timur', 1, 1, 0, 1677)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (102, N'Kalimantan Barat', 1, 1, 0, 1678)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (102, N'Kalimantan Selatan', 1, 1, 0, 1679)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (102, N'Kalimantan Tengah', 1, 1, 0, 1680)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (102, N'Kalimantan Timur', 1, 1, 0, 1681)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (102, N'Kendal', 1, 1, 0, 1682)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (102, N'Lampung', 1, 1, 0, 1683)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (102, N'Maluku', 1, 1, 0, 1684)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (102, N'Maluku Utara', 1, 1, 0, 1685)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (102, N'Nusa Tenggara Barat', 1, 1, 0, 1686)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (102, N'Nusa Tenggara Timur', 1, 1, 0, 1687)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (102, N'Papua', 1, 1, 0, 1688)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (102, N'Riau', 1, 1, 0, 1689)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (102, N'Riau Kepulauan', 1, 1, 0, 1690)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (102, N'Solo', 1, 1, 0, 1691)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (102, N'Sulawesi Selatan', 1, 1, 0, 1692)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (102, N'Sulawesi Tengah', 1, 1, 0, 1693)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (102, N'Sulawesi Tenggara', 1, 1, 0, 1694)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (102, N'Sulawesi Utara', 1, 1, 0, 1695)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (102, N'Sumatera Barat', 1, 1, 0, 1696)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (102, N'Sumatera Selatan', 1, 1, 0, 1697)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (102, N'Sumatera Utara', 1, 1, 0, 1698)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (102, N'Yogyakarta', 1, 1, 0, 1699)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (103, N'Ardabil', 1, 1, 0, 1700)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (103, N'Azarbayjan-e Bakhtari', 1, 1, 0, 1701)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (103, N'Azarbayjan-e Khavari', 1, 1, 0, 1702)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (103, N'Bushehr', 1, 1, 0, 1703)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (103, N'Chahar Mahal-e Bakhtiari', 1, 1, 0, 1704)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (103, N'Esfahan', 1, 1, 0, 1705)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (103, N'Fars', 1, 1, 0, 1706)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (103, N'Gilan', 1, 1, 0, 1707)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (103, N'Golestan', 1, 1, 0, 1708)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (103, N'Hamadan', 1, 1, 0, 1709)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (103, N'Hormozgan', 1, 1, 0, 1710)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (103, N'Ilam', 1, 1, 0, 1711)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (103, N'Kerman', 1, 1, 0, 1712)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (103, N'Kermanshah', 1, 1, 0, 1713)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (103, N'Khorasan', 1, 1, 0, 1714)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (103, N'Khuzestan', 1, 1, 0, 1715)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (103, N'Kohgiluyeh-e Boyerahmad', 1, 1, 0, 1716)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (103, N'Kordestan', 1, 1, 0, 1717)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (103, N'Lorestan', 1, 1, 0, 1718)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (103, N'Markazi', 1, 1, 0, 1719)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (103, N'Mazandaran', 1, 1, 0, 1720)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (103, N'Ostan-e Esfahan', 1, 1, 0, 1721)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (103, N'Qazvin', 1, 1, 0, 1722)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (103, N'Qom', 1, 1, 0, 1723)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (103, N'Semnan', 1, 1, 0, 1724)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (103, N'Sistan-e Baluchestan', 1, 1, 0, 1725)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (103, N'Tehran', 1, 1, 0, 1726)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (103, N'Yazd', 1, 1, 0, 1727)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (103, N'Zanjan', 1, 1, 0, 1728)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (104, N'Babil', 1, 1, 0, 1729)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (104, N'Baghdad', 1, 1, 0, 1730)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (104, N'Dahuk', 1, 1, 0, 1731)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (104, N'Dhi Qar', 1, 1, 0, 1732)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (104, N'Diyala', 1, 1, 0, 1733)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (104, N'Erbil', 1, 1, 0, 1734)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (104, N'Irbil', 1, 1, 0, 1735)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (104, N'Karbala', 1, 1, 0, 1736)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (104, N'Kurdistan', 1, 1, 0, 1737)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (104, N'Maysan', 1, 1, 0, 1738)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (104, N'Ninawa', 1, 1, 0, 1739)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (104, N'Salah-ad-Din', 1, 1, 0, 1740)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (104, N'Wasit', 1, 1, 0, 1741)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (104, N'al-Anbar', 1, 1, 0, 1742)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (104, N'al-Basrah', 1, 1, 0, 1743)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (104, N'al-Muthanna', 1, 1, 0, 1744)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (104, N'al-Qadisiyah', 1, 1, 0, 1745)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (104, N'an-Najaf', 1, 1, 0, 1746)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (104, N'as-Sulaymaniyah', 1, 1, 0, 1747)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (104, N'at-Ta''''mim', 1, 1, 0, 1748)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (105, N'Armagh', 1, 1, 0, 1749)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (105, N'Carlow', 1, 1, 0, 1750)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (105, N'Cavan', 1, 1, 0, 1751)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (105, N'Clare', 1, 1, 0, 1752)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (105, N'Cork', 1, 1, 0, 1753)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (105, N'Donegal', 1, 1, 0, 1754)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (105, N'Dublin', 1, 1, 0, 1755)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (105, N'Galway', 1, 1, 0, 1756)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (105, N'Kerry', 1, 1, 0, 1757)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (105, N'Kildare', 1, 1, 0, 1758)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (105, N'Kilkenny', 1, 1, 0, 1759)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (105, N'Laois', 1, 1, 0, 1760)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (105, N'Leinster', 1, 1, 0, 1761)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (105, N'Leitrim', 1, 1, 0, 1762)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (105, N'Limerick', 1, 1, 0, 1763)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (105, N'Loch Garman', 1, 1, 0, 1764)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (105, N'Longford', 1, 1, 0, 1765)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (105, N'Louth', 1, 1, 0, 1766)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (105, N'Mayo', 1, 1, 0, 1767)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (105, N'Meath', 1, 1, 0, 1768)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (105, N'Monaghan', 1, 1, 0, 1769)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (105, N'Offaly', 1, 1, 0, 1770)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (105, N'Roscommon', 1, 1, 0, 1771)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (105, N'Sligo', 1, 1, 0, 1772)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (105, N'Tipperary North Riding', 1, 1, 0, 1773)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (105, N'Tipperary South Riding', 1, 1, 0, 1774)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (105, N'Ulster', 1, 1, 0, 1775)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (105, N'Waterford', 1, 1, 0, 1776)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (105, N'Westmeath', 1, 1, 0, 1777)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (105, N'Wexford', 1, 1, 0, 1778)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (105, N'Wicklow', 1, 1, 0, 1779)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (106, N'Beit Hanania', 1, 1, 0, 1780)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (106, N'Ben Gurion Airport', 1, 1, 0, 1781)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (106, N'Bethlehem', 1, 1, 0, 1782)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (106, N'Caesarea', 1, 1, 0, 1783)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (106, N'Centre', 1, 1, 0, 1784)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (106, N'Gaza', 1, 1, 0, 1785)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (106, N'Hadaron', 1, 1, 0, 1786)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (106, N'Haifa District', 1, 1, 0, 1787)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (106, N'Hamerkaz', 1, 1, 0, 1788)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (106, N'Hazafon', 1, 1, 0, 1789)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (106, N'Hebron', 1, 1, 0, 1790)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (106, N'Jaffa', 1, 1, 0, 1791)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (106, N'Jerusalem', 1, 1, 0, 1792)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (106, N'Khefa', 1, 1, 0, 1793)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (106, N'Kiryat Yam', 1, 1, 0, 1794)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (106, N'Lower Galilee', 1, 1, 0, 1795)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (106, N'Qalqilya', 1, 1, 0, 1796)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (106, N'Talme Elazar', 1, 1, 0, 1797)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (106, N'Tel Aviv', 1, 1, 0, 1798)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (106, N'Tsafon', 1, 1, 0, 1799)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (106, N'Umm El Fahem', 1, 1, 0, 1800)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (106, N'Yerushalayim', 1, 1, 0, 1801)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Abruzzi', 1, 1, 0, 1802)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Abruzzo', 1, 1, 0, 1803)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Agrigento', 1, 1, 0, 1804)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Alessandria', 1, 1, 0, 1805)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Ancona', 1, 1, 0, 1806)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Arezzo', 1, 1, 0, 1807)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Ascoli Piceno', 1, 1, 0, 1808)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Asti', 1, 1, 0, 1809)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Avellino', 1, 1, 0, 1810)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Bari', 1, 1, 0, 1811)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Basilicata', 1, 1, 0, 1812)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Belluno', 1, 1, 0, 1813)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Benevento', 1, 1, 0, 1814)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Bergamo', 1, 1, 0, 1815)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Biella', 1, 1, 0, 1816)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Bologna', 1, 1, 0, 1817)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Bolzano', 1, 1, 0, 1818)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Brescia', 1, 1, 0, 1819)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Brindisi', 1, 1, 0, 1820)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Calabria', 1, 1, 0, 1821)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Campania', 1, 1, 0, 1822)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Cartoceto', 1, 1, 0, 1823)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Caserta', 1, 1, 0, 1824)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Catania', 1, 1, 0, 1825)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Chieti', 1, 1, 0, 1826)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Como', 1, 1, 0, 1827)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Cosenza', 1, 1, 0, 1828)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Cremona', 1, 1, 0, 1829)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Cuneo', 1, 1, 0, 1830)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Emilia-Romagna', 1, 1, 0, 1831)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Ferrara', 1, 1, 0, 1832)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Firenze', 1, 1, 0, 1833)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Florence', 1, 1, 0, 1834)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Forli-Cesena', 1, 1, 0, 1835)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Friuli-Venezia Giulia', 1, 1, 0, 1836)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Frosinone', 1, 1, 0, 1837)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Genoa', 1, 1, 0, 1838)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Gorizia', 1, 1, 0, 1839)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'L''''Aquila', 1, 1, 0, 1840)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Lazio', 1, 1, 0, 1841)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Lecce', 1, 1, 0, 1842)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Lecco', 1, 1, 0, 1843)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Lecco Province', 1, 1, 0, 1844)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Liguria', 1, 1, 0, 1845)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Lodi', 1, 1, 0, 1846)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Lombardia', 1, 1, 0, 1847)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Lombardy', 1, 1, 0, 1848)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Macerata', 1, 1, 0, 1849)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Mantova', 1, 1, 0, 1850)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Marche', 1, 1, 0, 1851)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Messina', 1, 1, 0, 1852)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Milan', 1, 1, 0, 1853)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Modena', 1, 1, 0, 1854)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Molise', 1, 1, 0, 1855)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Molteno', 1, 1, 0, 1856)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Montenegro', 1, 1, 0, 1857)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Monza and Brianza', 1, 1, 0, 1858)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Naples', 1, 1, 0, 1859)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Novara', 1, 1, 0, 1860)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Padova', 1, 1, 0, 1861)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Parma', 1, 1, 0, 1862)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Pavia', 1, 1, 0, 1863)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Perugia', 1, 1, 0, 1864)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Pesaro-Urbino', 1, 1, 0, 1865)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Piacenza', 1, 1, 0, 1866)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Piedmont', 1, 1, 0, 1867)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Piemonte', 1, 1, 0, 1868)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Pisa', 1, 1, 0, 1869)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Pordenone', 1, 1, 0, 1870)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Potenza', 1, 1, 0, 1871)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Puglia', 1, 1, 0, 1872)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Reggio Emilia', 1, 1, 0, 1873)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Rimini', 1, 1, 0, 1874)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Roma', 1, 1, 0, 1875)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Salerno', 1, 1, 0, 1876)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Sardegna', 1, 1, 0, 1877)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Sassari', 1, 1, 0, 1878)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Savona', 1, 1, 0, 1879)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Sicilia', 1, 1, 0, 1880)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Siena', 1, 1, 0, 1881)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Sondrio', 1, 1, 0, 1882)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'South Tyrol', 1, 1, 0, 1883)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Taranto', 1, 1, 0, 1884)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Teramo', 1, 1, 0, 1885)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Torino', 1, 1, 0, 1886)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Toscana', 1, 1, 0, 1887)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Trapani', 1, 1, 0, 1888)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Trentino-Alto Adige', 1, 1, 0, 1889)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Trento', 1, 1, 0, 1890)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Treviso', 1, 1, 0, 1891)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Udine', 1, 1, 0, 1892)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Umbria', 1, 1, 0, 1893)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Valle d''''Aosta', 1, 1, 0, 1894)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Varese', 1, 1, 0, 1895)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Veneto', 1, 1, 0, 1896)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Venezia', 1, 1, 0, 1897)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Verbano-Cusio-Ossola', 1, 1, 0, 1898)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Vercelli', 1, 1, 0, 1899)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Verona', 1, 1, 0, 1900)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Vicenza', 1, 1, 0, 1901)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (107, N'Viterbo', 1, 1, 0, 1902)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (108, N'Buxoro Viloyati', 1, 1, 0, 1903)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (108, N'Clarendon', 1, 1, 0, 1904)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (108, N'Hanover', 1, 1, 0, 1905)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (108, N'Kingston', 1, 1, 0, 1906)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (108, N'Manchester', 1, 1, 0, 1907)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (108, N'Portland', 1, 1, 0, 1908)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (108, N'Saint Andrews', 1, 1, 0, 1909)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (108, N'Saint Ann', 1, 1, 0, 1910)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (108, N'Saint Catherine', 1, 1, 0, 1911)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (108, N'Saint Elizabeth', 1, 1, 0, 1912)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (108, N'Saint James', 1, 1, 0, 1913)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (108, N'Saint Mary', 1, 1, 0, 1914)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (108, N'Saint Thomas', 1, 1, 0, 1915)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (108, N'Trelawney', 1, 1, 0, 1916)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (108, N'Westmoreland', 1, 1, 0, 1917)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (109, N'Aichi', 1, 1, 0, 1918)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (109, N'Akita', 1, 1, 0, 1919)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (109, N'Aomori', 1, 1, 0, 1920)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (109, N'Chiba', 1, 1, 0, 1921)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (109, N'Ehime', 1, 1, 0, 1922)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (109, N'Fukui', 1, 1, 0, 1923)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (109, N'Fukuoka', 1, 1, 0, 1924)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (109, N'Fukushima', 1, 1, 0, 1925)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (109, N'Gifu', 1, 1, 0, 1926)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (109, N'Gumma', 1, 1, 0, 1927)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (109, N'Hiroshima', 1, 1, 0, 1928)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (109, N'Hokkaido', 1, 1, 0, 1929)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (109, N'Hyogo', 1, 1, 0, 1930)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (109, N'Ibaraki', 1, 1, 0, 1931)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (109, N'Ishikawa', 1, 1, 0, 1932)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (109, N'Iwate', 1, 1, 0, 1933)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (109, N'Kagawa', 1, 1, 0, 1934)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (109, N'Kagoshima', 1, 1, 0, 1935)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (109, N'Kanagawa', 1, 1, 0, 1936)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (109, N'Kanto', 1, 1, 0, 1937)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (109, N'Kochi', 1, 1, 0, 1938)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (109, N'Kumamoto', 1, 1, 0, 1939)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (109, N'Kyoto', 1, 1, 0, 1940)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (109, N'Mie', 1, 1, 0, 1941)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (109, N'Miyagi', 1, 1, 0, 1942)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (109, N'Miyazaki', 1, 1, 0, 1943)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (109, N'Nagano', 1, 1, 0, 1944)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (109, N'Nagasaki', 1, 1, 0, 1945)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (109, N'Nara', 1, 1, 0, 1946)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (109, N'Niigata', 1, 1, 0, 1947)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (109, N'Oita', 1, 1, 0, 1948)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (109, N'Okayama', 1, 1, 0, 1949)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (109, N'Okinawa', 1, 1, 0, 1950)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (109, N'Osaka', 1, 1, 0, 1951)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (109, N'Saga', 1, 1, 0, 1952)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (109, N'Saitama', 1, 1, 0, 1953)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (109, N'Shiga', 1, 1, 0, 1954)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (109, N'Shimane', 1, 1, 0, 1955)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (109, N'Shizuoka', 1, 1, 0, 1956)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (109, N'Tochigi', 1, 1, 0, 1957)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (109, N'Tokushima', 1, 1, 0, 1958)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (109, N'Tokyo', 1, 1, 0, 1959)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (109, N'Tottori', 1, 1, 0, 1960)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (109, N'Toyama', 1, 1, 0, 1961)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (109, N'Wakayama', 1, 1, 0, 1962)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (109, N'Yamagata', 1, 1, 0, 1963)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (109, N'Yamaguchi', 1, 1, 0, 1964)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (109, N'Yamanashi', 1, 1, 0, 1965)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (110, N'Grouville', 1, 1, 0, 1966)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (110, N'Saint Brelade', 1, 1, 0, 1967)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (110, N'Saint Clement', 1, 1, 0, 1968)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (110, N'Saint Helier', 1, 1, 0, 1969)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (110, N'Saint John', 1, 1, 0, 1970)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (110, N'Saint Lawrence', 1, 1, 0, 1971)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (110, N'Saint Martin', 1, 1, 0, 1972)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (110, N'Saint Mary', 1, 1, 0, 1973)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (110, N'Saint Peter', 1, 1, 0, 1974)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (110, N'Saint Saviour', 1, 1, 0, 1975)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (110, N'Trinity', 1, 1, 0, 1976)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (111, N'Ajlun', 1, 1, 0, 1977)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (111, N'Amman', 1, 1, 0, 1978)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (111, N'Irbid', 1, 1, 0, 1979)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (111, N'Jarash', 1, 1, 0, 1980)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (111, N'Ma''''an', 1, 1, 0, 1981)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (111, N'Madaba', 1, 1, 0, 1982)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (111, N'al-''''Aqabah', 1, 1, 0, 1983)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (111, N'al-Balqa', 1, 1, 0, 1984)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (111, N'al-Karak', 1, 1, 0, 1985)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (111, N'al-Mafraq', 1, 1, 0, 1986)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (111, N'at-Tafilah', 1, 1, 0, 1987)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (111, N'az-Zarqa', 1, 1, 0, 1988)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (112, N'Akmecet', 1, 1, 0, 1989)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (112, N'Akmola', 1, 1, 0, 1990)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (112, N'Aktobe', 1, 1, 0, 1991)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (112, N'Almati', 1, 1, 0, 1992)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (112, N'Atirau', 1, 1, 0, 1993)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (112, N'Batis Kazakstan', 1, 1, 0, 1994)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (112, N'Burlinsky Region', 1, 1, 0, 1995)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (112, N'Karagandi', 1, 1, 0, 1996)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (112, N'Kostanay', 1, 1, 0, 1997)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (112, N'Mankistau', 1, 1, 0, 1998)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (112, N'Ontustik Kazakstan', 1, 1, 0, 1999)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (112, N'Pavlodar', 1, 1, 0, 2000)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (112, N'Sigis Kazakstan', 1, 1, 0, 2001)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (112, N'Soltustik Kazakstan', 1, 1, 0, 2002)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (112, N'Taraz', 1, 1, 0, 2003)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (113, N'Central', 1, 1, 0, 2004)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (113, N'Coast', 1, 1, 0, 2005)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (113, N'Eastern', 1, 1, 0, 2006)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (113, N'Nairobi', 1, 1, 0, 2007)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (113, N'North Eastern', 1, 1, 0, 2008)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (113, N'Nyanza', 1, 1, 0, 2009)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (113, N'Rift Valley', 1, 1, 0, 2010)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (113, N'Western', 1, 1, 0, 2011)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (114, N'Abaiang', 1, 1, 0, 2012)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (114, N'Abemana', 1, 1, 0, 2013)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (114, N'Aranuka', 1, 1, 0, 2014)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (114, N'Arorae', 1, 1, 0, 2015)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (114, N'Banaba', 1, 1, 0, 2016)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (114, N'Beru', 1, 1, 0, 2017)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (114, N'Butaritari', 1, 1, 0, 2018)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (114, N'Kiritimati', 1, 1, 0, 2019)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (114, N'Kuria', 1, 1, 0, 2020)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (114, N'Maiana', 1, 1, 0, 2021)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (114, N'Makin', 1, 1, 0, 2022)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (114, N'Marakei', 1, 1, 0, 2023)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (114, N'Nikunau', 1, 1, 0, 2024)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (114, N'Nonouti', 1, 1, 0, 2025)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (114, N'Onotoa', 1, 1, 0, 2026)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (114, N'Phoenix Islands', 1, 1, 0, 2027)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (114, N'Tabiteuea North', 1, 1, 0, 2028)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (114, N'Tabiteuea South', 1, 1, 0, 2029)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (114, N'Tabuaeran', 1, 1, 0, 2030)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (114, N'Tamana', 1, 1, 0, 2031)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (114, N'Tarawa North', 1, 1, 0, 2032)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (114, N'Tarawa South', 1, 1, 0, 2033)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (114, N'Teraina', 1, 1, 0, 2034)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (115, N'Chagangdo', 1, 1, 0, 2035)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (115, N'Hamgyeongbukto', 1, 1, 0, 2036)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (115, N'Hamgyeongnamdo', 1, 1, 0, 2037)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (115, N'Hwanghaebukto', 1, 1, 0, 2038)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (115, N'Hwanghaenamdo', 1, 1, 0, 2039)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (115, N'Kaeseong', 1, 1, 0, 2040)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (115, N'Kangweon', 1, 1, 0, 2041)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (115, N'Nampo', 1, 1, 0, 2042)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (115, N'Pyeonganbukto', 1, 1, 0, 2043)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (115, N'Pyeongannamdo', 1, 1, 0, 2044)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (115, N'Pyeongyang', 1, 1, 0, 2045)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (115, N'Yanggang', 1, 1, 0, 2046)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (116, N'Busan', 1, 1, 0, 2047)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (116, N'Cheju', 1, 1, 0, 2048)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (116, N'Chollabuk', 1, 1, 0, 2049)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (116, N'Chollanam', 1, 1, 0, 2050)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (116, N'Chungbuk', 1, 1, 0, 2051)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (116, N'Chungcheongbuk', 1, 1, 0, 2052)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (116, N'Chungcheongnam', 1, 1, 0, 2053)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (116, N'Chungnam', 1, 1, 0, 2054)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (116, N'Daegu', 1, 1, 0, 2055)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (116, N'Gangwon-do', 1, 1, 0, 2056)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (116, N'Goyang-si', 1, 1, 0, 2057)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (116, N'Gyeonggi-do', 1, 1, 0, 2058)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (116, N'Gyeongsang', 1, 1, 0, 2059)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (116, N'Gyeongsangnam-do', 1, 1, 0, 2060)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (116, N'Incheon', 1, 1, 0, 2061)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (116, N'Jeju-Si', 1, 1, 0, 2062)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (116, N'Jeonbuk', 1, 1, 0, 2063)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (116, N'Kangweon', 1, 1, 0, 2064)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (116, N'Kwangju', 1, 1, 0, 2065)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (116, N'Kyeonggi', 1, 1, 0, 2066)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (116, N'Kyeongsangbuk', 1, 1, 0, 2067)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (116, N'Kyeongsangnam', 1, 1, 0, 2068)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (116, N'Kyonggi-do', 1, 1, 0, 2069)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (116, N'Kyungbuk-Do', 1, 1, 0, 2070)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (116, N'Kyunggi-Do', 1, 1, 0, 2071)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (116, N'Kyunggi-do', 1, 1, 0, 2072)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (116, N'Pusan', 1, 1, 0, 2073)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (116, N'Seoul', 1, 1, 0, 2074)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (116, N'Sudogwon', 1, 1, 0, 2075)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (116, N'Taegu', 1, 1, 0, 2076)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (116, N'Taejeon', 1, 1, 0, 2077)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (116, N'Taejon-gwangyoksi', 1, 1, 0, 2078)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (116, N'Ulsan', 1, 1, 0, 2079)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (116, N'Wonju', 1, 1, 0, 2080)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (116, N'gwangyoksi', 1, 1, 0, 2081)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (117, N'Al Asimah', 1, 1, 0, 2082)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (117, N'Hawalli', 1, 1, 0, 2083)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (117, N'Mishref', 1, 1, 0, 2084)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (117, N'Qadesiya', 1, 1, 0, 2085)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (117, N'Safat', 1, 1, 0, 2086)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (117, N'Salmiya', 1, 1, 0, 2087)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (117, N'al-Ahmadi', 1, 1, 0, 2088)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (117, N'al-Farwaniyah', 1, 1, 0, 2089)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (117, N'al-Jahra', 1, 1, 0, 2090)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (117, N'al-Kuwayt', 1, 1, 0, 2091)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (118, N'Batken', 1, 1, 0, 2092)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (118, N'Bishkek', 1, 1, 0, 2093)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (118, N'Chui', 1, 1, 0, 2094)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (118, N'Issyk-Kul', 1, 1, 0, 2095)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (118, N'Jalal-Abad', 1, 1, 0, 2096)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (118, N'Naryn', 1, 1, 0, 2097)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (118, N'Osh', 1, 1, 0, 2098)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (118, N'Talas', 1, 1, 0, 2099)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (119, N'Attopu', 1, 1, 0, 2100)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (119, N'Bokeo', 1, 1, 0, 2101)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (119, N'Bolikhamsay', 1, 1, 0, 2102)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (119, N'Champasak', 1, 1, 0, 2103)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (119, N'Houaphanh', 1, 1, 0, 2104)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (119, N'Khammouane', 1, 1, 0, 2105)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (119, N'Luang Nam Tha', 1, 1, 0, 2106)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (119, N'Luang Prabang', 1, 1, 0, 2107)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (119, N'Oudomxay', 1, 1, 0, 2108)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (119, N'Phongsaly', 1, 1, 0, 2109)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (119, N'Saravan', 1, 1, 0, 2110)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (119, N'Savannakhet', 1, 1, 0, 2111)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (119, N'Sekong', 1, 1, 0, 2112)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (119, N'Viangchan Prefecture', 1, 1, 0, 2113)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (119, N'Viangchan Province', 1, 1, 0, 2114)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (119, N'Xaignabury', 1, 1, 0, 2115)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (119, N'Xiang Khuang', 1, 1, 0, 2116)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (120, N'Aizkraukles', 1, 1, 0, 2117)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (120, N'Aluksnes', 1, 1, 0, 2118)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (120, N'Balvu', 1, 1, 0, 2119)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (120, N'Bauskas', 1, 1, 0, 2120)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (120, N'Cesu', 1, 1, 0, 2121)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (120, N'Daugavpils', 1, 1, 0, 2122)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (120, N'Daugavpils City', 1, 1, 0, 2123)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (120, N'Dobeles', 1, 1, 0, 2124)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (120, N'Gulbenes', 1, 1, 0, 2125)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (120, N'Jekabspils', 1, 1, 0, 2126)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (120, N'Jelgava', 1, 1, 0, 2127)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (120, N'Jelgavas', 1, 1, 0, 2128)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (120, N'Jurmala City', 1, 1, 0, 2129)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (120, N'Kraslavas', 1, 1, 0, 2130)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (120, N'Kuldigas', 1, 1, 0, 2131)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (120, N'Liepaja', 1, 1, 0, 2132)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (120, N'Liepajas', 1, 1, 0, 2133)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (120, N'Limbazhu', 1, 1, 0, 2134)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (120, N'Ludzas', 1, 1, 0, 2135)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (120, N'Madonas', 1, 1, 0, 2136)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (120, N'Ogres', 1, 1, 0, 2137)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (120, N'Preilu', 1, 1, 0, 2138)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (120, N'Rezekne', 1, 1, 0, 2139)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (120, N'Rezeknes', 1, 1, 0, 2140)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (120, N'Riga', 1, 1, 0, 2141)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (120, N'Rigas', 1, 1, 0, 2142)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (120, N'Saldus', 1, 1, 0, 2143)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (120, N'Talsu', 1, 1, 0, 2144)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (120, N'Tukuma', 1, 1, 0, 2145)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (120, N'Valkas', 1, 1, 0, 2146)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (120, N'Valmieras', 1, 1, 0, 2147)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (120, N'Ventspils', 1, 1, 0, 2148)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (120, N'Ventspils City', 1, 1, 0, 2149)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (121, N'Beirut', 1, 1, 0, 2150)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (121, N'Jabal Lubnan', 1, 1, 0, 2151)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (121, N'Mohafazat Liban-Nord', 1, 1, 0, 2152)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (121, N'Mohafazat Mont-Liban', 1, 1, 0, 2153)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (121, N'Sidon', 1, 1, 0, 2154)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (121, N'al-Biqa', 1, 1, 0, 2155)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (121, N'al-Janub', 1, 1, 0, 2156)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (121, N'an-Nabatiyah', 1, 1, 0, 2157)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (121, N'ash-Shamal', 1, 1, 0, 2158)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (122, N'Berea', 1, 1, 0, 2159)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (122, N'Butha-Buthe', 1, 1, 0, 2160)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (122, N'Leribe', 1, 1, 0, 2161)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (122, N'Mafeteng', 1, 1, 0, 2162)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (122, N'Maseru', 1, 1, 0, 2163)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (122, N'Mohale''''s Hoek', 1, 1, 0, 2164)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (122, N'Mokhotlong', 1, 1, 0, 2165)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (122, N'Qacha''''s Nek', 1, 1, 0, 2166)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (122, N'Quthing', 1, 1, 0, 2167)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (122, N'Thaba-Tseka', 1, 1, 0, 2168)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (123, N'Bomi', 1, 1, 0, 2169)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (123, N'Bong', 1, 1, 0, 2170)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (123, N'Grand Bassa', 1, 1, 0, 2171)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (123, N'Grand Cape Mount', 1, 1, 0, 2172)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (123, N'Grand Gedeh', 1, 1, 0, 2173)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (123, N'Loffa', 1, 1, 0, 2174)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (123, N'Margibi', 1, 1, 0, 2175)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (123, N'Maryland and Grand Kru', 1, 1, 0, 2176)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (123, N'Montserrado', 1, 1, 0, 2177)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (123, N'Nimba', 1, 1, 0, 2178)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (123, N'Rivercess', 1, 1, 0, 2179)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (123, N'Sinoe', 1, 1, 0, 2180)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (124, N'Ajdabiya', 1, 1, 0, 2181)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (124, N'Fezzan', 1, 1, 0, 2182)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (124, N'Banghazi', 1, 1, 0, 2183)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (124, N'Darnah', 1, 1, 0, 2184)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (124, N'Ghadamis', 1, 1, 0, 2185)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (124, N'Gharyan', 1, 1, 0, 2186)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (124, N'Misratah', 1, 1, 0, 2187)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (124, N'Murzuq', 1, 1, 0, 2188)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (124, N'Sabha', 1, 1, 0, 2189)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (124, N'Sawfajjin', 1, 1, 0, 2190)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (124, N'Surt', 1, 1, 0, 2191)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (124, N'Tarabulus', 1, 1, 0, 2192)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (124, N'Tarhunah', 1, 1, 0, 2193)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (124, N'Tripolitania', 1, 1, 0, 2194)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (124, N'Tubruq', 1, 1, 0, 2195)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (124, N'Yafran', 1, 1, 0, 2196)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (124, N'Zlitan', 1, 1, 0, 2197)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (124, N'al-''''Aziziyah', 1, 1, 0, 2198)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (124, N'al-Fatih', 1, 1, 0, 2199)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (124, N'al-Jabal al Akhdar', 1, 1, 0, 2200)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (124, N'al-Jufrah', 1, 1, 0, 2201)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (124, N'al-Khums', 1, 1, 0, 2202)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (124, N'al-Kufrah', 1, 1, 0, 2203)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (124, N'an-Nuqat al-Khams', 1, 1, 0, 2204)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (124, N'ash-Shati', 1, 1, 0, 2205)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (124, N'az-Zawiyah', 1, 1, 0, 2206)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (125, N'Balzers', 1, 1, 0, 2207)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (125, N'Eschen', 1, 1, 0, 2208)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (125, N'Gamprin', 1, 1, 0, 2209)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (125, N'Mauren', 1, 1, 0, 2210)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (125, N'Planken', 1, 1, 0, 2211)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (125, N'Ruggell', 1, 1, 0, 2212)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (125, N'Schaan', 1, 1, 0, 2213)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (125, N'Schellenberg', 1, 1, 0, 2214)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (125, N'Triesen', 1, 1, 0, 2215)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (125, N'Triesenberg', 1, 1, 0, 2216)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (125, N'Vaduz', 1, 1, 0, 2217)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (126, N'Alytaus', 1, 1, 0, 2218)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (126, N'Anyksciai', 1, 1, 0, 2219)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (126, N'Kauno', 1, 1, 0, 2220)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (126, N'Klaipedos', 1, 1, 0, 2221)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (126, N'Marijampoles', 1, 1, 0, 2222)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (126, N'Panevezhio', 1, 1, 0, 2223)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (126, N'Panevezys', 1, 1, 0, 2224)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (126, N'Shiauliu', 1, 1, 0, 2225)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (126, N'Taurages', 1, 1, 0, 2226)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (126, N'Telshiu', 1, 1, 0, 2227)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (126, N'Telsiai', 1, 1, 0, 2228)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (126, N'Utenos', 1, 1, 0, 2229)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (126, N'Vilniaus', 1, 1, 0, 2230)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (127, N'Capellen', 1, 1, 0, 2231)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (127, N'Clervaux', 1, 1, 0, 2232)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (127, N'Diekirch', 1, 1, 0, 2233)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (127, N'Echternach', 1, 1, 0, 2234)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (127, N'Esch-sur-Alzette', 1, 1, 0, 2235)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (127, N'Grevenmacher', 1, 1, 0, 2236)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (127, N'Luxembourg', 1, 1, 0, 2237)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (127, N'Mersch', 1, 1, 0, 2238)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (127, N'Redange', 1, 1, 0, 2239)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (127, N'Remich', 1, 1, 0, 2240)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (127, N'Vianden', 1, 1, 0, 2241)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (127, N'Wiltz', 1, 1, 0, 2242)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (128, N'Macau', 1, 1, 0, 2243)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (129, N'Berovo', 1, 1, 0, 2244)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (129, N'Bitola', 1, 1, 0, 2245)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (129, N'Brod', 1, 1, 0, 2246)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (129, N'Debar', 1, 1, 0, 2247)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (129, N'Delchevo', 1, 1, 0, 2248)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (129, N'Demir Hisar', 1, 1, 0, 2249)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (129, N'Gevgelija', 1, 1, 0, 2250)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (129, N'Gostivar', 1, 1, 0, 2251)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (129, N'Kavadarci', 1, 1, 0, 2252)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (129, N'Kichevo', 1, 1, 0, 2253)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (129, N'Kochani', 1, 1, 0, 2254)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (129, N'Kratovo', 1, 1, 0, 2255)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (129, N'Kriva Palanka', 1, 1, 0, 2256)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (129, N'Krushevo', 1, 1, 0, 2257)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (129, N'Kumanovo', 1, 1, 0, 2258)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (129, N'Negotino', 1, 1, 0, 2259)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (129, N'Ohrid', 1, 1, 0, 2260)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (129, N'Prilep', 1, 1, 0, 2261)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (129, N'Probishtip', 1, 1, 0, 2262)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (129, N'Radovish', 1, 1, 0, 2263)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (129, N'Resen', 1, 1, 0, 2264)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (129, N'Shtip', 1, 1, 0, 2265)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (129, N'Skopje', 1, 1, 0, 2266)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (129, N'Struga', 1, 1, 0, 2267)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (129, N'Strumica', 1, 1, 0, 2268)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (129, N'Sveti Nikole', 1, 1, 0, 2269)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (129, N'Tetovo', 1, 1, 0, 2270)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (129, N'Valandovo', 1, 1, 0, 2271)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (129, N'Veles', 1, 1, 0, 2272)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (129, N'Vinica', 1, 1, 0, 2273)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (130, N'Antananarivo', 1, 1, 0, 2274)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (130, N'Antsiranana', 1, 1, 0, 2275)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (130, N'Fianarantsoa', 1, 1, 0, 2276)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (130, N'Mahajanga', 1, 1, 0, 2277)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (130, N'Toamasina', 1, 1, 0, 2278)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (130, N'Toliary', 1, 1, 0, 2279)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (131, N'Balaka', 1, 1, 0, 2280)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (131, N'Blantyre City', 1, 1, 0, 2281)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (131, N'Chikwawa', 1, 1, 0, 2282)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (131, N'Chiradzulu', 1, 1, 0, 2283)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (131, N'Chitipa', 1, 1, 0, 2284)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (131, N'Dedza', 1, 1, 0, 2285)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (131, N'Dowa', 1, 1, 0, 2286)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (131, N'Karonga', 1, 1, 0, 2287)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (131, N'Kasungu', 1, 1, 0, 2288)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (131, N'Lilongwe City', 1, 1, 0, 2289)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (131, N'Machinga', 1, 1, 0, 2290)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (131, N'Mangochi', 1, 1, 0, 2291)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (131, N'Mchinji', 1, 1, 0, 2292)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (131, N'Mulanje', 1, 1, 0, 2293)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (131, N'Mwanza', 1, 1, 0, 2294)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (131, N'Mzimba', 1, 1, 0, 2295)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (131, N'Mzuzu City', 1, 1, 0, 2296)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (131, N'Nkhata Bay', 1, 1, 0, 2297)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (131, N'Nkhotakota', 1, 1, 0, 2298)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (131, N'Nsanje', 1, 1, 0, 2299)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (131, N'Ntcheu', 1, 1, 0, 2300)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (131, N'Ntchisi', 1, 1, 0, 2301)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (131, N'Phalombe', 1, 1, 0, 2302)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (131, N'Rumphi', 1, 1, 0, 2303)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (131, N'Salima', 1, 1, 0, 2304)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (131, N'Thyolo', 1, 1, 0, 2305)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (131, N'Zomba Municipality', 1, 1, 0, 2306)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (132, N'Johor', 1, 1, 0, 2307)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (132, N'Kedah', 1, 1, 0, 2308)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (132, N'Kelantan', 1, 1, 0, 2309)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (132, N'Kuala Lumpur', 1, 1, 0, 2310)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (132, N'Labuan', 1, 1, 0, 2311)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (132, N'Melaka', 1, 1, 0, 2312)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (132, N'Negeri Johor', 1, 1, 0, 2313)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (132, N'Negeri Sembilan', 1, 1, 0, 2314)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (132, N'Pahang', 1, 1, 0, 2315)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (132, N'Penang', 1, 1, 0, 2316)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (132, N'Perak', 1, 1, 0, 2317)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (132, N'Perlis', 1, 1, 0, 2318)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (132, N'Pulau Pinang', 1, 1, 0, 2319)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (132, N'Sabah', 1, 1, 0, 2320)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (132, N'Sarawak', 1, 1, 0, 2321)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (132, N'Selangor', 1, 1, 0, 2322)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (132, N'Sembilan', 1, 1, 0, 2323)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (132, N'Terengganu', 1, 1, 0, 2324)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (133, N'Alif Alif', 1, 1, 0, 2325)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (133, N'Alif Dhaal', 1, 1, 0, 2326)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (133, N'Baa', 1, 1, 0, 2327)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (133, N'Dhaal', 1, 1, 0, 2328)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (133, N'Faaf', 1, 1, 0, 2329)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (133, N'Gaaf Alif', 1, 1, 0, 2330)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (133, N'Gaaf Dhaal', 1, 1, 0, 2331)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (133, N'Ghaviyani', 1, 1, 0, 2332)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (133, N'Haa Alif', 1, 1, 0, 2333)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (133, N'Haa Dhaal', 1, 1, 0, 2334)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (133, N'Kaaf', 1, 1, 0, 2335)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (133, N'Laam', 1, 1, 0, 2336)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (133, N'Lhaviyani', 1, 1, 0, 2337)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (133, N'Male', 1, 1, 0, 2338)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (133, N'Miim', 1, 1, 0, 2339)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (133, N'Nuun', 1, 1, 0, 2340)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (133, N'Raa', 1, 1, 0, 2341)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (133, N'Shaviyani', 1, 1, 0, 2342)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (133, N'Siin', 1, 1, 0, 2343)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (133, N'Thaa', 1, 1, 0, 2344)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (133, N'Vaav', 1, 1, 0, 2345)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (134, N'Bamako', 1, 1, 0, 2346)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (134, N'Gao', 1, 1, 0, 2347)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (134, N'Kayes', 1, 1, 0, 2348)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (134, N'Kidal', 1, 1, 0, 2349)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (134, N'Koulikoro', 1, 1, 0, 2350)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (134, N'Mopti', 1, 1, 0, 2351)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (134, N'Segou', 1, 1, 0, 2352)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (134, N'Sikasso', 1, 1, 0, 2353)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (134, N'Tombouctou', 1, 1, 0, 2354)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (135, N'Gozo and Comino', 1, 1, 0, 2355)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (135, N'Inner Harbour', 1, 1, 0, 2356)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (135, N'Northern', 1, 1, 0, 2357)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (135, N'Outer Harbour', 1, 1, 0, 2358)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (135, N'South Eastern', 1, 1, 0, 2359)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (135, N'Valletta', 1, 1, 0, 2360)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (135, N'Western', 1, 1, 0, 2361)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (136, N'Castletown', 1, 1, 0, 2362)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (136, N'Douglas', 1, 1, 0, 2363)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (136, N'Laxey', 1, 1, 0, 2364)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (136, N'Onchan', 1, 1, 0, 2365)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (136, N'Peel', 1, 1, 0, 2366)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (136, N'Port Erin', 1, 1, 0, 2367)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (136, N'Port Saint Mary', 1, 1, 0, 2368)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (136, N'Ramsey', 1, 1, 0, 2369)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (137, N'Ailinlaplap', 1, 1, 0, 2370)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (137, N'Ailuk', 1, 1, 0, 2371)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (137, N'Arno', 1, 1, 0, 2372)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (137, N'Aur', 1, 1, 0, 2373)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (137, N'Bikini', 1, 1, 0, 2374)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (137, N'Ebon', 1, 1, 0, 2375)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (137, N'Enewetak', 1, 1, 0, 2376)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (137, N'Jabat', 1, 1, 0, 2377)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (137, N'Jaluit', 1, 1, 0, 2378)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (137, N'Kili', 1, 1, 0, 2379)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (137, N'Kwajalein', 1, 1, 0, 2380)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (137, N'Lae', 1, 1, 0, 2381)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (137, N'Lib', 1, 1, 0, 2382)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (137, N'Likiep', 1, 1, 0, 2383)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (137, N'Majuro', 1, 1, 0, 2384)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (137, N'Maloelap', 1, 1, 0, 2385)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (137, N'Mejit', 1, 1, 0, 2386)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (137, N'Mili', 1, 1, 0, 2387)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (137, N'Namorik', 1, 1, 0, 2388)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (137, N'Namu', 1, 1, 0, 2389)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (137, N'Rongelap', 1, 1, 0, 2390)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (137, N'Ujae', 1, 1, 0, 2391)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (137, N'Utrik', 1, 1, 0, 2392)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (137, N'Wotho', 1, 1, 0, 2393)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (137, N'Wotje', 1, 1, 0, 2394)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (138, N'Fort-de-France', 1, 1, 0, 2395)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (138, N'La Trinite', 1, 1, 0, 2396)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (138, N'Le Marin', 1, 1, 0, 2397)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (138, N'Saint-Pierre', 1, 1, 0, 2398)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (139, N'Adrar', 1, 1, 0, 2399)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (139, N'Assaba', 1, 1, 0, 2400)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (139, N'Brakna', 1, 1, 0, 2401)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (139, N'Dhakhlat Nawadibu', 1, 1, 0, 2402)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (139, N'Hudh-al-Gharbi', 1, 1, 0, 2403)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (139, N'Hudh-ash-Sharqi', 1, 1, 0, 2404)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (139, N'Inshiri', 1, 1, 0, 2405)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (139, N'Nawakshut', 1, 1, 0, 2406)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (139, N'Qidimagha', 1, 1, 0, 2407)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (139, N'Qurqul', 1, 1, 0, 2408)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (139, N'Taqant', 1, 1, 0, 2409)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (139, N'Tiris Zammur', 1, 1, 0, 2410)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (139, N'Trarza', 1, 1, 0, 2411)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (140, N'Black River', 1, 1, 0, 2412)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (140, N'Eau Coulee', 1, 1, 0, 2413)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (140, N'Flacq', 1, 1, 0, 2414)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (140, N'Floreal', 1, 1, 0, 2415)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (140, N'Grand Port', 1, 1, 0, 2416)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (140, N'Moka', 1, 1, 0, 2417)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (140, N'Pamplempousses', 1, 1, 0, 2418)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (140, N'Plaines Wilhelm', 1, 1, 0, 2419)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (140, N'Port Louis', 1, 1, 0, 2420)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (140, N'Riviere du Rempart', 1, 1, 0, 2421)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (140, N'Rodrigues', 1, 1, 0, 2422)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (140, N'Rose Hill', 1, 1, 0, 2423)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (140, N'Savanne', 1, 1, 0, 2424)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (141, N'Mayotte', 1, 1, 0, 2425)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (141, N'Pamanzi', 1, 1, 0, 2426)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (142, N'Aguascalientes', 1, 1, 0, 2427)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (142, N'Baja California', 1, 1, 0, 2428)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (142, N'Baja California Sur', 1, 1, 0, 2429)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (142, N'Campeche', 1, 1, 0, 2430)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (142, N'Chiapas', 1, 1, 0, 2431)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (142, N'Chihuahua', 1, 1, 0, 2432)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (142, N'Coahuila', 1, 1, 0, 2433)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (142, N'Colima', 1, 1, 0, 2434)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (142, N'Distrito Federal', 1, 1, 0, 2435)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (142, N'Durango', 1, 1, 0, 2436)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (142, N'Estado de Mexico', 1, 1, 0, 2437)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (142, N'Guanajuato', 1, 1, 0, 2438)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (142, N'Guerrero', 1, 1, 0, 2439)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (142, N'Hidalgo', 1, 1, 0, 2440)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (142, N'Jalisco', 1, 1, 0, 2441)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (142, N'Mexico', 1, 1, 0, 2442)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (142, N'Michoacan', 1, 1, 0, 2443)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (142, N'Morelos', 1, 1, 0, 2444)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (142, N'Nayarit', 1, 1, 0, 2445)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (142, N'Nuevo Leon', 1, 1, 0, 2446)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (142, N'Oaxaca', 1, 1, 0, 2447)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (142, N'Puebla', 1, 1, 0, 2448)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (142, N'Queretaro', 1, 1, 0, 2449)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (142, N'Quintana Roo', 1, 1, 0, 2450)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (142, N'San Luis Potosi', 1, 1, 0, 2451)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (142, N'Sinaloa', 1, 1, 0, 2452)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (142, N'Sonora', 1, 1, 0, 2453)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (142, N'Tabasco', 1, 1, 0, 2454)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (142, N'Tamaulipas', 1, 1, 0, 2455)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (142, N'Tlaxcala', 1, 1, 0, 2456)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (142, N'Veracruz', 1, 1, 0, 2457)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (142, N'Yucatan', 1, 1, 0, 2458)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (142, N'Zacatecas', 1, 1, 0, 2459)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (143, N'Chuuk', 1, 1, 0, 2460)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (143, N'Kusaie', 1, 1, 0, 2461)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (143, N'Pohnpei', 1, 1, 0, 2462)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (143, N'Yap', 1, 1, 0, 2463)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (144, N'Balti', 1, 1, 0, 2464)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (144, N'Cahul', 1, 1, 0, 2465)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (144, N'Chisinau', 1, 1, 0, 2466)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (144, N'Chisinau Oras', 1, 1, 0, 2467)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (144, N'Edinet', 1, 1, 0, 2468)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (144, N'Gagauzia', 1, 1, 0, 2469)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (144, N'Lapusna', 1, 1, 0, 2470)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (144, N'Orhei', 1, 1, 0, 2471)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (144, N'Soroca', 1, 1, 0, 2472)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (144, N'Taraclia', 1, 1, 0, 2473)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (144, N'Tighina', 1, 1, 0, 2474)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (144, N'Transnistria', 1, 1, 0, 2475)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (144, N'Ungheni', 1, 1, 0, 2476)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (145, N'Fontvieille', 1, 1, 0, 2477)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (145, N'La Condamine', 1, 1, 0, 2478)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (145, N'Monaco-Ville', 1, 1, 0, 2479)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (145, N'Monte Carlo', 1, 1, 0, 2480)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (146, N'Arhangaj', 1, 1, 0, 2481)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (146, N'Bajan-Olgij', 1, 1, 0, 2482)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (146, N'Bajanhongor', 1, 1, 0, 2483)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (146, N'Bulgan', 1, 1, 0, 2484)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (146, N'Darhan-Uul', 1, 1, 0, 2485)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (146, N'Dornod', 1, 1, 0, 2486)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (146, N'Dornogovi', 1, 1, 0, 2487)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (146, N'Dundgovi', 1, 1, 0, 2488)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (146, N'Govi-Altaj', 1, 1, 0, 2489)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (146, N'Govisumber', 1, 1, 0, 2490)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (146, N'Hentij', 1, 1, 0, 2491)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (146, N'Hovd', 1, 1, 0, 2492)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (146, N'Hovsgol', 1, 1, 0, 2493)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (146, N'Omnogovi', 1, 1, 0, 2494)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (146, N'Orhon', 1, 1, 0, 2495)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (146, N'Ovorhangaj', 1, 1, 0, 2496)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (146, N'Selenge', 1, 1, 0, 2497)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (146, N'Suhbaatar', 1, 1, 0, 2498)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (146, N'Tov', 1, 1, 0, 2499)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (146, N'Ulaanbaatar', 1, 1, 0, 2500)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (146, N'Uvs', 1, 1, 0, 2501)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (146, N'Zavhan', 1, 1, 0, 2502)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (147, N'Montserrat', 1, 1, 0, 2503)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (148, N'Agadir', 1, 1, 0, 2504)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (148, N'Casablanca', 1, 1, 0, 2505)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (148, N'Chaouia-Ouardigha', 1, 1, 0, 2506)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (148, N'Doukkala-Abda', 1, 1, 0, 2507)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (148, N'Fes-Boulemane', 1, 1, 0, 2508)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (148, N'Gharb-Chrarda-Beni Hssen', 1, 1, 0, 2509)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (148, N'Guelmim', 1, 1, 0, 2510)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (148, N'Kenitra', 1, 1, 0, 2511)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (148, N'Marrakech-Tensift-Al Haouz', 1, 1, 0, 2512)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (148, N'Meknes-Tafilalet', 1, 1, 0, 2513)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (148, N'Oriental', 1, 1, 0, 2514)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (148, N'Oujda', 1, 1, 0, 2515)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (148, N'Province de Tanger', 1, 1, 0, 2516)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (148, N'Rabat-Sale-Zammour-Zaer', 1, 1, 0, 2517)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (148, N'Sala Al Jadida', 1, 1, 0, 2518)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (148, N'Settat', 1, 1, 0, 2519)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (148, N'Souss Massa-Draa', 1, 1, 0, 2520)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (148, N'Tadla-Azilal', 1, 1, 0, 2521)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (148, N'Tangier-Tetouan', 1, 1, 0, 2522)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (148, N'Taza-Al Hoceima-Taounate', 1, 1, 0, 2523)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (148, N'Wilaya de Casablanca', 1, 1, 0, 2524)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (148, N'Wilaya de Rabat-Sale', 1, 1, 0, 2525)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (149, N'Cabo Delgado', 1, 1, 0, 2526)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (149, N'Gaza', 1, 1, 0, 2527)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (149, N'Inhambane', 1, 1, 0, 2528)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (149, N'Manica', 1, 1, 0, 2529)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (149, N'Maputo', 1, 1, 0, 2530)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (149, N'Maputo Provincia', 1, 1, 0, 2531)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (149, N'Nampula', 1, 1, 0, 2532)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (149, N'Niassa', 1, 1, 0, 2533)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (149, N'Sofala', 1, 1, 0, 2534)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (149, N'Tete', 1, 1, 0, 2535)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (149, N'Zambezia', 1, 1, 0, 2536)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (150, N'Ayeyarwady', 1, 1, 0, 2537)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (150, N'Bago', 1, 1, 0, 2538)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (150, N'Chin', 1, 1, 0, 2539)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (150, N'Kachin', 1, 1, 0, 2540)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (150, N'Kayah', 1, 1, 0, 2541)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (150, N'Kayin', 1, 1, 0, 2542)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (150, N'Magway', 1, 1, 0, 2543)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (150, N'Mandalay', 1, 1, 0, 2544)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (150, N'Mon', 1, 1, 0, 2545)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (150, N'Nay Pyi Taw', 1, 1, 0, 2546)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (150, N'Rakhine', 1, 1, 0, 2547)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (150, N'Sagaing', 1, 1, 0, 2548)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (150, N'Shan', 1, 1, 0, 2549)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (150, N'Tanintharyi', 1, 1, 0, 2550)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (150, N'Yangon', 1, 1, 0, 2551)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (151, N'Caprivi', 1, 1, 0, 2552)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (151, N'Erongo', 1, 1, 0, 2553)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (151, N'Hardap', 1, 1, 0, 2554)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (151, N'Karas', 1, 1, 0, 2555)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (151, N'Kavango', 1, 1, 0, 2556)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (151, N'Khomas', 1, 1, 0, 2557)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (151, N'Kunene', 1, 1, 0, 2558)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (151, N'Ohangwena', 1, 1, 0, 2559)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (151, N'Omaheke', 1, 1, 0, 2560)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (151, N'Omusati', 1, 1, 0, 2561)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (151, N'Oshana', 1, 1, 0, 2562)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (151, N'Oshikoto', 1, 1, 0, 2563)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (151, N'Otjozondjupa', 1, 1, 0, 2564)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (152, N'Yaren', 1, 1, 0, 2565)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (153, N'Bagmati', 1, 1, 0, 2566)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (153, N'Bheri', 1, 1, 0, 2567)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (153, N'Dhawalagiri', 1, 1, 0, 2568)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (153, N'Gandaki', 1, 1, 0, 2569)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (153, N'Janakpur', 1, 1, 0, 2570)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (153, N'Karnali', 1, 1, 0, 2571)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (153, N'Koshi', 1, 1, 0, 2572)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (153, N'Lumbini', 1, 1, 0, 2573)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (153, N'Mahakali', 1, 1, 0, 2574)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (153, N'Mechi', 1, 1, 0, 2575)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (153, N'Narayani', 1, 1, 0, 2576)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (153, N'Rapti', 1, 1, 0, 2577)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (153, N'Sagarmatha', 1, 1, 0, 2578)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (153, N'Seti', 1, 1, 0, 2579)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (154, N'Bonaire', 1, 1, 0, 2580)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (154, N'Curacao', 1, 1, 0, 2581)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (154, N'Saba', 1, 1, 0, 2582)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (154, N'Sint Eustatius', 1, 1, 0, 2583)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (154, N'Sint Maarten', 1, 1, 0, 2584)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (155, N'Amsterdam', 1, 1, 0, 2585)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (155, N'Benelux', 1, 1, 0, 2586)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (155, N'Drenthe', 1, 1, 0, 2587)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (155, N'Flevoland', 1, 1, 0, 2588)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (155, N'Friesland', 1, 1, 0, 2589)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (155, N'Gelderland', 1, 1, 0, 2590)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (155, N'Groningen', 1, 1, 0, 2591)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (155, N'Limburg', 1, 1, 0, 2592)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (155, N'Noord-Brabant', 1, 1, 0, 2593)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (155, N'Noord-Holland', 1, 1, 0, 2594)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (155, N'Overijssel', 1, 1, 0, 2595)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (155, N'South Holland', 1, 1, 0, 2596)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (155, N'Utrecht', 1, 1, 0, 2597)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (155, N'Zeeland', 1, 1, 0, 2598)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (155, N'Zuid-Holland', 1, 1, 0, 2599)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (156, N'Iles', 1, 1, 0, 2600)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (156, N'Nord', 1, 1, 0, 2601)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (156, N'Sud', 1, 1, 0, 2602)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (157, N'Area Outside Region', 1, 1, 0, 2603)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (157, N'Auckland', 1, 1, 0, 2604)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (157, N'Bay of Plenty', 1, 1, 0, 2605)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (157, N'Canterbury', 1, 1, 0, 2606)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (157, N'Christchurch', 1, 1, 0, 2607)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (157, N'Gisborne', 1, 1, 0, 2608)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (157, N'Hawke''''s Bay', 1, 1, 0, 2609)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (157, N'Manawatu-Wanganui', 1, 1, 0, 2610)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (157, N'Marlborough', 1, 1, 0, 2611)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (157, N'Nelson', 1, 1, 0, 2612)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (157, N'Northland', 1, 1, 0, 2613)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (157, N'Otago', 1, 1, 0, 2614)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (157, N'Rodney', 1, 1, 0, 2615)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (157, N'Southland', 1, 1, 0, 2616)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (157, N'Taranaki', 1, 1, 0, 2617)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (157, N'Tasman', 1, 1, 0, 2618)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (157, N'Waikato', 1, 1, 0, 2619)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (157, N'Wellington', 1, 1, 0, 2620)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (157, N'West Coast', 1, 1, 0, 2621)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (158, N'Atlantico Norte', 1, 1, 0, 2622)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (158, N'Atlantico Sur', 1, 1, 0, 2623)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (158, N'Boaco', 1, 1, 0, 2624)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (158, N'Carazo', 1, 1, 0, 2625)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (158, N'Chinandega', 1, 1, 0, 2626)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (158, N'Chontales', 1, 1, 0, 2627)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (158, N'Esteli', 1, 1, 0, 2628)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (158, N'Granada', 1, 1, 0, 2629)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (158, N'Jinotega', 1, 1, 0, 2630)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (158, N'Leon', 1, 1, 0, 2631)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (158, N'Madriz', 1, 1, 0, 2632)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (158, N'Managua', 1, 1, 0, 2633)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (158, N'Masaya', 1, 1, 0, 2634)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (158, N'Matagalpa', 1, 1, 0, 2635)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (158, N'Nueva Segovia', 1, 1, 0, 2636)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (158, N'Rio San Juan', 1, 1, 0, 2637)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (158, N'Rivas', 1, 1, 0, 2638)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (159, N'Agadez', 1, 1, 0, 2639)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (159, N'Diffa', 1, 1, 0, 2640)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (159, N'Dosso', 1, 1, 0, 2641)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (159, N'Maradi', 1, 1, 0, 2642)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (159, N'Niamey', 1, 1, 0, 2643)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (159, N'Tahoua', 1, 1, 0, 2644)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (159, N'Tillabery', 1, 1, 0, 2645)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (159, N'Zinder', 1, 1, 0, 2646)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (160, N'Abia', 1, 1, 0, 2647)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (160, N'Abuja Federal Capital Territor', 1, 1, 0, 2648)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (160, N'Adamawa', 1, 1, 0, 2649)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (160, N'Akwa Ibom', 1, 1, 0, 2650)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (160, N'Anambra', 1, 1, 0, 2651)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (160, N'Bauchi', 1, 1, 0, 2652)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (160, N'Bayelsa', 1, 1, 0, 2653)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (160, N'Benue', 1, 1, 0, 2654)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (160, N'Borno', 1, 1, 0, 2655)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (160, N'Cross River', 1, 1, 0, 2656)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (160, N'Delta', 1, 1, 0, 2657)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (160, N'Ebonyi', 1, 1, 0, 2658)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (160, N'Edo', 1, 1, 0, 2659)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (160, N'Ekiti', 1, 1, 0, 2660)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (160, N'Enugu', 1, 1, 0, 2661)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (160, N'Gombe', 1, 1, 0, 2662)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (160, N'Imo', 1, 1, 0, 2663)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (160, N'Jigawa', 1, 1, 0, 2664)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (160, N'Kaduna', 1, 1, 0, 2665)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (160, N'Kano', 1, 1, 0, 2666)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (160, N'Katsina', 1, 1, 0, 2667)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (160, N'Kebbi', 1, 1, 0, 2668)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (160, N'Kogi', 1, 1, 0, 2669)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (160, N'Kwara', 1, 1, 0, 2670)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (160, N'Lagos', 1, 1, 0, 2671)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (160, N'Nassarawa', 1, 1, 0, 2672)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (160, N'Niger', 1, 1, 0, 2673)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (160, N'Ogun', 1, 1, 0, 2674)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (160, N'Ondo', 1, 1, 0, 2675)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (160, N'Osun', 1, 1, 0, 2676)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (160, N'Oyo', 1, 1, 0, 2677)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (160, N'Plateau', 1, 1, 0, 2678)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (160, N'Rivers', 1, 1, 0, 2679)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (160, N'Sokoto', 1, 1, 0, 2680)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (160, N'Taraba', 1, 1, 0, 2681)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (160, N'Yobe', 1, 1, 0, 2682)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (160, N'Zamfara', 1, 1, 0, 2683)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (161, N'Niue', 1, 1, 0, 2684)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (162, N'Norfolk Island', 1, 1, 0, 2685)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (163, N'Northern Islands', 1, 1, 0, 2686)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (163, N'Rota', 1, 1, 0, 2687)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (163, N'Saipan', 1, 1, 0, 2688)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (163, N'Tinian', 1, 1, 0, 2689)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (164, N'Akershus', 1, 1, 0, 2690)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (164, N'Aust Agder', 1, 1, 0, 2691)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (164, N'Bergen', 1, 1, 0, 2692)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (164, N'Buskerud', 1, 1, 0, 2693)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (164, N'Finnmark', 1, 1, 0, 2694)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (164, N'Hedmark', 1, 1, 0, 2695)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (164, N'Hordaland', 1, 1, 0, 2696)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (164, N'Moere og Romsdal', 1, 1, 0, 2697)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (164, N'Nord Trondelag', 1, 1, 0, 2698)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (164, N'Nordland', 1, 1, 0, 2699)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (164, N'Oestfold', 1, 1, 0, 2700)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (164, N'Oppland', 1, 1, 0, 2701)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (164, N'Oslo', 1, 1, 0, 2702)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (164, N'Rogaland', 1, 1, 0, 2703)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (164, N'Soer Troendelag', 1, 1, 0, 2704)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (164, N'Sogn og Fjordane', 1, 1, 0, 2705)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (164, N'Stavern', 1, 1, 0, 2706)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (164, N'Sykkylven', 1, 1, 0, 2707)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (164, N'Telemark', 1, 1, 0, 2708)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (164, N'Troms', 1, 1, 0, 2709)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (164, N'Vest Agder', 1, 1, 0, 2710)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (164, N'Vestfold', 1, 1, 0, 2711)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (164, N'ÃƒÂ˜stfold', 1, 1, 0, 2712)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (165, N'Al Buraimi', 1, 1, 0, 2713)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (165, N'Dhufar', 1, 1, 0, 2714)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (165, N'Masqat', 1, 1, 0, 2715)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (165, N'Musandam', 1, 1, 0, 2716)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (165, N'Rusayl', 1, 1, 0, 2717)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (165, N'Wadi Kabir', 1, 1, 0, 2718)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (165, N'ad-Dakhiliyah', 1, 1, 0, 2719)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (165, N'adh-Dhahirah', 1, 1, 0, 2720)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (165, N'al-Batinah', 1, 1, 0, 2721)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (165, N'ash-Sharqiyah', 1, 1, 0, 2722)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (166, N'Baluchistan', 1, 1, 0, 2723)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (166, N'Federal Capital Area', 1, 1, 0, 2724)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (166, N'Federally administered Tribal', 1, 1, 0, 2725)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (166, N'North-West Frontier', 1, 1, 0, 2726)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (166, N'Northern Areas', 1, 1, 0, 2727)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (166, N'Punjab', 1, 1, 0, 2728)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (166, N'Sind', 1, 1, 0, 2729)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (167, N'Aimeliik', 1, 1, 0, 2730)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (167, N'Airai', 1, 1, 0, 2731)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (167, N'Angaur', 1, 1, 0, 2732)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (167, N'Hatobohei', 1, 1, 0, 2733)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (167, N'Kayangel', 1, 1, 0, 2734)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (167, N'Koror', 1, 1, 0, 2735)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (167, N'Melekeok', 1, 1, 0, 2736)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (167, N'Ngaraard', 1, 1, 0, 2737)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (167, N'Ngardmau', 1, 1, 0, 2738)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (167, N'Ngaremlengui', 1, 1, 0, 2739)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (167, N'Ngatpang', 1, 1, 0, 2740)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (167, N'Ngchesar', 1, 1, 0, 2741)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (167, N'Ngerchelong', 1, 1, 0, 2742)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (167, N'Ngiwal', 1, 1, 0, 2743)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (167, N'Peleliu', 1, 1, 0, 2744)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (167, N'Sonsorol', 1, 1, 0, 2745)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (168, N'Ariha', 1, 1, 0, 2746)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (168, N'Bayt Lahm', 1, 1, 0, 2747)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (168, N'Bethlehem', 1, 1, 0, 2748)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (168, N'Dayr-al-Balah', 1, 1, 0, 2749)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (168, N'Ghazzah', 1, 1, 0, 2750)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (168, N'Ghazzah ash-Shamaliyah', 1, 1, 0, 2751)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (168, N'Janin', 1, 1, 0, 2752)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (168, N'Khan Yunis', 1, 1, 0, 2753)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (168, N'Nabulus', 1, 1, 0, 2754)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (168, N'Qalqilyah', 1, 1, 0, 2755)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (168, N'Rafah', 1, 1, 0, 2756)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (168, N'Ram Allah wal-Birah', 1, 1, 0, 2757)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (168, N'Salfit', 1, 1, 0, 2758)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (168, N'Tubas', 1, 1, 0, 2759)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (168, N'Tulkarm', 1, 1, 0, 2760)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (168, N'al-Khalil', 1, 1, 0, 2761)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (168, N'al-Quds', 1, 1, 0, 2762)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (169, N'Bocas del Toro', 1, 1, 0, 2763)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (169, N'Chiriqui', 1, 1, 0, 2764)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (169, N'Cocle', 1, 1, 0, 2765)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (169, N'Colon', 1, 1, 0, 2766)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (169, N'Darien', 1, 1, 0, 2767)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (169, N'Embera', 1, 1, 0, 2768)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (169, N'Herrera', 1, 1, 0, 2769)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (169, N'Kuna Yala', 1, 1, 0, 2770)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (169, N'Los Santos', 1, 1, 0, 2771)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (169, N'Ngobe Bugle', 1, 1, 0, 2772)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (169, N'Panama', 1, 1, 0, 2773)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (169, N'Veraguas', 1, 1, 0, 2774)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (170, N'East New Britain', 1, 1, 0, 2775)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (170, N'East Sepik', 1, 1, 0, 2776)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (170, N'Eastern Highlands', 1, 1, 0, 2777)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (170, N'Enga', 1, 1, 0, 2778)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (170, N'Fly River', 1, 1, 0, 2779)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (170, N'Gulf', 1, 1, 0, 2780)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (170, N'Madang', 1, 1, 0, 2781)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (170, N'Manus', 1, 1, 0, 2782)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (170, N'Milne Bay', 1, 1, 0, 2783)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (170, N'Morobe', 1, 1, 0, 2784)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (170, N'National Capital District', 1, 1, 0, 2785)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (170, N'New Ireland', 1, 1, 0, 2786)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (170, N'North Solomons', 1, 1, 0, 2787)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (170, N'Oro', 1, 1, 0, 2788)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (170, N'Sandaun', 1, 1, 0, 2789)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (170, N'Simbu', 1, 1, 0, 2790)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (170, N'Southern Highlands', 1, 1, 0, 2791)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (170, N'West New Britain', 1, 1, 0, 2792)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (170, N'Western Highlands', 1, 1, 0, 2793)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (171, N'Alto Paraguay', 1, 1, 0, 2794)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (171, N'Alto Parana', 1, 1, 0, 2795)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (171, N'Amambay', 1, 1, 0, 2796)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (171, N'Asuncion', 1, 1, 0, 2797)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (171, N'Boqueron', 1, 1, 0, 2798)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (171, N'Caaguazu', 1, 1, 0, 2799)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (171, N'Caazapa', 1, 1, 0, 2800)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (171, N'Canendiyu', 1, 1, 0, 2801)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (171, N'Central', 1, 1, 0, 2802)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (171, N'Concepcion', 1, 1, 0, 2803)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (171, N'Cordillera', 1, 1, 0, 2804)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (171, N'Guaira', 1, 1, 0, 2805)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (171, N'Itapua', 1, 1, 0, 2806)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (171, N'Misiones', 1, 1, 0, 2807)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (171, N'Neembucu', 1, 1, 0, 2808)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (171, N'Paraguari', 1, 1, 0, 2809)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (171, N'Presidente Hayes', 1, 1, 0, 2810)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (171, N'San Pedro', 1, 1, 0, 2811)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (172, N'Amazonas', 1, 1, 0, 2812)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (172, N'Ancash', 1, 1, 0, 2813)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (172, N'Apurimac', 1, 1, 0, 2814)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (172, N'Arequipa', 1, 1, 0, 2815)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (172, N'Ayacucho', 1, 1, 0, 2816)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (172, N'Cajamarca', 1, 1, 0, 2817)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (172, N'Cusco', 1, 1, 0, 2818)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (172, N'Huancavelica', 1, 1, 0, 2819)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (172, N'Huanuco', 1, 1, 0, 2820)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (172, N'Ica', 1, 1, 0, 2821)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (172, N'Junin', 1, 1, 0, 2822)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (172, N'La Libertad', 1, 1, 0, 2823)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (172, N'Lambayeque', 1, 1, 0, 2824)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (172, N'Lima y Callao', 1, 1, 0, 2825)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (172, N'Loreto', 1, 1, 0, 2826)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (172, N'Madre de Dios', 1, 1, 0, 2827)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (172, N'Moquegua', 1, 1, 0, 2828)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (172, N'Pasco', 1, 1, 0, 2829)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (172, N'Piura', 1, 1, 0, 2830)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (172, N'Puno', 1, 1, 0, 2831)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (172, N'San Martin', 1, 1, 0, 2832)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (172, N'Tacna', 1, 1, 0, 2833)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (172, N'Tumbes', 1, 1, 0, 2834)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (172, N'Ucayali', 1, 1, 0, 2835)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (173, N'Batangas', 1, 1, 0, 2836)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (173, N'Bicol', 1, 1, 0, 2837)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (173, N'Bulacan', 1, 1, 0, 2838)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (173, N'Cagayan', 1, 1, 0, 2839)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (173, N'Caraga', 1, 1, 0, 2840)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (173, N'Central Luzon', 1, 1, 0, 2841)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (173, N'Central Mindanao', 1, 1, 0, 2842)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (173, N'Central Visayas', 1, 1, 0, 2843)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (173, N'Cordillera', 1, 1, 0, 2844)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (173, N'Davao', 1, 1, 0, 2845)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (173, N'Eastern Visayas', 1, 1, 0, 2846)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (173, N'Greater Metropolitan Area', 1, 1, 0, 2847)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (173, N'Ilocos', 1, 1, 0, 2848)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (173, N'Laguna', 1, 1, 0, 2849)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (173, N'Luzon', 1, 1, 0, 2850)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (173, N'Mactan', 1, 1, 0, 2851)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (173, N'Metropolitan Manila Area', 1, 1, 0, 2852)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (173, N'Muslim Mindanao', 1, 1, 0, 2853)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (173, N'Northern Mindanao', 1, 1, 0, 2854)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (173, N'Southern Mindanao', 1, 1, 0, 2855)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (173, N'Southern Tagalog', 1, 1, 0, 2856)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (173, N'Western Mindanao', 1, 1, 0, 2857)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (173, N'Western Visayas', 1, 1, 0, 2858)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (174, N'Pitcairn Island', 1, 1, 0, 2859)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (175, N'Biale Blota', 1, 1, 0, 2860)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (175, N'Dobroszyce', 1, 1, 0, 2861)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (175, N'Dolnoslaskie', 1, 1, 0, 2862)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (175, N'Dziekanow Lesny', 1, 1, 0, 2863)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (175, N'Hopowo', 1, 1, 0, 2864)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (175, N'Kartuzy', 1, 1, 0, 2865)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (175, N'Koscian', 1, 1, 0, 2866)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (175, N'Krakow', 1, 1, 0, 2867)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (175, N'Kujawsko-Pomorskie', 1, 1, 0, 2868)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (175, N'Lodzkie', 1, 1, 0, 2869)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (175, N'Lubelskie', 1, 1, 0, 2870)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (175, N'Lubuskie', 1, 1, 0, 2871)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (175, N'Malomice', 1, 1, 0, 2872)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (175, N'Malopolskie', 1, 1, 0, 2873)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (175, N'Mazowieckie', 1, 1, 0, 2874)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (175, N'Mirkow', 1, 1, 0, 2875)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (175, N'Opolskie', 1, 1, 0, 2876)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (175, N'Ostrowiec', 1, 1, 0, 2877)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (175, N'Podkarpackie', 1, 1, 0, 2878)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (175, N'Podlaskie', 1, 1, 0, 2879)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (175, N'Polska', 1, 1, 0, 2880)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (175, N'Pomorskie', 1, 1, 0, 2881)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (175, N'Poznan', 1, 1, 0, 2882)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (175, N'Pruszkow', 1, 1, 0, 2883)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (175, N'Rymanowska', 1, 1, 0, 2884)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (175, N'Rzeszow', 1, 1, 0, 2885)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (175, N'Slaskie', 1, 1, 0, 2886)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (175, N'Stare Pole', 1, 1, 0, 2887)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (175, N'Swietokrzyskie', 1, 1, 0, 2888)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (175, N'Warminsko-Mazurskie', 1, 1, 0, 2889)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (175, N'Warsaw', 1, 1, 0, 2890)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (175, N'Wejherowo', 1, 1, 0, 2891)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (175, N'Wielkopolskie', 1, 1, 0, 2892)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (175, N'Wroclaw', 1, 1, 0, 2893)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (175, N'Zachodnio-Pomorskie', 1, 1, 0, 2894)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (175, N'Zukowo', 1, 1, 0, 2895)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (176, N'Abrantes', 1, 1, 0, 2896)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (176, N'Acores', 1, 1, 0, 2897)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (176, N'Alentejo', 1, 1, 0, 2898)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (176, N'Algarve', 1, 1, 0, 2899)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (176, N'Braga', 1, 1, 0, 2900)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (176, N'Centro', 1, 1, 0, 2901)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (176, N'Distrito de Leiria', 1, 1, 0, 2902)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (176, N'Distrito de Viana do Castelo', 1, 1, 0, 2903)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (176, N'Distrito de Vila Real', 1, 1, 0, 2904)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (176, N'Distrito do Porto', 1, 1, 0, 2905)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (176, N'Lisboa e Vale do Tejo', 1, 1, 0, 2906)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (176, N'Madeira', 1, 1, 0, 2907)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (176, N'Norte', 1, 1, 0, 2908)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (176, N'Paivas', 1, 1, 0, 2909)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (177, N'Arecibo', 1, 1, 0, 2910)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (177, N'Bayamon', 1, 1, 0, 2911)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (177, N'Carolina', 1, 1, 0, 2912)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (177, N'Florida', 1, 1, 0, 2913)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (177, N'Guayama', 1, 1, 0, 2914)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (177, N'Humacao', 1, 1, 0, 2915)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (177, N'Mayaguez-Aguadilla', 1, 1, 0, 2916)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (177, N'Ponce', 1, 1, 0, 2917)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (177, N'Salinas', 1, 1, 0, 2918)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (177, N'San Juan', 1, 1, 0, 2919)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (178, N'Doha', 1, 1, 0, 2920)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (178, N'Jarian-al-Batnah', 1, 1, 0, 2921)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (178, N'Umm Salal', 1, 1, 0, 2922)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (178, N'ad-Dawhah', 1, 1, 0, 2923)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (178, N'al-Ghuwayriyah', 1, 1, 0, 2924)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (178, N'al-Jumayliyah', 1, 1, 0, 2925)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (178, N'al-Khawr', 1, 1, 0, 2926)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (178, N'al-Wakrah', 1, 1, 0, 2927)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (178, N'ar-Rayyan', 1, 1, 0, 2928)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (178, N'ash-Shamal', 1, 1, 0, 2929)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (179, N'Saint-Benoit', 1, 1, 0, 2930)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (179, N'Saint-Denis', 1, 1, 0, 2931)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (179, N'Saint-Paul', 1, 1, 0, 2932)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (179, N'Saint-Pierre', 1, 1, 0, 2933)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (180, N'Alba', 1, 1, 0, 2934)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (180, N'Arad', 1, 1, 0, 2935)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (180, N'Arges', 1, 1, 0, 2936)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (180, N'Bacau', 1, 1, 0, 2937)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (180, N'Bihor', 1, 1, 0, 2938)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (180, N'Bistrita-Nasaud', 1, 1, 0, 2939)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (180, N'Botosani', 1, 1, 0, 2940)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (180, N'Braila', 1, 1, 0, 2941)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (180, N'Brasov', 1, 1, 0, 2942)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (180, N'Bucuresti', 1, 1, 0, 2943)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (180, N'Buzau', 1, 1, 0, 2944)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (180, N'Calarasi', 1, 1, 0, 2945)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (180, N'Caras-Severin', 1, 1, 0, 2946)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (180, N'Cluj', 1, 1, 0, 2947)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (180, N'Constanta', 1, 1, 0, 2948)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (180, N'Covasna', 1, 1, 0, 2949)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (180, N'Dambovita', 1, 1, 0, 2950)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (180, N'Dolj', 1, 1, 0, 2951)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (180, N'Galati', 1, 1, 0, 2952)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (180, N'Giurgiu', 1, 1, 0, 2953)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (180, N'Gorj', 1, 1, 0, 2954)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (180, N'Harghita', 1, 1, 0, 2955)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (180, N'Hunedoara', 1, 1, 0, 2956)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (180, N'Ialomita', 1, 1, 0, 2957)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (180, N'Iasi', 1, 1, 0, 2958)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (180, N'Ilfov', 1, 1, 0, 2959)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (180, N'Maramures', 1, 1, 0, 2960)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (180, N'Mehedinti', 1, 1, 0, 2961)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (180, N'Mures', 1, 1, 0, 2962)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (180, N'Neamt', 1, 1, 0, 2963)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (180, N'Olt', 1, 1, 0, 2964)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (180, N'Prahova', 1, 1, 0, 2965)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (180, N'Salaj', 1, 1, 0, 2966)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (180, N'Satu Mare', 1, 1, 0, 2967)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (180, N'Sibiu', 1, 1, 0, 2968)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (180, N'Sondelor', 1, 1, 0, 2969)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (180, N'Suceava', 1, 1, 0, 2970)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (180, N'Teleorman', 1, 1, 0, 2971)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (180, N'Timis', 1, 1, 0, 2972)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (180, N'Tulcea', 1, 1, 0, 2973)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (180, N'Valcea', 1, 1, 0, 2974)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (180, N'Vaslui', 1, 1, 0, 2975)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (180, N'Vrancea', 1, 1, 0, 2976)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Adygeja', 1, 1, 0, 2977)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Aga', 1, 1, 0, 2978)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Alanija', 1, 1, 0, 2979)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Altaj', 1, 1, 0, 2980)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Amur', 1, 1, 0, 2981)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Arhangelsk', 1, 1, 0, 2982)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Astrahan', 1, 1, 0, 2983)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Bashkortostan', 1, 1, 0, 2984)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Belgorod', 1, 1, 0, 2985)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Brjansk', 1, 1, 0, 2986)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Burjatija', 1, 1, 0, 2987)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Chechenija', 1, 1, 0, 2988)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Cheljabinsk', 1, 1, 0, 2989)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Chita', 1, 1, 0, 2990)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Chukotka', 1, 1, 0, 2991)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Chuvashija', 1, 1, 0, 2992)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Dagestan', 1, 1, 0, 2993)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Evenkija', 1, 1, 0, 2994)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Gorno-Altaj', 1, 1, 0, 2995)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Habarovsk', 1, 1, 0, 2996)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Hakasija', 1, 1, 0, 2997)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Hanty-Mansija', 1, 1, 0, 2998)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Ingusetija', 1, 1, 0, 2999)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Irkutsk', 1, 1, 0, 3000)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Ivanovo', 1, 1, 0, 3001)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Jamalo-Nenets', 1, 1, 0, 3002)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Jaroslavl', 1, 1, 0, 3003)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Jevrej', 1, 1, 0, 3004)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Kabardino-Balkarija', 1, 1, 0, 3005)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Kaliningrad', 1, 1, 0, 3006)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Kalmykija', 1, 1, 0, 3007)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Kaluga', 1, 1, 0, 3008)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Kamchatka', 1, 1, 0, 3009)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Karachaj-Cherkessija', 1, 1, 0, 3010)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Karelija', 1, 1, 0, 3011)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Kemerovo', 1, 1, 0, 3012)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Khabarovskiy Kray', 1, 1, 0, 3013)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Kirov', 1, 1, 0, 3014)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Komi', 1, 1, 0, 3015)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Komi-Permjakija', 1, 1, 0, 3016)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Korjakija', 1, 1, 0, 3017)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Kostroma', 1, 1, 0, 3018)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Krasnodar', 1, 1, 0, 3019)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Krasnojarsk', 1, 1, 0, 3020)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Krasnoyarskiy Kray', 1, 1, 0, 3021)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Kurgan', 1, 1, 0, 3022)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Kursk', 1, 1, 0, 3023)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Leningrad', 1, 1, 0, 3024)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Lipeck', 1, 1, 0, 3025)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Magadan', 1, 1, 0, 3026)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Marij El', 1, 1, 0, 3027)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Mordovija', 1, 1, 0, 3028)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Moscow', 1, 1, 0, 3029)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Moskovskaja Oblast', 1, 1, 0, 3030)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Moskovskaya Oblast', 1, 1, 0, 3031)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Moskva', 1, 1, 0, 3032)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Murmansk', 1, 1, 0, 3033)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Nenets', 1, 1, 0, 3034)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Nizhnij Novgorod', 1, 1, 0, 3035)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Novgorod', 1, 1, 0, 3036)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Novokusnezk', 1, 1, 0, 3037)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Novosibirsk', 1, 1, 0, 3038)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Omsk', 1, 1, 0, 3039)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Orenburg', 1, 1, 0, 3040)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Orjol', 1, 1, 0, 3041)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Penza', 1, 1, 0, 3042)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Perm', 1, 1, 0, 3043)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Primorje', 1, 1, 0, 3044)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Pskov', 1, 1, 0, 3045)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Pskovskaya Oblast', 1, 1, 0, 3046)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Rjazan', 1, 1, 0, 3047)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Rostov', 1, 1, 0, 3048)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Saha', 1, 1, 0, 3049)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Sahalin', 1, 1, 0, 3050)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Samara', 1, 1, 0, 3051)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Samarskaya', 1, 1, 0, 3052)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Sankt-Peterburg', 1, 1, 0, 3053)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Saratov', 1, 1, 0, 3054)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Smolensk', 1, 1, 0, 3055)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Stavropol', 1, 1, 0, 3056)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Sverdlovsk', 1, 1, 0, 3057)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Tajmyrija', 1, 1, 0, 3058)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Tambov', 1, 1, 0, 3059)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Tatarstan', 1, 1, 0, 3060)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Tjumen', 1, 1, 0, 3061)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Tomsk', 1, 1, 0, 3062)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Tula', 1, 1, 0, 3063)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Tver', 1, 1, 0, 3064)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Tyva', 1, 1, 0, 3065)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Udmurtija', 1, 1, 0, 3066)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Uljanovsk', 1, 1, 0, 3067)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Ulyanovskaya Oblast', 1, 1, 0, 3068)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Ust-Orda', 1, 1, 0, 3069)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Vladimir', 1, 1, 0, 3070)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Volgograd', 1, 1, 0, 3071)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Vologda', 1, 1, 0, 3072)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (181, N'Voronezh', 1, 1, 0, 3073)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (182, N'Butare', 1, 1, 0, 3074)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (182, N'Byumba', 1, 1, 0, 3075)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (182, N'Cyangugu', 1, 1, 0, 3076)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (182, N'Gikongoro', 1, 1, 0, 3077)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (182, N'Gisenyi', 1, 1, 0, 3078)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (182, N'Gitarama', 1, 1, 0, 3079)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (182, N'Kibungo', 1, 1, 0, 3080)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (182, N'Kibuye', 1, 1, 0, 3081)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (182, N'Kigali-ngali', 1, 1, 0, 3082)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (182, N'Ruhengeri', 1, 1, 0, 3083)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (183, N'Ascension', 1, 1, 0, 3084)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (183, N'Gough Island', 1, 1, 0, 3085)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (183, N'Saint Helena', 1, 1, 0, 3086)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (183, N'Tristan da Cunha', 1, 1, 0, 3087)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (184, N'Christ Church Nichola Town', 1, 1, 0, 3088)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (184, N'Saint Anne Sandy Point', 1, 1, 0, 3089)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (184, N'Saint George Basseterre', 1, 1, 0, 3090)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (184, N'Saint George Gingerland', 1, 1, 0, 3091)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (184, N'Saint James Windward', 1, 1, 0, 3092)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (184, N'Saint John Capesterre', 1, 1, 0, 3093)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (184, N'Saint John Figtree', 1, 1, 0, 3094)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (184, N'Saint Mary Cayon', 1, 1, 0, 3095)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (184, N'Saint Paul Capesterre', 1, 1, 0, 3096)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (184, N'Saint Paul Charlestown', 1, 1, 0, 3097)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (184, N'Saint Peter Basseterre', 1, 1, 0, 3098)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (184, N'Saint Thomas Lowland', 1, 1, 0, 3099)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (184, N'Saint Thomas Middle Island', 1, 1, 0, 3100)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (184, N'Trinity Palmetto Point', 1, 1, 0, 3101)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (185, N'Anse-la-Raye', 1, 1, 0, 3102)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (185, N'Canaries', 1, 1, 0, 3103)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (185, N'Castries', 1, 1, 0, 3104)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (185, N'Choiseul', 1, 1, 0, 3105)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (185, N'Dennery', 1, 1, 0, 3106)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (185, N'Gros Inlet', 1, 1, 0, 3107)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (185, N'Laborie', 1, 1, 0, 3108)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (185, N'Micoud', 1, 1, 0, 3109)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (185, N'Soufriere', 1, 1, 0, 3110)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (185, N'Vieux Fort', 1, 1, 0, 3111)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (186, N'Miquelon-Langlade', 1, 1, 0, 3112)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (186, N'Saint-Pierre', 1, 1, 0, 3113)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (187, N'Charlotte', 1, 1, 0, 3114)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (187, N'Grenadines', 1, 1, 0, 3115)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (187, N'Saint Andrew', 1, 1, 0, 3116)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (187, N'Saint David', 1, 1, 0, 3117)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (187, N'Saint George', 1, 1, 0, 3118)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (187, N'Saint Patrick', 1, 1, 0, 3119)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (188, N'A''''ana', 1, 1, 0, 3120)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (188, N'Aiga-i-le-Tai', 1, 1, 0, 3121)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (188, N'Atua', 1, 1, 0, 3122)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (188, N'Fa''''asaleleaga', 1, 1, 0, 3123)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (188, N'Gaga''''emauga', 1, 1, 0, 3124)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (188, N'Gagaifomauga', 1, 1, 0, 3125)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (188, N'Palauli', 1, 1, 0, 3126)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (188, N'Satupa''''itea', 1, 1, 0, 3127)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (188, N'Tuamasaga', 1, 1, 0, 3128)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (188, N'Va''''a-o-Fonoti', 1, 1, 0, 3129)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (188, N'Vaisigano', 1, 1, 0, 3130)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (189, N'Acquaviva', 1, 1, 0, 3131)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (189, N'Borgo Maggiore', 1, 1, 0, 3132)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (189, N'Chiesanuova', 1, 1, 0, 3133)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (189, N'Domagnano', 1, 1, 0, 3134)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (189, N'Faetano', 1, 1, 0, 3135)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (189, N'Fiorentino', 1, 1, 0, 3136)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (189, N'Montegiardino', 1, 1, 0, 3137)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (189, N'San Marino', 1, 1, 0, 3138)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (189, N'Serravalle', 1, 1, 0, 3139)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (190, N'Agua Grande', 1, 1, 0, 3140)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (190, N'Cantagalo', 1, 1, 0, 3141)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (190, N'Lemba', 1, 1, 0, 3142)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (190, N'Lobata', 1, 1, 0, 3143)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (190, N'Me-Zochi', 1, 1, 0, 3144)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (190, N'Pague', 1, 1, 0, 3145)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (191, N'Al Khobar', 1, 1, 0, 3146)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (191, N'Aseer', 1, 1, 0, 3147)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (191, N'Ash Sharqiyah', 1, 1, 0, 3148)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (191, N'Asir', 1, 1, 0, 3149)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (191, N'Central Province', 1, 1, 0, 3150)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (191, N'Eastern Province', 1, 1, 0, 3151)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (191, N'Ha''''il', 1, 1, 0, 3152)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (191, N'Jawf', 1, 1, 0, 3153)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (191, N'Jizan', 1, 1, 0, 3154)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (191, N'Makkah', 1, 1, 0, 3155)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (191, N'Najran', 1, 1, 0, 3156)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (191, N'Qasim', 1, 1, 0, 3157)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (191, N'Tabuk', 1, 1, 0, 3158)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (191, N'Western Province', 1, 1, 0, 3159)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (191, N'al-Bahah', 1, 1, 0, 3160)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (191, N'al-Hudud-ash-Shamaliyah', 1, 1, 0, 3161)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (191, N'al-Madinah', 1, 1, 0, 3162)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (191, N'ar-Riyad', 1, 1, 0, 3163)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (192, N'Dakar', 1, 1, 0, 3164)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (192, N'Diourbel', 1, 1, 0, 3165)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (192, N'Fatick', 1, 1, 0, 3166)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (192, N'Kaolack', 1, 1, 0, 3167)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (192, N'Kolda', 1, 1, 0, 3168)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (192, N'Louga', 1, 1, 0, 3169)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (192, N'Saint-Louis', 1, 1, 0, 3170)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (192, N'Tambacounda', 1, 1, 0, 3171)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (192, N'Thies', 1, 1, 0, 3172)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (192, N'Ziguinchor', 1, 1, 0, 3173)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (193, N'Central Serbia', 1, 1, 0, 3174)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (193, N'Kosovo and Metohija', 1, 1, 0, 3175)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (193, N'Vojvodina', 1, 1, 0, 3176)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (194, N'Anse Boileau', 1, 1, 0, 3177)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (194, N'Anse Royale', 1, 1, 0, 3178)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (194, N'Cascade', 1, 1, 0, 3179)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (194, N'Takamaka', 1, 1, 0, 3180)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (194, N'Victoria', 1, 1, 0, 3181)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (195, N'Eastern', 1, 1, 0, 3182)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (195, N'Northern', 1, 1, 0, 3183)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (195, N'Southern', 1, 1, 0, 3184)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (195, N'Western', 1, 1, 0, 3185)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (196, N'Singapore', 1, 1, 0, 3186)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (197, N'Banskobystricky', 1, 1, 0, 3187)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (197, N'Bratislavsky', 1, 1, 0, 3188)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (197, N'Kosicky', 1, 1, 0, 3189)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (197, N'Nitriansky', 1, 1, 0, 3190)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (197, N'Presovsky', 1, 1, 0, 3191)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (197, N'Trenciansky', 1, 1, 0, 3192)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (197, N'Trnavsky', 1, 1, 0, 3193)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (197, N'Zilinsky', 1, 1, 0, 3194)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (198, N'Benedikt', 1, 1, 0, 3195)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (198, N'Gorenjska', 1, 1, 0, 3196)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (198, N'Gorishka', 1, 1, 0, 3197)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (198, N'Jugovzhodna Slovenija', 1, 1, 0, 3198)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (198, N'Koroshka', 1, 1, 0, 3199)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (198, N'Notranjsko-krashka', 1, 1, 0, 3200)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (198, N'Obalno-krashka', 1, 1, 0, 3201)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (198, N'Obcina Domzale', 1, 1, 0, 3202)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (198, N'Obcina Vitanje', 1, 1, 0, 3203)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (198, N'Osrednjeslovenska', 1, 1, 0, 3204)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (198, N'Podravska', 1, 1, 0, 3205)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (198, N'Pomurska', 1, 1, 0, 3206)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (198, N'Savinjska', 1, 1, 0, 3207)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (198, N'Slovenian Littoral', 1, 1, 0, 3208)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (198, N'Spodnjeposavska', 1, 1, 0, 3209)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (198, N'Zasavska', 1, 1, 0, 3210)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (199, N'Pitcairn', 1, 1, 0, 3211)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (200, N'Central', 1, 1, 0, 3212)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (200, N'Choiseul', 1, 1, 0, 3213)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (200, N'Guadalcanal', 1, 1, 0, 3214)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (200, N'Isabel', 1, 1, 0, 3215)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (200, N'Makira and Ulawa', 1, 1, 0, 3216)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (200, N'Malaita', 1, 1, 0, 3217)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (200, N'Rennell and Bellona', 1, 1, 0, 3218)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (200, N'Temotu', 1, 1, 0, 3219)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (200, N'Western', 1, 1, 0, 3220)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (201, N'Awdal', 1, 1, 0, 3221)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (201, N'Bakol', 1, 1, 0, 3222)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (201, N'Banadir', 1, 1, 0, 3223)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (201, N'Bari', 1, 1, 0, 3224)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (201, N'Bay', 1, 1, 0, 3225)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (201, N'Galgudug', 1, 1, 0, 3226)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (201, N'Gedo', 1, 1, 0, 3227)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (201, N'Hiran', 1, 1, 0, 3228)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (201, N'Jubbada Hose', 1, 1, 0, 3229)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (201, N'Jubbadha Dexe', 1, 1, 0, 3230)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (201, N'Mudug', 1, 1, 0, 3231)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (201, N'Nugal', 1, 1, 0, 3232)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (201, N'Sanag', 1, 1, 0, 3233)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (201, N'Shabellaha Dhexe', 1, 1, 0, 3234)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (201, N'Shabellaha Hose', 1, 1, 0, 3235)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (201, N'Togdher', 1, 1, 0, 3236)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (201, N'Woqoyi Galbed', 1, 1, 0, 3237)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (202, N'Eastern Cape', 1, 1, 0, 3238)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (202, N'Free State', 1, 1, 0, 3239)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (202, N'Gauteng', 1, 1, 0, 3240)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (202, N'Kempton Park', 1, 1, 0, 3241)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (202, N'Kramerville', 1, 1, 0, 3242)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (202, N'KwaZulu Natal', 1, 1, 0, 3243)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (202, N'Limpopo', 1, 1, 0, 3244)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (202, N'Mpumalanga', 1, 1, 0, 3245)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (202, N'North West', 1, 1, 0, 3246)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (202, N'Northern Cape', 1, 1, 0, 3247)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (202, N'Parow', 1, 1, 0, 3248)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (202, N'Table View', 1, 1, 0, 3249)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (202, N'Umtentweni', 1, 1, 0, 3250)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (202, N'Western Cape', 1, 1, 0, 3251)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (203, N'South Georgia', 1, 1, 0, 3252)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (204, N'Central Equatoria', 1, 1, 0, 3253)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (205, N'A Coruna', 1, 1, 0, 3254)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (205, N'Alacant', 1, 1, 0, 3255)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (205, N'Alava', 1, 1, 0, 3256)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (205, N'Albacete', 1, 1, 0, 3257)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (205, N'Almeria', 1, 1, 0, 3258)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (205, N'Andalucia', 1, 1, 0, 3259)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (205, N'Asturias', 1, 1, 0, 3260)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (205, N'Avila', 1, 1, 0, 3261)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (205, N'Badajoz', 1, 1, 0, 3262)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (205, N'Balears', 1, 1, 0, 3263)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (205, N'Barcelona', 1, 1, 0, 3264)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (205, N'Bertamirans', 1, 1, 0, 3265)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (205, N'Biscay', 1, 1, 0, 3266)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (205, N'Burgos', 1, 1, 0, 3267)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (205, N'Caceres', 1, 1, 0, 3268)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (205, N'Cadiz', 1, 1, 0, 3269)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (205, N'Cantabria', 1, 1, 0, 3270)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (205, N'Castello', 1, 1, 0, 3271)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (205, N'Catalunya', 1, 1, 0, 3272)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (205, N'Ceuta', 1, 1, 0, 3273)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (205, N'Ciudad Real', 1, 1, 0, 3274)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (205, N'Comunidad Autonoma de Canarias', 1, 1, 0, 3275)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (205, N'Comunidad Autonoma de Cataluna', 1, 1, 0, 3276)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (205, N'Comunidad Autonoma de Galicia', 1, 1, 0, 3277)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (205, N'Comunidad Autonoma de las Isla', 1, 1, 0, 3278)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (205, N'Comunidad Autonoma del Princip', 1, 1, 0, 3279)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (205, N'Comunidad Valenciana', 1, 1, 0, 3280)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (205, N'Cordoba', 1, 1, 0, 3281)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (205, N'Cuenca', 1, 1, 0, 3282)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (205, N'Gipuzkoa', 1, 1, 0, 3283)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (205, N'Girona', 1, 1, 0, 3284)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (205, N'Granada', 1, 1, 0, 3285)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (205, N'Guadalajara', 1, 1, 0, 3286)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (205, N'Guipuzcoa', 1, 1, 0, 3287)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (205, N'Huelva', 1, 1, 0, 3288)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (205, N'Huesca', 1, 1, 0, 3289)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (205, N'Jaen', 1, 1, 0, 3290)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (205, N'La Rioja', 1, 1, 0, 3291)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (205, N'Las Palmas', 1, 1, 0, 3292)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (205, N'Leon', 1, 1, 0, 3293)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (205, N'Lerida', 1, 1, 0, 3294)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (205, N'Lleida', 1, 1, 0, 3295)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (205, N'Lugo', 1, 1, 0, 3296)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (205, N'Madrid', 1, 1, 0, 3297)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (205, N'Malaga', 1, 1, 0, 3298)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (205, N'Melilla', 1, 1, 0, 3299)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (205, N'Murcia', 1, 1, 0, 3300)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (205, N'Navarra', 1, 1, 0, 3301)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (205, N'Ourense', 1, 1, 0, 3302)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (205, N'Pais Vasco', 1, 1, 0, 3303)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (205, N'Palencia', 1, 1, 0, 3304)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (205, N'Pontevedra', 1, 1, 0, 3305)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (205, N'Salamanca', 1, 1, 0, 3306)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (205, N'Santa Cruz de Tenerife', 1, 1, 0, 3307)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (205, N'Segovia', 1, 1, 0, 3308)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (205, N'Sevilla', 1, 1, 0, 3309)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (205, N'Soria', 1, 1, 0, 3310)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (205, N'Tarragona', 1, 1, 0, 3311)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (205, N'Tenerife', 1, 1, 0, 3312)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (205, N'Teruel', 1, 1, 0, 3313)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (205, N'Toledo', 1, 1, 0, 3314)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (205, N'Valencia', 1, 1, 0, 3315)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (205, N'Valladolid', 1, 1, 0, 3316)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (205, N'Vizcaya', 1, 1, 0, 3317)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (205, N'Zamora', 1, 1, 0, 3318)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (205, N'Zaragoza', 1, 1, 0, 3319)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (206, N'Amparai', 1, 1, 0, 3320)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (206, N'Anuradhapuraya', 1, 1, 0, 3321)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (206, N'Badulla', 1, 1, 0, 3322)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (206, N'Boralesgamuwa', 1, 1, 0, 3323)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (206, N'Colombo', 1, 1, 0, 3324)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (206, N'Galla', 1, 1, 0, 3325)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (206, N'Gampaha', 1, 1, 0, 3326)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (206, N'Hambantota', 1, 1, 0, 3327)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (206, N'Kalatura', 1, 1, 0, 3328)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (206, N'Kegalla', 1, 1, 0, 3329)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (206, N'Kilinochchi', 1, 1, 0, 3330)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (206, N'Kurunegala', 1, 1, 0, 3331)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (206, N'Madakalpuwa', 1, 1, 0, 3332)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (206, N'Maha Nuwara', 1, 1, 0, 3333)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (206, N'Malwana', 1, 1, 0, 3334)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (206, N'Mannarama', 1, 1, 0, 3335)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (206, N'Matale', 1, 1, 0, 3336)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (206, N'Matara', 1, 1, 0, 3337)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (206, N'Monaragala', 1, 1, 0, 3338)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (206, N'Mullaitivu', 1, 1, 0, 3339)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (206, N'North Eastern Province', 1, 1, 0, 3340)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (206, N'North Western Province', 1, 1, 0, 3341)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (206, N'Nuwara Eliya', 1, 1, 0, 3342)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (206, N'Polonnaruwa', 1, 1, 0, 3343)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (206, N'Puttalama', 1, 1, 0, 3344)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (206, N'Ratnapuraya', 1, 1, 0, 3345)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (206, N'Southern Province', 1, 1, 0, 3346)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (206, N'Tirikunamalaya', 1, 1, 0, 3347)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (206, N'Tuscany', 1, 1, 0, 3348)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (206, N'Vavuniyawa', 1, 1, 0, 3349)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (206, N'Western Province', 1, 1, 0, 3350)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (206, N'Yapanaya', 1, 1, 0, 3351)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (206, N'kadawatha', 1, 1, 0, 3352)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (207, N'A''''ali-an-Nil', 1, 1, 0, 3353)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (207, N'Bahr-al-Jabal', 1, 1, 0, 3354)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (207, N'Central Equatoria', 1, 1, 0, 3355)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (207, N'Gharb Bahr-al-Ghazal', 1, 1, 0, 3356)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (207, N'Gharb Darfur', 1, 1, 0, 3357)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (207, N'Gharb Kurdufan', 1, 1, 0, 3358)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (207, N'Gharb-al-Istiwa''''iyah', 1, 1, 0, 3359)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (207, N'Janub Darfur', 1, 1, 0, 3360)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (207, N'Janub Kurdufan', 1, 1, 0, 3361)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (207, N'Junqali', 1, 1, 0, 3362)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (207, N'Kassala', 1, 1, 0, 3363)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (207, N'Nahr-an-Nil', 1, 1, 0, 3364)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (207, N'Shamal Bahr-al-Ghazal', 1, 1, 0, 3365)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (207, N'Shamal Darfur', 1, 1, 0, 3366)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (207, N'Shamal Kurdufan', 1, 1, 0, 3367)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (207, N'Sharq-al-Istiwa''''iyah', 1, 1, 0, 3368)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (207, N'Sinnar', 1, 1, 0, 3369)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (207, N'Warab', 1, 1, 0, 3370)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (207, N'Wilayat al Khartum', 1, 1, 0, 3371)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (207, N'al-Bahr-al-Ahmar', 1, 1, 0, 3372)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (207, N'al-Buhayrat', 1, 1, 0, 3373)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (207, N'al-Jazirah', 1, 1, 0, 3374)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (207, N'al-Khartum', 1, 1, 0, 3375)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (207, N'al-Qadarif', 1, 1, 0, 3376)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (207, N'al-Wahdah', 1, 1, 0, 3377)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (207, N'an-Nil-al-Abyad', 1, 1, 0, 3378)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (207, N'an-Nil-al-Azraq', 1, 1, 0, 3379)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (207, N'ash-Shamaliyah', 1, 1, 0, 3380)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (208, N'Brokopondo', 1, 1, 0, 3381)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (208, N'Commewijne', 1, 1, 0, 3382)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (208, N'Coronie', 1, 1, 0, 3383)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (208, N'Marowijne', 1, 1, 0, 3384)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (208, N'Nickerie', 1, 1, 0, 3385)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (208, N'Para', 1, 1, 0, 3386)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (208, N'Paramaribo', 1, 1, 0, 3387)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (208, N'Saramacca', 1, 1, 0, 3388)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (208, N'Wanica', 1, 1, 0, 3389)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (209, N'Svalbard', 1, 1, 0, 3390)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (210, N'Hhohho', 1, 1, 0, 3391)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (210, N'Lubombo', 1, 1, 0, 3392)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (210, N'Manzini', 1, 1, 0, 3393)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (210, N'Shiselweni', 1, 1, 0, 3394)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (211, N'Alvsborgs Lan', 1, 1, 0, 3395)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (211, N'Angermanland', 1, 1, 0, 3396)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (211, N'Blekinge', 1, 1, 0, 3397)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (211, N'Bohuslan', 1, 1, 0, 3398)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (211, N'Dalarna', 1, 1, 0, 3399)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (211, N'Gavleborg', 1, 1, 0, 3400)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (211, N'Gaza', 1, 1, 0, 3401)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (211, N'Gotland', 1, 1, 0, 3402)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (211, N'Halland', 1, 1, 0, 3403)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (211, N'Jamtland', 1, 1, 0, 3404)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (211, N'Jonkoping', 1, 1, 0, 3405)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (211, N'Kalmar', 1, 1, 0, 3406)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (211, N'Kristianstads', 1, 1, 0, 3407)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (211, N'Kronoberg', 1, 1, 0, 3408)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (211, N'Norrbotten', 1, 1, 0, 3409)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (211, N'Orebro', 1, 1, 0, 3410)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (211, N'Ostergotland', 1, 1, 0, 3411)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (211, N'Saltsjo-Boo', 1, 1, 0, 3412)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (211, N'Skane', 1, 1, 0, 3413)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (211, N'Smaland', 1, 1, 0, 3414)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (211, N'Sodermanland', 1, 1, 0, 3415)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (211, N'Stockholm', 1, 1, 0, 3416)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (211, N'Uppsala', 1, 1, 0, 3417)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (211, N'Varmland', 1, 1, 0, 3418)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (211, N'Vasterbotten', 1, 1, 0, 3419)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (211, N'Vastergotland', 1, 1, 0, 3420)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (211, N'Vasternorrland', 1, 1, 0, 3421)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (211, N'Vastmanland', 1, 1, 0, 3422)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (211, N'Vastra Gotaland', 1, 1, 0, 3423)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (212, N'Aargau', 1, 1, 0, 3424)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (212, N'Appenzell Inner-Rhoden', 1, 1, 0, 3425)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (212, N'Appenzell-Ausser Rhoden', 1, 1, 0, 3426)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (212, N'Basel-Landschaft', 1, 1, 0, 3427)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (212, N'Basel-Stadt', 1, 1, 0, 3428)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (212, N'Bern', 1, 1, 0, 3429)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (212, N'Canton Ticino', 1, 1, 0, 3430)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (212, N'Fribourg', 1, 1, 0, 3431)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (212, N'Geneve', 1, 1, 0, 3432)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (212, N'Glarus', 1, 1, 0, 3433)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (212, N'Graubunden', 1, 1, 0, 3434)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (212, N'Heerbrugg', 1, 1, 0, 3435)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (212, N'Jura', 1, 1, 0, 3436)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (212, N'Kanton Aargau', 1, 1, 0, 3437)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (212, N'Luzern', 1, 1, 0, 3438)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (212, N'Morbio Inferiore', 1, 1, 0, 3439)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (212, N'Muhen', 1, 1, 0, 3440)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (212, N'Neuchatel', 1, 1, 0, 3441)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (212, N'Nidwalden', 1, 1, 0, 3442)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (212, N'Obwalden', 1, 1, 0, 3443)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (212, N'Sankt Gallen', 1, 1, 0, 3444)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (212, N'Schaffhausen', 1, 1, 0, 3445)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (212, N'Schwyz', 1, 1, 0, 3446)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (212, N'Solothurn', 1, 1, 0, 3447)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (212, N'Thurgau', 1, 1, 0, 3448)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (212, N'Ticino', 1, 1, 0, 3449)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (212, N'Uri', 1, 1, 0, 3450)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (212, N'Valais', 1, 1, 0, 3451)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (212, N'Vaud', 1, 1, 0, 3452)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (212, N'Vauffelin', 1, 1, 0, 3453)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (212, N'Zug', 1, 1, 0, 3454)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (212, N'Zurich', 1, 1, 0, 3455)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (213, N'Aleppo', 1, 1, 0, 3456)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (213, N'Dar''''a', 1, 1, 0, 3457)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (213, N'Dayr-az-Zawr', 1, 1, 0, 3458)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (213, N'Dimashq', 1, 1, 0, 3459)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (213, N'Halab', 1, 1, 0, 3460)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (213, N'Hamah', 1, 1, 0, 3461)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (213, N'Hims', 1, 1, 0, 3462)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (213, N'Idlib', 1, 1, 0, 3463)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (213, N'Madinat Dimashq', 1, 1, 0, 3464)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (213, N'Tartus', 1, 1, 0, 3465)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (213, N'al-Hasakah', 1, 1, 0, 3466)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (213, N'al-Ladhiqiyah', 1, 1, 0, 3467)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (213, N'al-Qunaytirah', 1, 1, 0, 3468)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (213, N'ar-Raqqah', 1, 1, 0, 3469)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (213, N'as-Suwayda', 1, 1, 0, 3470)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (214, N'Changhua County', 1, 1, 0, 3471)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (214, N'Chiayi County', 1, 1, 0, 3472)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (214, N'Chiayi City', 1, 1, 0, 3473)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (214, N'Taipei City', 1, 1, 0, 3474)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (214, N'Hsinchu County', 1, 1, 0, 3475)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (214, N'Hsinchu City', 1, 1, 0, 3476)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (214, N'Hualien County', 1, 1, 0, 3477)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (214, N'Kaohsiung City', 1, 1, 0, 3480)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (214, N'Keelung City', 1, 1, 0, 3481)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (214, N'Kinmen County', 1, 1, 0, 3482)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (214, N'Miaoli County', 1, 1, 0, 3483)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (214, N'Nantou County', 1, 1, 0, 3484)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (214, N'Penghu County', 1, 1, 0, 3486)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (214, N'Pingtung County', 1, 1, 0, 3487)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (214, N'Taichung City', 1, 1, 0, 3488)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (214, N'Tainan City', 1, 1, 0, 3492)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (214, N'New Taipei City', 1, 1, 0, 3493)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (214, N'Taitung County', 1, 1, 0, 3495)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (214, N'Taoyuan City', 1, 1, 0, 3496)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (214, N'Yilan County', 1, 1, 0, 3497)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (214, N'YunLin County', 1, 1, 0, 3498)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (215, N'Dushanbe', 1, 1, 0, 3500)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (215, N'Gorno-Badakhshan', 1, 1, 0, 3501)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (215, N'Karotegin', 1, 1, 0, 3502)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (215, N'Khatlon', 1, 1, 0, 3503)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (215, N'Sughd', 1, 1, 0, 3504)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (216, N'Arusha', 1, 1, 0, 3505)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (216, N'Dar es Salaam', 1, 1, 0, 3506)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (216, N'Dodoma', 1, 1, 0, 3507)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (216, N'Iringa', 1, 1, 0, 3508)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (216, N'Kagera', 1, 1, 0, 3509)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (216, N'Kigoma', 1, 1, 0, 3510)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (216, N'Kilimanjaro', 1, 1, 0, 3511)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (216, N'Lindi', 1, 1, 0, 3512)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (216, N'Mara', 1, 1, 0, 3513)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (216, N'Mbeya', 1, 1, 0, 3514)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (216, N'Morogoro', 1, 1, 0, 3515)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (216, N'Mtwara', 1, 1, 0, 3516)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (216, N'Mwanza', 1, 1, 0, 3517)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (216, N'Pwani', 1, 1, 0, 3518)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (216, N'Rukwa', 1, 1, 0, 3519)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (216, N'Ruvuma', 1, 1, 0, 3520)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (216, N'Shinyanga', 1, 1, 0, 3521)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (216, N'Singida', 1, 1, 0, 3522)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (216, N'Tabora', 1, 1, 0, 3523)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (216, N'Tanga', 1, 1, 0, 3524)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (216, N'Zanzibar and Pemba', 1, 1, 0, 3525)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (217, N'Amnat Charoen', 1, 1, 0, 3526)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (217, N'Ang Thong', 1, 1, 0, 3527)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (217, N'Bangkok', 1, 1, 0, 3528)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (217, N'Buri Ram', 1, 1, 0, 3529)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (217, N'Chachoengsao', 1, 1, 0, 3530)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (217, N'Chai Nat', 1, 1, 0, 3531)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (217, N'Chaiyaphum', 1, 1, 0, 3532)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (217, N'Changwat Chaiyaphum', 1, 1, 0, 3533)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (217, N'Chanthaburi', 1, 1, 0, 3534)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (217, N'Chiang Mai', 1, 1, 0, 3535)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (217, N'Chiang Rai', 1, 1, 0, 3536)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (217, N'Chon Buri', 1, 1, 0, 3537)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (217, N'Chumphon', 1, 1, 0, 3538)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (217, N'Kalasin', 1, 1, 0, 3539)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (217, N'Kamphaeng Phet', 1, 1, 0, 3540)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (217, N'Kanchanaburi', 1, 1, 0, 3541)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (217, N'Khon Kaen', 1, 1, 0, 3542)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (217, N'Krabi', 1, 1, 0, 3543)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (217, N'Krung Thep', 1, 1, 0, 3544)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (217, N'Lampang', 1, 1, 0, 3545)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (217, N'Lamphun', 1, 1, 0, 3546)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (217, N'Loei', 1, 1, 0, 3547)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (217, N'Lop Buri', 1, 1, 0, 3548)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (217, N'Mae Hong Son', 1, 1, 0, 3549)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (217, N'Maha Sarakham', 1, 1, 0, 3550)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (217, N'Mukdahan', 1, 1, 0, 3551)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (217, N'Nakhon Nayok', 1, 1, 0, 3552)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (217, N'Nakhon Pathom', 1, 1, 0, 3553)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (217, N'Nakhon Phanom', 1, 1, 0, 3554)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (217, N'Nakhon Ratchasima', 1, 1, 0, 3555)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (217, N'Nakhon Sawan', 1, 1, 0, 3556)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (217, N'Nakhon Si Thammarat', 1, 1, 0, 3557)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (217, N'Nan', 1, 1, 0, 3558)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (217, N'Narathiwat', 1, 1, 0, 3559)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (217, N'Nong Bua Lam Phu', 1, 1, 0, 3560)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (217, N'Nong Khai', 1, 1, 0, 3561)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (217, N'Nonthaburi', 1, 1, 0, 3562)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (217, N'Pathum Thani', 1, 1, 0, 3563)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (217, N'Pattani', 1, 1, 0, 3564)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (217, N'Phangnga', 1, 1, 0, 3565)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (217, N'Phatthalung', 1, 1, 0, 3566)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (217, N'Phayao', 1, 1, 0, 3567)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (217, N'Phetchabun', 1, 1, 0, 3568)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (217, N'Phetchaburi', 1, 1, 0, 3569)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (217, N'Phichit', 1, 1, 0, 3570)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (217, N'Phitsanulok', 1, 1, 0, 3571)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (217, N'Phra Nakhon Si Ayutthaya', 1, 1, 0, 3572)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (217, N'Phrae', 1, 1, 0, 3573)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (217, N'Phuket', 1, 1, 0, 3574)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (217, N'Prachin Buri', 1, 1, 0, 3575)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (217, N'Prachuap Khiri Khan', 1, 1, 0, 3576)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (217, N'Ranong', 1, 1, 0, 3577)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (217, N'Ratchaburi', 1, 1, 0, 3578)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (217, N'Rayong', 1, 1, 0, 3579)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (217, N'Roi Et', 1, 1, 0, 3580)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (217, N'Sa Kaeo', 1, 1, 0, 3581)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (217, N'Sakon Nakhon', 1, 1, 0, 3582)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (217, N'Samut Prakan', 1, 1, 0, 3583)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (217, N'Samut Sakhon', 1, 1, 0, 3584)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (217, N'Samut Songkhran', 1, 1, 0, 3585)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (217, N'Saraburi', 1, 1, 0, 3586)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (217, N'Satun', 1, 1, 0, 3587)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (217, N'Si Sa Ket', 1, 1, 0, 3588)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (217, N'Sing Buri', 1, 1, 0, 3589)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (217, N'Songkhla', 1, 1, 0, 3590)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (217, N'Sukhothai', 1, 1, 0, 3591)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (217, N'Suphan Buri', 1, 1, 0, 3592)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (217, N'Surat Thani', 1, 1, 0, 3593)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (217, N'Surin', 1, 1, 0, 3594)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (217, N'Tak', 1, 1, 0, 3595)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (217, N'Trang', 1, 1, 0, 3596)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (217, N'Trat', 1, 1, 0, 3597)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (217, N'Ubon Ratchathani', 1, 1, 0, 3598)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (217, N'Udon Thani', 1, 1, 0, 3599)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (217, N'Uthai Thani', 1, 1, 0, 3600)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (217, N'Uttaradit', 1, 1, 0, 3601)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (217, N'Yala', 1, 1, 0, 3602)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (217, N'Yasothon', 1, 1, 0, 3603)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (218, N'Centre', 1, 1, 0, 3604)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (218, N'Kara', 1, 1, 0, 3605)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (218, N'Maritime', 1, 1, 0, 3606)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (218, N'Plateaux', 1, 1, 0, 3607)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (218, N'Savanes', 1, 1, 0, 3608)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (219, N'Atafu', 1, 1, 0, 3609)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (219, N'Fakaofo', 1, 1, 0, 3610)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (219, N'Nukunonu', 1, 1, 0, 3611)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (220, N'Eua', 1, 1, 0, 3612)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (220, N'Ha''''apai', 1, 1, 0, 3613)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (220, N'Niuas', 1, 1, 0, 3614)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (220, N'Tongatapu', 1, 1, 0, 3615)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (220, N'Vava''''u', 1, 1, 0, 3616)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (221, N'Arima-Tunapuna-Piarco', 1, 1, 0, 3617)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (221, N'Caroni', 1, 1, 0, 3618)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (221, N'Chaguanas', 1, 1, 0, 3619)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (221, N'Couva-Tabaquite-Talparo', 1, 1, 0, 3620)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (221, N'Diego Martin', 1, 1, 0, 3621)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (221, N'Glencoe', 1, 1, 0, 3622)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (221, N'Penal Debe', 1, 1, 0, 3623)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (221, N'Point Fortin', 1, 1, 0, 3624)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (221, N'Port of Spain', 1, 1, 0, 3625)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (221, N'Princes Town', 1, 1, 0, 3626)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (221, N'Saint George', 1, 1, 0, 3627)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (221, N'San Fernando', 1, 1, 0, 3628)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (221, N'San Juan', 1, 1, 0, 3629)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (221, N'Sangre Grande', 1, 1, 0, 3630)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (221, N'Siparia', 1, 1, 0, 3631)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (221, N'Tobago', 1, 1, 0, 3632)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (222, N'Aryanah', 1, 1, 0, 3633)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (222, N'Bajah', 1, 1, 0, 3634)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (222, N'Bin ''''Arus', 1, 1, 0, 3635)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (222, N'Binzart', 1, 1, 0, 3636)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (222, N'Gouvernorat de Ariana', 1, 1, 0, 3637)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (222, N'Gouvernorat de Nabeul', 1, 1, 0, 3638)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (222, N'Gouvernorat de Sousse', 1, 1, 0, 3639)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (222, N'Hammamet Yasmine', 1, 1, 0, 3640)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (222, N'Jundubah', 1, 1, 0, 3641)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (222, N'Madaniyin', 1, 1, 0, 3642)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (222, N'Manubah', 1, 1, 0, 3643)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (222, N'Monastir', 1, 1, 0, 3644)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (222, N'Nabul', 1, 1, 0, 3645)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (222, N'Qabis', 1, 1, 0, 3646)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (222, N'Qafsah', 1, 1, 0, 3647)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (222, N'Qibili', 1, 1, 0, 3648)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (222, N'Safaqis', 1, 1, 0, 3649)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (222, N'Sfax', 1, 1, 0, 3650)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (222, N'Sidi Bu Zayd', 1, 1, 0, 3651)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (222, N'Silyanah', 1, 1, 0, 3652)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (222, N'Susah', 1, 1, 0, 3653)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (222, N'Tatawin', 1, 1, 0, 3654)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (222, N'Tawzar', 1, 1, 0, 3655)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (222, N'Tunis', 1, 1, 0, 3656)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (222, N'Zaghwan', 1, 1, 0, 3657)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (222, N'al-Kaf', 1, 1, 0, 3658)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (222, N'al-Mahdiyah', 1, 1, 0, 3659)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (222, N'al-Munastir', 1, 1, 0, 3660)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (222, N'al-Qasrayn', 1, 1, 0, 3661)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (222, N'al-Qayrawan', 1, 1, 0, 3662)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Adana', 1, 1, 0, 3663)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Adiyaman', 1, 1, 0, 3664)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Afyon', 1, 1, 0, 3665)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Agri', 1, 1, 0, 3666)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Aksaray', 1, 1, 0, 3667)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Amasya', 1, 1, 0, 3668)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Ankara', 1, 1, 0, 3669)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Antalya', 1, 1, 0, 3670)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Ardahan', 1, 1, 0, 3671)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Artvin', 1, 1, 0, 3672)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Aydin', 1, 1, 0, 3673)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Balikesir', 1, 1, 0, 3674)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Bartin', 1, 1, 0, 3675)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Batman', 1, 1, 0, 3676)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Bayburt', 1, 1, 0, 3677)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Bilecik', 1, 1, 0, 3678)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Bingol', 1, 1, 0, 3679)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Bitlis', 1, 1, 0, 3680)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Bolu', 1, 1, 0, 3681)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Burdur', 1, 1, 0, 3682)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Bursa', 1, 1, 0, 3683)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Canakkale', 1, 1, 0, 3684)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Cankiri', 1, 1, 0, 3685)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Corum', 1, 1, 0, 3686)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Denizli', 1, 1, 0, 3687)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Diyarbakir', 1, 1, 0, 3688)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Duzce', 1, 1, 0, 3689)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Edirne', 1, 1, 0, 3690)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Elazig', 1, 1, 0, 3691)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Erzincan', 1, 1, 0, 3692)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Erzurum', 1, 1, 0, 3693)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Eskisehir', 1, 1, 0, 3694)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Gaziantep', 1, 1, 0, 3695)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Giresun', 1, 1, 0, 3696)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Gumushane', 1, 1, 0, 3697)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Hakkari', 1, 1, 0, 3698)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Hatay', 1, 1, 0, 3699)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Icel', 1, 1, 0, 3700)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Igdir', 1, 1, 0, 3701)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Isparta', 1, 1, 0, 3702)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Istanbul', 1, 1, 0, 3703)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Izmir', 1, 1, 0, 3704)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Kahramanmaras', 1, 1, 0, 3705)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Karabuk', 1, 1, 0, 3706)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Karaman', 1, 1, 0, 3707)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Kars', 1, 1, 0, 3708)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Karsiyaka', 1, 1, 0, 3709)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Kastamonu', 1, 1, 0, 3710)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Kayseri', 1, 1, 0, 3711)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Kilis', 1, 1, 0, 3712)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Kirikkale', 1, 1, 0, 3713)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Kirklareli', 1, 1, 0, 3714)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Kirsehir', 1, 1, 0, 3715)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Kocaeli', 1, 1, 0, 3716)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Konya', 1, 1, 0, 3717)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Kutahya', 1, 1, 0, 3718)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Lefkosa', 1, 1, 0, 3719)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Malatya', 1, 1, 0, 3720)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Manisa', 1, 1, 0, 3721)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Mardin', 1, 1, 0, 3722)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Mugla', 1, 1, 0, 3723)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Mus', 1, 1, 0, 3724)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Nevsehir', 1, 1, 0, 3725)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Nigde', 1, 1, 0, 3726)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Ordu', 1, 1, 0, 3727)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Osmaniye', 1, 1, 0, 3728)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Rize', 1, 1, 0, 3729)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Sakarya', 1, 1, 0, 3730)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Samsun', 1, 1, 0, 3731)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Sanliurfa', 1, 1, 0, 3732)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Siirt', 1, 1, 0, 3733)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Sinop', 1, 1, 0, 3734)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Sirnak', 1, 1, 0, 3735)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Sivas', 1, 1, 0, 3736)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Tekirdag', 1, 1, 0, 3737)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Tokat', 1, 1, 0, 3738)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Trabzon', 1, 1, 0, 3739)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Tunceli', 1, 1, 0, 3740)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Usak', 1, 1, 0, 3741)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Van', 1, 1, 0, 3742)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Yalova', 1, 1, 0, 3743)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Yozgat', 1, 1, 0, 3744)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (223, N'Zonguldak', 1, 1, 0, 3745)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (224, N'Ahal', 1, 1, 0, 3746)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (224, N'Asgabat', 1, 1, 0, 3747)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (224, N'Balkan', 1, 1, 0, 3748)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (224, N'Dasoguz', 1, 1, 0, 3749)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (224, N'Lebap', 1, 1, 0, 3750)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (224, N'Mari', 1, 1, 0, 3751)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (225, N'Grand Turk', 1, 1, 0, 3752)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (225, N'South Caicos and East Caicos', 1, 1, 0, 3753)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (226, N'Funafuti', 1, 1, 0, 3754)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (226, N'Nanumanga', 1, 1, 0, 3755)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (226, N'Nanumea', 1, 1, 0, 3756)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (226, N'Niutao', 1, 1, 0, 3757)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (226, N'Nui', 1, 1, 0, 3758)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (226, N'Nukufetau', 1, 1, 0, 3759)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (226, N'Nukulaelae', 1, 1, 0, 3760)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (226, N'Vaitupu', 1, 1, 0, 3761)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (227, N'Central', 1, 1, 0, 3762)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (227, N'Eastern', 1, 1, 0, 3763)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (227, N'Northern', 1, 1, 0, 3764)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (227, N'Western', 1, 1, 0, 3765)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (228, N'Cherkas''''ka', 1, 1, 0, 3766)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (228, N'Chernihivs''''ka', 1, 1, 0, 3767)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (228, N'Chernivets''''ka', 1, 1, 0, 3768)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (228, N'Crimea', 1, 1, 0, 3769)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (228, N'Dnipropetrovska', 1, 1, 0, 3770)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (228, N'Donets''''ka', 1, 1, 0, 3771)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (228, N'Ivano-Frankivs''''ka', 1, 1, 0, 3772)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (228, N'Kharkiv', 1, 1, 0, 3773)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (228, N'Kharkov', 1, 1, 0, 3774)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (228, N'Khersonska', 1, 1, 0, 3775)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (228, N'Khmel''''nyts''''ka', 1, 1, 0, 3776)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (228, N'Kirovohrad', 1, 1, 0, 3777)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (228, N'Krym', 1, 1, 0, 3778)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (228, N'Kyyiv', 1, 1, 0, 3779)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (228, N'Kyyivs''''ka', 1, 1, 0, 3780)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (228, N'L''''vivs''''ka', 1, 1, 0, 3781)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (228, N'Luhans''''ka', 1, 1, 0, 3782)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (228, N'Mykolayivs''''ka', 1, 1, 0, 3783)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (228, N'Odes''''ka', 1, 1, 0, 3784)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (228, N'Odessa', 1, 1, 0, 3785)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (228, N'Poltavs''''ka', 1, 1, 0, 3786)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (228, N'Rivnens''''ka', 1, 1, 0, 3787)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (228, N'Sevastopol', 1, 1, 0, 3788)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (228, N'Sums''''ka', 1, 1, 0, 3789)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (228, N'Ternopil''''s''''ka', 1, 1, 0, 3790)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (228, N'Volyns''''ka', 1, 1, 0, 3791)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (228, N'Vynnyts''''ka', 1, 1, 0, 3792)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (228, N'Zakarpats''''ka', 1, 1, 0, 3793)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (228, N'Zaporizhia', 1, 1, 0, 3794)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (228, N'Zhytomyrs''''ka', 1, 1, 0, 3795)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (229, N'Abu Zabi', 1, 1, 0, 3796)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (229, N'Ajman', 1, 1, 0, 3797)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (229, N'Dubai', 1, 1, 0, 3798)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (229, N'Ras al-Khaymah', 1, 1, 0, 3799)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (229, N'Sharjah', 1, 1, 0, 3800)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (229, N'Sharjha', 1, 1, 0, 3801)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (229, N'Umm al Qaywayn', 1, 1, 0, 3802)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (229, N'al-Fujayrah', 1, 1, 0, 3803)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (229, N'ash-Shariqah', 1, 1, 0, 3804)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Aberdeen', 1, 1, 0, 3805)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Aberdeenshire', 1, 1, 0, 3806)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Argyll', 1, 1, 0, 3807)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Armagh', 1, 1, 0, 3808)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Bedfordshire', 1, 1, 0, 3809)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Belfast', 1, 1, 0, 3810)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Berkshire', 1, 1, 0, 3811)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Birmingham', 1, 1, 0, 3812)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Brechin', 1, 1, 0, 3813)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Bridgnorth', 1, 1, 0, 3814)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Bristol', 1, 1, 0, 3815)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Buckinghamshire', 1, 1, 0, 3816)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Cambridge', 1, 1, 0, 3817)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Cambridgeshire', 1, 1, 0, 3818)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Channel Islands', 1, 1, 0, 3819)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Cheshire', 1, 1, 0, 3820)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Cleveland', 1, 1, 0, 3821)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Co Fermanagh', 1, 1, 0, 3822)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Conwy', 1, 1, 0, 3823)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Cornwall', 1, 1, 0, 3824)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Coventry', 1, 1, 0, 3825)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Craven Arms', 1, 1, 0, 3826)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Cumbria', 1, 1, 0, 3827)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Denbighshire', 1, 1, 0, 3828)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Derby', 1, 1, 0, 3829)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Derbyshire', 1, 1, 0, 3830)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Devon', 1, 1, 0, 3831)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Dial Code Dungannon', 1, 1, 0, 3832)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Didcot', 1, 1, 0, 3833)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Dorset', 1, 1, 0, 3834)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Dunbartonshire', 1, 1, 0, 3835)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Durham', 1, 1, 0, 3836)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'East Dunbartonshire', 1, 1, 0, 3837)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'East Lothian', 1, 1, 0, 3838)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'East Midlands', 1, 1, 0, 3839)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'East Sussex', 1, 1, 0, 3840)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'East Yorkshire', 1, 1, 0, 3841)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'England', 1, 1, 0, 3842)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Essex', 1, 1, 0, 3843)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Fermanagh', 1, 1, 0, 3844)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Fife', 1, 1, 0, 3845)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Flintshire', 1, 1, 0, 3846)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Fulham', 1, 1, 0, 3847)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Gainsborough', 1, 1, 0, 3848)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Glocestershire', 1, 1, 0, 3849)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Gwent', 1, 1, 0, 3850)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Hampshire', 1, 1, 0, 3851)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Hants', 1, 1, 0, 3852)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Herefordshire', 1, 1, 0, 3853)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Hertfordshire', 1, 1, 0, 3854)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Ireland', 1, 1, 0, 3855)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Isle Of Man', 1, 1, 0, 3856)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Isle of Wight', 1, 1, 0, 3857)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Kenford', 1, 1, 0, 3858)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Kent', 1, 1, 0, 3859)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Kilmarnock', 1, 1, 0, 3860)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Lanarkshire', 1, 1, 0, 3861)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Lancashire', 1, 1, 0, 3862)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Leicestershire', 1, 1, 0, 3863)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Lincolnshire', 1, 1, 0, 3864)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Llanymynech', 1, 1, 0, 3865)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'London', 1, 1, 0, 3866)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Ludlow', 1, 1, 0, 3867)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Manchester', 1, 1, 0, 3868)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Mayfair', 1, 1, 0, 3869)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Merseyside', 1, 1, 0, 3870)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Mid Glamorgan', 1, 1, 0, 3871)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Middlesex', 1, 1, 0, 3872)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Mildenhall', 1, 1, 0, 3873)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Monmouthshire', 1, 1, 0, 3874)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Newton Stewart', 1, 1, 0, 3875)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Norfolk', 1, 1, 0, 3876)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'North Humberside', 1, 1, 0, 3877)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'North Yorkshire', 1, 1, 0, 3878)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Northamptonshire', 1, 1, 0, 3879)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Northants', 1, 1, 0, 3880)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Northern Ireland', 1, 1, 0, 3881)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Northumberland', 1, 1, 0, 3882)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Nottinghamshire', 1, 1, 0, 3883)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Oxford', 1, 1, 0, 3884)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Powys', 1, 1, 0, 3885)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Roos-shire', 1, 1, 0, 3886)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'SUSSEX', 1, 1, 0, 3887)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Sark', 1, 1, 0, 3888)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Scotland', 1, 1, 0, 3889)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Scottish Borders', 1, 1, 0, 3890)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Shropshire', 1, 1, 0, 3891)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Somerset', 1, 1, 0, 3892)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'South Glamorgan', 1, 1, 0, 3893)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'South Wales', 1, 1, 0, 3894)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'South Yorkshire', 1, 1, 0, 3895)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Southwell', 1, 1, 0, 3896)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Staffordshire', 1, 1, 0, 3897)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Strabane', 1, 1, 0, 3898)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Suffolk', 1, 1, 0, 3899)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Surrey', 1, 1, 0, 3900)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Sussex', 1, 1, 0, 3901)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Twickenham', 1, 1, 0, 3902)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Tyne and Wear', 1, 1, 0, 3903)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Tyrone', 1, 1, 0, 3904)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Utah', 1, 1, 0, 3905)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Wales', 1, 1, 0, 3906)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Warwickshire', 1, 1, 0, 3907)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'West Lothian', 1, 1, 0, 3908)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'West Midlands', 1, 1, 0, 3909)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'West Sussex', 1, 1, 0, 3910)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'West Yorkshire', 1, 1, 0, 3911)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Whissendine', 1, 1, 0, 3912)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Wiltshire', 1, 1, 0, 3913)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Wokingham', 1, 1, 0, 3914)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Worcestershire', 1, 1, 0, 3915)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Wrexham', 1, 1, 0, 3916)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Wurttemberg', 1, 1, 0, 3917)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (230, N'Yorkshire', 1, 1, 0, 3918)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (231, N'Alabama', 1, 1, 0, 3919)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (231, N'Alaska', 1, 1, 0, 3920)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (231, N'Arizona', 1, 1, 0, 3921)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (231, N'Arkansas', 1, 1, 0, 3922)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (231, N'Byram', 1, 1, 0, 3923)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (231, N'California', 1, 1, 0, 3924)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (231, N'Cokato', 1, 1, 0, 3925)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (231, N'Colorado', 1, 1, 0, 3926)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (231, N'Connecticut', 1, 1, 0, 3927)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (231, N'Delaware', 1, 1, 0, 3928)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (231, N'District of Columbia', 1, 1, 0, 3929)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (231, N'Florida', 1, 1, 0, 3930)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (231, N'Georgia', 1, 1, 0, 3931)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (231, N'Hawaii', 1, 1, 0, 3932)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (231, N'Idaho', 1, 1, 0, 3933)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (231, N'Illinois', 1, 1, 0, 3934)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (231, N'Indiana', 1, 1, 0, 3935)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (231, N'Iowa', 1, 1, 0, 3936)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (231, N'Kansas', 1, 1, 0, 3937)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (231, N'Kentucky', 1, 1, 0, 3938)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (231, N'Louisiana', 1, 1, 0, 3939)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (231, N'Lowa', 1, 1, 0, 3940)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (231, N'Maine', 1, 1, 0, 3941)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (231, N'Maryland', 1, 1, 0, 3942)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (231, N'Massachusetts', 1, 1, 0, 3943)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (231, N'Medfield', 1, 1, 0, 3944)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (231, N'Michigan', 1, 1, 0, 3945)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (231, N'Minnesota', 1, 1, 0, 3946)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (231, N'Mississippi', 1, 1, 0, 3947)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (231, N'Missouri', 1, 1, 0, 3948)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (231, N'Montana', 1, 1, 0, 3949)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (231, N'Nebraska', 1, 1, 0, 3950)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (231, N'Nevada', 1, 1, 0, 3951)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (231, N'New Hampshire', 1, 1, 0, 3952)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (231, N'New Jersey', 1, 1, 0, 3953)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (231, N'New Jersy', 1, 1, 0, 3954)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (231, N'New Mexico', 1, 1, 0, 3955)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (231, N'New York', 1, 1, 0, 3956)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (231, N'North Carolina', 1, 1, 0, 3957)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (231, N'North Dakota', 1, 1, 0, 3958)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (231, N'Ohio', 1, 1, 0, 3959)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (231, N'Oklahoma', 1, 1, 0, 3960)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (231, N'Ontario', 1, 1, 0, 3961)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (231, N'Oregon', 1, 1, 0, 3962)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (231, N'Pennsylvania', 1, 1, 0, 3963)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (231, N'Ramey', 1, 1, 0, 3964)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (231, N'Rhode Island', 1, 1, 0, 3965)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (231, N'South Carolina', 1, 1, 0, 3966)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (231, N'South Dakota', 1, 1, 0, 3967)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (231, N'Sublimity', 1, 1, 0, 3968)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (231, N'Tennessee', 1, 1, 0, 3969)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (231, N'Texas', 1, 1, 0, 3970)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (231, N'Trimble', 1, 1, 0, 3971)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (231, N'Utah', 1, 1, 0, 3972)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (231, N'Vermont', 1, 1, 0, 3973)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (231, N'Virginia', 1, 1, 0, 3974)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (231, N'Washington', 1, 1, 0, 3975)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (231, N'West Virginia', 1, 1, 0, 3976)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (231, N'Wisconsin', 1, 1, 0, 3977)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (231, N'Wyoming', 1, 1, 0, 3978)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (232, N'United States Minor Outlying I', 1, 1, 0, 3979)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (233, N'Artigas', 1, 1, 0, 3980)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (233, N'Canelones', 1, 1, 0, 3981)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (233, N'Cerro Largo', 1, 1, 0, 3982)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (233, N'Colonia', 1, 1, 0, 3983)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (233, N'Durazno', 1, 1, 0, 3984)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (233, N'FLorida', 1, 1, 0, 3985)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (233, N'Flores', 1, 1, 0, 3986)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (233, N'Lavalleja', 1, 1, 0, 3987)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (233, N'Maldonado', 1, 1, 0, 3988)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (233, N'Montevideo', 1, 1, 0, 3989)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (233, N'Paysandu', 1, 1, 0, 3990)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (233, N'Rio Negro', 1, 1, 0, 3991)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (233, N'Rivera', 1, 1, 0, 3992)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (233, N'Rocha', 1, 1, 0, 3993)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (233, N'Salto', 1, 1, 0, 3994)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (233, N'San Jose', 1, 1, 0, 3995)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (233, N'Soriano', 1, 1, 0, 3996)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (233, N'Tacuarembo', 1, 1, 0, 3997)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (233, N'Treinta y Tres', 1, 1, 0, 3998)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (234, N'Andijon', 1, 1, 0, 3999)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (234, N'Buhoro', 1, 1, 0, 4000)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (234, N'Buxoro Viloyati', 1, 1, 0, 4001)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (234, N'Cizah', 1, 1, 0, 4002)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (234, N'Fargona', 1, 1, 0, 4003)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (234, N'Horazm', 1, 1, 0, 4004)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (234, N'Kaskadar', 1, 1, 0, 4005)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (234, N'Korakalpogiston', 1, 1, 0, 4006)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (234, N'Namangan', 1, 1, 0, 4007)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (234, N'Navoi', 1, 1, 0, 4008)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (234, N'Samarkand', 1, 1, 0, 4009)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (234, N'Sirdare', 1, 1, 0, 4010)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (234, N'Surhondar', 1, 1, 0, 4011)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (234, N'Toskent', 1, 1, 0, 4012)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (235, N'Malampa', 1, 1, 0, 4013)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (235, N'Penama', 1, 1, 0, 4014)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (235, N'Sanma', 1, 1, 0, 4015)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (235, N'Shefa', 1, 1, 0, 4016)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (235, N'Tafea', 1, 1, 0, 4017)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (235, N'Torba', 1, 1, 0, 4018)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (236, N'Vatican City State (Holy See)', 1, 1, 0, 4019)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (237, N'Amazonas', 1, 1, 0, 4020)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (237, N'Anzoategui', 1, 1, 0, 4021)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (237, N'Apure', 1, 1, 0, 4022)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (237, N'Aragua', 1, 1, 0, 4023)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (237, N'Barinas', 1, 1, 0, 4024)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (237, N'Bolivar', 1, 1, 0, 4025)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (237, N'Carabobo', 1, 1, 0, 4026)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (237, N'Cojedes', 1, 1, 0, 4027)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (237, N'Delta Amacuro', 1, 1, 0, 4028)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (237, N'Distrito Federal', 1, 1, 0, 4029)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (237, N'Falcon', 1, 1, 0, 4030)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (237, N'Guarico', 1, 1, 0, 4031)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (237, N'Lara', 1, 1, 0, 4032)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (237, N'Merida', 1, 1, 0, 4033)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (237, N'Miranda', 1, 1, 0, 4034)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (237, N'Monagas', 1, 1, 0, 4035)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (237, N'Nueva Esparta', 1, 1, 0, 4036)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (237, N'Portuguesa', 1, 1, 0, 4037)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (237, N'Sucre', 1, 1, 0, 4038)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (237, N'Tachira', 1, 1, 0, 4039)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (237, N'Trujillo', 1, 1, 0, 4040)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (237, N'Vargas', 1, 1, 0, 4041)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (237, N'Yaracuy', 1, 1, 0, 4042)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (237, N'Zulia', 1, 1, 0, 4043)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (238, N'Bac Giang', 1, 1, 0, 4044)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (238, N'Binh Dinh', 1, 1, 0, 4045)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (238, N'Binh Duong', 1, 1, 0, 4046)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (238, N'Da Nang', 1, 1, 0, 4047)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (238, N'Dong Bang Song Cuu Long', 1, 1, 0, 4048)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (238, N'Dong Bang Song Hong', 1, 1, 0, 4049)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (238, N'Dong Nai', 1, 1, 0, 4050)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (238, N'Dong Nam Bo', 1, 1, 0, 4051)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (238, N'Duyen Hai Mien Trung', 1, 1, 0, 4052)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (238, N'Hanoi', 1, 1, 0, 4053)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (238, N'Hung Yen', 1, 1, 0, 4054)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (238, N'Khu Bon Cu', 1, 1, 0, 4055)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (238, N'Long An', 1, 1, 0, 4056)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (238, N'Mien Nui Va Trung Du', 1, 1, 0, 4057)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (238, N'Thai Nguyen', 1, 1, 0, 4058)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (238, N'Thanh Pho Ho Chi Minh', 1, 1, 0, 4059)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (238, N'Thu Do Ha Noi', 1, 1, 0, 4060)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (238, N'Tinh Can Tho', 1, 1, 0, 4061)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (238, N'Tinh Da Nang', 1, 1, 0, 4062)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (238, N'Tinh Gia Lai', 1, 1, 0, 4063)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (239, N'Anegada', 1, 1, 0, 4064)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (239, N'Jost van Dyke', 1, 1, 0, 4065)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (239, N'Tortola', 1, 1, 0, 4066)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (240, N'Saint Croix', 1, 1, 0, 4067)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (240, N'Saint John', 1, 1, 0, 4068)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (240, N'Saint Thomas', 1, 1, 0, 4069)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (241, N'Alo', 1, 1, 0, 4070)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (241, N'Singave', 1, 1, 0, 4071)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (241, N'Wallis', 1, 1, 0, 4072)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (242, N'Bu Jaydur', 1, 1, 0, 4073)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (242, N'Wad-adh-Dhahab', 1, 1, 0, 4074)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (242, N'al-''''Ayun', 1, 1, 0, 4075)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (242, N'as-Samarah', 1, 1, 0, 4076)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (243, N'Adan', 1, 1, 0, 4077)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (243, N'Abyan', 1, 1, 0, 4078)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (243, N'Dhamar', 1, 1, 0, 4079)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (243, N'Hadramaut', 1, 1, 0, 4080)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (243, N'Hajjah', 1, 1, 0, 4081)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (243, N'Hudaydah', 1, 1, 0, 4082)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (243, N'Ibb', 1, 1, 0, 4083)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (243, N'Lahij', 1, 1, 0, 4084)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (243, N'Ma''''rib', 1, 1, 0, 4085)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (243, N'Madinat San''''a', 1, 1, 0, 4086)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (243, N'Sa''''dah', 1, 1, 0, 4087)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (243, N'Sana', 1, 1, 0, 4088)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (243, N'Shabwah', 1, 1, 0, 4089)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (243, N'Ta''''izz', 1, 1, 0, 4090)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (243, N'al-Bayda', 1, 1, 0, 4091)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (243, N'al-Hudaydah', 1, 1, 0, 4092)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (243, N'al-Jawf', 1, 1, 0, 4093)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (243, N'al-Mahrah', 1, 1, 0, 4094)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (243, N'al-Mahwit', 1, 1, 0, 4095)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (244, N'Central Serbia', 1, 1, 0, 4096)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (244, N'Kosovo and Metohija', 1, 1, 0, 4097)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (244, N'Montenegro', 1, 1, 0, 4098)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (244, N'Republic of Serbia', 1, 1, 0, 4099)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (244, N'Serbia', 1, 1, 0, 4100)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (244, N'Vojvodina', 1, 1, 0, 4101)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (245, N'Central', 1, 1, 0, 4102)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (245, N'Copperbelt', 1, 1, 0, 4103)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (245, N'Eastern', 1, 1, 0, 4104)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (245, N'Luapala', 1, 1, 0, 4105)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (245, N'Lusaka', 1, 1, 0, 4106)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (245, N'North-Western', 1, 1, 0, 4107)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (245, N'Northern', 1, 1, 0, 4108)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (245, N'Southern', 1, 1, 0, 4109)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (245, N'Western', 1, 1, 0, 4110)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (246, N'Bulawayo', 1, 1, 0, 4111)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (246, N'Harare', 1, 1, 0, 4112)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (246, N'Manicaland', 1, 1, 0, 4113)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (246, N'Mashonaland Central', 1, 1, 0, 4114)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (246, N'Mashonaland East', 1, 1, 0, 4115)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (246, N'Mashonaland West', 1, 1, 0, 4116)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (246, N'Masvingo', 1, 1, 0, 4117)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (246, N'Matabeleland North', 1, 1, 0, 4118)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (246, N'Matabeleland South', 1, 1, 0, 4119)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (246, N'Midlands', 1, 1, 0, 4120)

INSERT [StateOrProvince] ([CountryId], [Name], [Published], [ShippingEnabled], [DisplayOrder], [Id]) VALUES (214, N'Lienchiang County', 1, 1, 0, 4121)

SET IDENTITY_INSERT [StateOrProvince] OFF

SET IDENTITY_INSERT [Setting] ON 

DELETE FROM [Setting] WHERE [Key] = 'SitePlugins'

INSERT [Setting] ([GroupName], [Key], [Value], [Id]) VALUES (N'PluginSettings', N'SitePlugins', N'[{"pluginSystemName":"EvenCart.Ui.Slider","installed":true,"active":true},{"pluginSystemName":"EvenCart.Payments.Stripe","installed":true,"active":true},{"pluginSystemName":"EvenCart.Payments.PaypalWithRedirect","installed":true,"active":true},{"pluginSystemName":"EvenCart.Ui.SearchPlus","installed":true,"active":true},{"pluginSystemName":"EvenCart.Payments.Square","installed":true,"active":true},{"pluginSystemName":"Shipping.Shippo","installed":true,"active":true}]', 95)

INSERT [Setting] ([GroupName], [Key], [Value], [Id]) VALUES (N'PluginSettings', N'SiteWidgets', N'[{"pluginSystemName":"EvenCart.InbuiltWidgets","widgetSystemName":"Menu","displayOrder":0,"zoneName":"footer-one","id":"c3309a82-1e46-4b0c-b420-22c1d0fac4d2"},{"pluginSystemName":"EvenCart.InbuiltWidgets","widgetSystemName":"Menu","displayOrder":0,"zoneName":"footer-two","id":"8b66cb71-7d69-4414-94ac-13954ba86a74"},{"pluginSystemName":"EvenCart.InbuiltWidgets","widgetSystemName":"SocialIcons","displayOrder":0,"zoneName":"footer-four","id":"4f464c03-8b33-4dbc-8458-d89904d3f252"},{"pluginSystemName":"EvenCart.InbuiltWidgets","widgetSystemName":"CustomHtml","displayOrder":0,"zoneName":"footer-three","id":"4fa74cb6-3602-4674-b9e1-94619a6662d8"},{"pluginSystemName":"EvenCart.InbuiltWidgets","widgetSystemName":"CustomHtml","displayOrder":0,"zoneName":"footer","id":"98ae4133-a388-47d4-9489-25064b2a4ee0"},{"pluginSystemName":"EvenCart.Ui.Slider","widgetSystemName":"SliderWidget","displayOrder":0,"zoneName":"slider","id":"04aeb61e-b4af-434e-a1ac-d47a2c282861"},{"pluginSystemName":"EvenCart.InbuiltWidgets","widgetSystemName":"ProductCarousel","displayOrder":1,"zoneName":"slider","id":"5fce627c-e9e1-4060-b691-e55535563c43"},{"pluginSystemName":"EvenCart.InbuiltWidgets","widgetSystemName":"ProductCarousel","displayOrder":2,"zoneName":"slider","id":"eb4cece4-4586-4cc2-980e-9f6664f3c6e9"},{"pluginSystemName":"EvenCart.Ui.SearchPlus","widgetSystemName":"SearchPlusWidget","displayOrder":0,"zoneName":"after_global_search","id":"c3b1ecb8-1989-4727-b8a4-7371b4b041f7"}]', 96)

INSERT [Setting] ([GroupName], [Key], [Value], [Id]) VALUES (N'WidgetSettings', N'widget_c3309a82-1e46-4b0c-b420-22c1d0fac4d2', N'{"title":"EvenCart Store","menuId":2,"id":"c3309a82-1e46-4b0c-b420-22c1d0fac4d2"}', 97)

INSERT [Setting] ([GroupName], [Key], [Value], [Id]) VALUES (N'WidgetSettings', N'widget_8b66cb71-7d69-4414-94ac-13954ba86a74', N'{"title":"My Account","menuId":3,"id":"8b66cb71-7d69-4414-94ac-13954ba86a74"}', 98)

INSERT [Setting] ([GroupName], [Key], [Value], [Id]) VALUES (N'WidgetSettings', N'widget_4f464c03-8b33-4dbc-8458-d89904d3f252', N'{"title":"Connect With Us","facebookUrl":"https://www.facebook.com/evencart","twitterUrl":"https://www.twitter.com/evencarthq","instagramUrl":"","linkedInUrl":"https://www.linkedin.com/evencart","youtubeUrl":"","rssFeedUrl":"","whatsAppUrl":"","skypeUrl":"","emailUrl":"mailto:support@evencart.com","id":"4f464c03-8b33-4dbc-8458-d89904d3f252"}', 99)

INSERT [Setting] ([GroupName], [Key], [Value], [Id]) VALUES (N'WidgetSettings', N'widget_4fa74cb6-3602-4674-b9e1-94619a6662d8', N'{"title":"Reach Us","content":"<p><strong>Sojatia Infocrafts Pvt. Ltd.</strong><br><span style=\"font-size: 0.9rem; letter-spacing: 0.02rem;\">M25, Sterling Tower, Near Apollo Tower<br></span><span style=\"font-size: 0.9rem; letter-spacing: 0.02rem;\">M.G. Road, Indore - 452009<br></span><span style=\"font-size: 0.9rem; letter-spacing: 0.02rem;\">Madhya Pradesh, India</span></p>","customFormat":"","id":"4fa74cb6-3602-4674-b9e1-94619a6662d8"}', 100)

INSERT [Setting] ([GroupName], [Key], [Value], [Id]) VALUES (N'WidgetSettings', N'widget_98ae4133-a388-47d4-9489-25064b2a4ee0', N'{"title":"","content":"<p style=\"text-align: center;\"><span style=\"text-align: left; font-size: 14.4px;\">©&nbsp;</span><span style=\"font-size: 0.9rem; letter-spacing: 0.02rem;\">Copyright 2019 Sojatia Infocrafts Private Limited. All rights reserved.</span></p>","customFormat":"","id":"98ae4133-a388-47d4-9489-25064b2a4ee0"}', 101)

INSERT [Setting] ([GroupName], [Key], [Value], [Id]) VALUES (N'WidgetSettings', N'widget_5fce627c-e9e1-4060-b691-e55535563c43', N'{"title":"Best Sellers","productIds":[12,8,6,11],"id":"5fce627c-e9e1-4060-b691-e55535563c43"}', 102)

INSERT [Setting] ([GroupName], [Key], [Value], [Id]) VALUES (N'WidgetSettings', N'widget_eb4cece4-4586-4cc2-980e-9f6664f3c6e9', N'{"title":"Premium Apple Collection","productIds":[4,8],"id":"eb4cece4-4586-4cc2-980e-9f6664f3c6e9"}', 103)

SET IDENTITY_INSERT [Setting] OFF

SET IDENTITY_INSERT [Tax] ON 

INSERT [Tax] ([Name], [Id]) VALUES (N'Goods and Services Tax', 1)

SET IDENTITY_INSERT [Tax] OFF



-- enable all constraints
exec sp_MSforeachtable @command1="print '?'", @command2="ALTER TABLE ? WITH CHECK CHECK CONSTRAINT all"