from django.contrib import admin

# Register your models here.
# Register your models here.
from goods.models import GoodsInfo


class GoodsInfoAdmin(admin.ModelAdmin):
    list_per_pag=15
    list_display = ['id','title','type','picture','price','address','description']
    list_filter = ['title']
    search_fields = ['title']

admin.site.register(GoodsInfo, GoodsInfoAdmin)