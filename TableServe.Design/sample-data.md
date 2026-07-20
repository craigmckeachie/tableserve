# TableServe Design — Sample Data Reference

Dummy data used across the `TableServe.Design` static HTML pages. TableServe has no
seed-data source (unlike PRS's `Prs.Api` seed script) — all data below is invented,
realistic restaurant data, kept consistent across every page.

Signed-in staff member shown in the header/nav on every page: **Jordan Reyes** (Manager + Admin).

---

## signin.html

Standalone page, no header/nav. Fields: Username, Password, "Forgot It?" link, Sign In button.
No sample data needed — fields render empty. Brand: TableServe logo + "TableServe" name.

---

## categories.html

Card grid. Each card shows: Name, Sort Order.

| Id | Name | Sort Order |
|---|---|---|
| 1 | Appetizers | 1 |
| 2 | Entrees | 2 |
| 3 | Sides | 3 |
| 4 | Desserts | 4 |
| 5 | Drinks | 5 |

Delete confirm modal text: "Are you sure you want to delete this category?"

## category-create.html / category-edit.html

Create: blank Name, Sort Order defaults to 0.
Edit: pre-populated for Appetizers (Name = "Appetizers", Sort Order = 1).

---

## menuitems.html

Card grid. Each card shows: Name, Price, Category name, price badge.

| Id | Name | Price | Category |
|---|---|---|---|
| 1 | Loaded Nachos | $9.99 | Appetizers |
| 2 | Mozzarella Sticks | $7.49 | Appetizers |
| 3 | Buffalo Wings | $11.99 | Appetizers |
| 4 | Grilled Salmon | $18.99 | Entrees |
| 5 | Ribeye Steak | $24.99 | Entrees |
| 6 | Chicken Alfredo | $15.99 | Entrees |
| 7 | Garlic Fries | $5.49 | Sides |
| 8 | Side Caesar Salad | $4.99 | Sides |
| 9 | Chocolate Lava Cake | $7.99 | Desserts |
| 10 | New York Cheesecake | $6.99 | Desserts |
| 11 | Iced Tea | $2.99 | Drinks |
| 12 | Craft Lemonade | $3.49 | Drinks |

Delete confirm modal text: "Are you sure you want to delete this menu item?"

## menuitem-create.html / menuitem-edit.html

Create: blank Name, blank Price, unselected Category.
Edit: pre-populated for Loaded Nachos (Name = "Loaded Nachos", Price = 9.99, Category = Appetizers).

---

## staff.html

Card grid. Each card shows: avatar circle with initials, Full Name, Username, Phone, Email,
IsManager badge (shown only if true), IsAdmin badge (shown only if true).

| Initials | Name | Username | Manager | Admin | Phone | Email |
|---|---|---|---|---|---|---|
| JR | Jordan Reyes | jreyes | Yes | Yes | (555) 123-4567 | jordan.reyes@tableserve.example |
| CN | Casey Nguyen | cnguyen | Yes | No | (555) 234-5678 | casey.nguyen@tableserve.example |
| ME | Morgan Ellis | mellis | No | Yes | (555) 345-6789 | morgan.ellis@tableserve.example |
| RT | Riley Thompson | rthompson | No | No | (555) 456-7890 | riley.thompson@tableserve.example |
| AB | Avery Brooks | abrooks | Yes | No | (555) 567-8901 | avery.brooks@tableserve.example |
| PD | Peyton Diaz | pdiaz | No | No | (555) 678-9012 | peyton.diaz@tableserve.example |
| SC | Sam Coleman | scoleman | No | Yes | (555) 789-0123 | sam.coleman@tableserve.example |
| TB | Taylor Brennan | tbrennan | No | No | (555) 890-1234 | taylor.brennan@tableserve.example |

Delete confirm modal text: "Are you sure you want to delete this staff member?"

## staff-create.html / staff-edit.html

Create: all fields blank, Manager/Admin checkboxes unchecked.
Edit: pre-populated for Jordan Reyes (First Name = Jordan, Last Name = Reyes,
Email = jordan.reyes@tableserve.example, Phone = 5551234567, Username = jreyes,
Manager checked, Admin checked).

---

## orders.html

Status filter dropdown: All | Placed | Preparing | Ready | Served | Cancelled.
Table columns: Id | Table Number | Notes (truncated) | Status | Total | Staff | Ordered At | actions (View / Edit / Delete).

| Id | Table Number | Notes | Status | Total | Staff | Ordered At |
|---|---|---|---|---|---|---|
| 201 | 4 | No onions please | PLACED | $23.98 | Casey Nguyen | 6:05 PM |
| 202 | 12 | — | PREPARING | $42.95 | Avery Brooks | 6:12 PM |
| 203 | 7 | Birthday - bring candle | READY | $18.99 | Jordan Reyes | 6:20 PM |
| 204 | 2 | — | SERVED | $32.98 | Casey Nguyen | 5:45 PM |
| 205 | 9 | Allergic to peanuts | CANCELLED | $15.99 | Avery Brooks | 5:30 PM |
| 206 | 15 | — | PLACED | $9.99 | Jordan Reyes | 6:30 PM |
| 207 | 3 | Extra napkins | PREPARING | $27.48 | Casey Nguyen | 6:15 PM |
| 208 | 6 | — | SERVED | $45.97 | Avery Brooks | 5:50 PM |

Order #205 is Cancelled with **CancellationReason**: "Guest left before order was ready"
(shown on `order-detail.html` only when status is Cancelled — not depicted on the sample
detail page below, which uses order #202 instead).

Status badge colors: Placed = secondary (grey), Preparing = warning (yellow),
Ready = info (blue), Served = success (green), Cancelled = danger (red).

Delete confirm modal text: "Are you sure you want to delete this order?"

## order-create.html

Blank form. Table Number, Notes (optional), Status (select, disabled, defaults to Placed),
Staff (select, always disabled, defaults to the signed-in staff member — Jordan Reyes).

Staff dropdown options: Jordan Reyes (selected), Casey Nguyen, Morgan Ellis, Riley Thompson,
Avery Brooks, Peyton Diaz, Sam Coleman, Taylor Brennan.

## order-edit.html

Pre-populated for Order #207:

| Field | Value |
|---|---|
| Table Number | 3 |
| Notes | Extra napkins |
| Status | Preparing (select enabled on Edit) |
| Staff | Casey Nguyen (disabled) |

## order-detail.html

Shows Order #202 (Table 12), status PREPARING, staff Avery Brooks, ordered at 6:12 PM, no notes.

**Summary:**

| Table Number | Status | Staff |
|---|---|---|
| 12 | PREPARING (badge) | Avery Brooks |

| Notes | Total | Ordered At |
|---|---|---|
| — | $42.95 | 6:12 PM |

**Order Items table:**

| Menu Item | Price | Quantity | Notes | Amount |
|---|---|---|---|---|
| Ribeye Steak | $24.99 | 1 | Medium rare | $24.99 |
| Garlic Fries | $5.49 | 2 | — | $10.98 |
| Craft Lemonade | $3.49 | 2 | — | $6.98 |

Footer total: **$42.95**

Workflow buttons shown (status = Preparing): "Mark Ready" (primary) + "Cancel Order"
(outline-danger, opens cancel modal). Edit pencil icon always present.

**Cancel modal:** title "Cancel Order", required textarea labeled "Cancellation Reason",
Cancel + Confirm buttons.
**Delete order item modal:** "Are you sure you want to delete this order item?"

---

## orderitem-create.html

Empty/default state: Menu Item = "Select..." (unselected), Price = $0.00, Quantity = 0,
Notes = empty, Amount = $0.00.

Menu Item dropdown lists all 12 menu items from the `menuitems.html` table above.

## orderitem-edit.html

Editing the "Garlic Fries" line from Order #202:
Menu Item = Garlic Fries (selected), Price = $5.49, Quantity = 2, Notes = empty, Amount = $10.98.

---

## Shared header (all pages except signin)

Signed-in staff: **Jordan Reyes**, initials "JR", dropdown with Settings, Profile action, Sign out.
Brand: TableServe logo (orange triangles) + "TableServe" text in `#FF7A00`.

## Shared nav (all pages except signin)

"Serve" section label, links: Orders, Menu Items, Categories, Staff.
