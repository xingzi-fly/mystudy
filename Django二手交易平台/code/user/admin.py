from django.contrib import admin

# Register your models here.
from user.models import UserInfo


class UserInfoAdmin(admin.ModelAdmin):
    list_per_pag=15
    list_display = ['id','username','password','number']
    list_filter = ['username']
    search_fields = ['username']
admin.site.register(UserInfo , UserInfoAdmin)