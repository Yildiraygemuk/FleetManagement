In this application, I designed a small scale fleet management system where vehicles deliver to predetermined locations on a given route.

The system includes two different types of shipments that can be transported by vehicles and unloaded at delivery points. Delivery points, barcode numbers and volumetric weight are indicated on the shipments.

The system includes three different delivery points.
* Branch: Only package-type shipments can be unloaded. Bags and packages in bags may not be unloaded.
* Distribution Center: Bags, packages in bags and packages not assigned to any bags may be unloaded.
* Transfer Center: Only bags and packages in bags may be unloaded.

Shipments with delivery points that do not meet the above criteria cannot be unloaded. In such cases, these particular shipments are skipped and the remaining shipments are checked to see if they meet the unloading criteria.

A shipment's status is "created" when first created, "bag loaded" when bagged, and "emptyed" when unloaded at the delivery point.

Packages loaded in a bag have the same delivery point as the bag.

When a shipment is loaded into the bag, the status of the shipment is updated to "loaded in bag" while the status of the bag remains "created".

If the bag itself has been emptied while the packages are still inside, the status of the bag and all packages inside is updated to "empty".

The vehicles go to the delivery points allocated to them and are unloaded from the relevant delivery points of the relevant shipments.
