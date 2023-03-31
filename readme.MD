To run this you need:

1. Specify **authKey** and **secretKey** of your api from your dadata account
2. Write correct cors settings for your environment

All this steps can be done in `appsettings.json`

Instead of using kestrel server like first server with cors it will be better to use it behind some reverse proxy like **nginx** with enabled cors if its needed.