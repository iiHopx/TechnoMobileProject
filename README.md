# TechnoMobileProject

## Overview
This project includes several admin and customer pages with various functionalities.

## Pages

### Add Admin Page
![Add Admin Page](images/ADDADMIN%20PAGE.png)
A form for adding new administrators. Should include validation for username existence. Role automatically set to "admin". Success redirects to the index page.

### Admin Home Page
![Admin Home Page](images/ADMIN%20HOME%20PAGE.png)
Welcome title with a navigation bar including: home, Manage Items, Send email, Customer Search, Report Page, dashboard, Add Admin, User Management, Logout, and an administrative dashboard layout.

### Buy Page
![Buy Page](images/BUYPAGE.png)
Shows item details with quantity input and JavaScript calculation for total price (price * quantity). Quantity validation against available stock. Buy button to add to cart.

### Cart Buy
![Cart Buy](images/CART%20BUY.png)
Shopping cart view listing selected items, showing quantities and total prices, and option to proceed with purchase.

### Catalogue
![Catalogue](images/CATALOGUE.png)
Grid/list of available items with clickable item names linking to the Buy page. Shows prices and basic item info.

### Dashboard Page
![Dashboard Page](images/DASHBOARDPAGE.png)
Pie chart showing category item counts. Cards showing total items count and sum of quantities from order table. Statistical overview.

### Detail Items
![Detail Items](images/DETAIL%20ITEMS.png)
Detailed view of a single item with complete item information. Used in modal view for item management.

### Homepage Customer
![Homepage Customer](images/HOMEPAGECUSTOMER.png)
Welcome message with customer name, shows discounted items, footer with contact details (email, phone), and customer-specific navigation bar.

### Item List
![Item List](images/ITEMLIST.png)
Items table ordered by Category. List view of all items. Admin sees full management controls. Customers see limited view.

### Manage Items Page
![Manage Items Page](images/MANAGE%20ITEMS%20PAGE.png)
CRUD operations for items with image upload capability. Uses partial views with modal effects. Full item management interface.

### Order Details More
![Order Details More](images/ORDERDETAILSMORE.png)
Detailed view of order lines showing individual items in an order with quantities and prices.

### Order Details Page
![Order Details Page](images/ORDERDETAILSPAGE.png)
Overview of customer orders with order history and details in a date-ordered view.

### Order Report Page
![Order Report Page](images/ORDERREPORT%20PAGE.png)
Shows all customers' total purchase amounts with clickable details link per customer. Summary of order statistics.

### Place Order
![Place Order](images/PLACEORDER.png)
Final order confirmation page with order summary before purchase and submit order functionality.

### Register
![Register](images/REGISTER.png)
Customer registration form with required field validation and password comparison validation. Success redirects to customer home.

### Send Email Page
![Send Email Page](images/SEND%20EMAIL%20PAGE.png)
Interface for sending welcome emails with an email composition form and customer email selection.

### User Search Page
![User Search Page](images/USERSEARCHPAGE.png)
Cascading dropdown lists for role/name using API for name retrieval. Shows user details on search. Role-based user search.

### Login Page
![Login Page](images/Untitled.png)
The login page for the project.
