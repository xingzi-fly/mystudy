from django.urls import path

from . import views


urlpatterns = [
    path('login/', views.login),
    path('register/',views.register),
    path('register_handle/',views.register_handle),
    path('login_handle/',views.login_handle),
    path('logout/',views.logout),
    path('user_center/',views.user_center),
    path('reset_password/',views.reset_password),
    path('release_records/',views.release_records),
    path('purchase_record/',views.purchase_record),
    path('collection/',views.collection),
    path('browse_history/',views.browse_history),
    path('c_password/',views.c_password),
    path('logon_record/',views.logon_record),
    path('cart/',views.cart),
    path('delete_cart/',views.delete_cart),

    # path(r'^login/$', views.login),
    # path(r'^register/$',views.register),
    # path(r'^register_handle/$',views.register_handle),
    # path(r'^login_handle/$',views.login_handle),
    # path(r'^logout/$',views.logout),
    # path(r'^user_center/$',views.user_center),
    # path(r'^reset_password/$',views.reset_password),
    # path(r'^release_records/$',views.release_records),
    # path(r'^purchase_record/$',views.purchase_record),
    # path(r'^collection/$',views.collection),
    # path(r'^browse_history/$',views.browse_history),
    # path(r'^c_password/$',views.c_password),
    # path(r'^logon_record/$',views.logon_record),
    # path(r'^cart/$',views.cart),
    # path(r'^delete_cart/$',views.delete_cart),
]