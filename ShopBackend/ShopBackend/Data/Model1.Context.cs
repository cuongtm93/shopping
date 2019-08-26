﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ShopBackend.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class shop2Entities : DbContext
    {
        public shop2Entities()
            : base("name=shop2Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<oc_address> oc_address { get; set; }
        public virtual DbSet<oc_api> oc_api { get; set; }
        public virtual DbSet<oc_api_ip> oc_api_ip { get; set; }
        public virtual DbSet<oc_api_session> oc_api_session { get; set; }
        public virtual DbSet<oc_attribute> oc_attribute { get; set; }
        public virtual DbSet<oc_attribute_description> oc_attribute_description { get; set; }
        public virtual DbSet<oc_attribute_group> oc_attribute_group { get; set; }
        public virtual DbSet<oc_attribute_group_description> oc_attribute_group_description { get; set; }
        public virtual DbSet<oc_banner> oc_banner { get; set; }
        public virtual DbSet<oc_banner_image> oc_banner_image { get; set; }
        public virtual DbSet<oc_cart> oc_cart { get; set; }
        public virtual DbSet<oc_category> oc_category { get; set; }
        public virtual DbSet<oc_category_description> oc_category_description { get; set; }
        public virtual DbSet<oc_category_filter> oc_category_filter { get; set; }
        public virtual DbSet<oc_category_path> oc_category_path { get; set; }
        public virtual DbSet<oc_category_to_layout> oc_category_to_layout { get; set; }
        public virtual DbSet<oc_category_to_store> oc_category_to_store { get; set; }
        public virtual DbSet<oc_country> oc_country { get; set; }
        public virtual DbSet<oc_coupon> oc_coupon { get; set; }
        public virtual DbSet<oc_coupon_category> oc_coupon_category { get; set; }
        public virtual DbSet<oc_coupon_history> oc_coupon_history { get; set; }
        public virtual DbSet<oc_coupon_product> oc_coupon_product { get; set; }
        public virtual DbSet<oc_currency> oc_currency { get; set; }
        public virtual DbSet<oc_custom_field> oc_custom_field { get; set; }
        public virtual DbSet<oc_custom_field_customer_group> oc_custom_field_customer_group { get; set; }
        public virtual DbSet<oc_custom_field_description> oc_custom_field_description { get; set; }
        public virtual DbSet<oc_custom_field_value> oc_custom_field_value { get; set; }
        public virtual DbSet<oc_custom_field_value_description> oc_custom_field_value_description { get; set; }
        public virtual DbSet<oc_customer> oc_customer { get; set; }
        public virtual DbSet<oc_customer_activity> oc_customer_activity { get; set; }
        public virtual DbSet<oc_customer_affiliate> oc_customer_affiliate { get; set; }
        public virtual DbSet<oc_customer_approval> oc_customer_approval { get; set; }
        public virtual DbSet<oc_customer_group> oc_customer_group { get; set; }
        public virtual DbSet<oc_customer_group_description> oc_customer_group_description { get; set; }
        public virtual DbSet<oc_customer_history> oc_customer_history { get; set; }
        public virtual DbSet<oc_customer_ip> oc_customer_ip { get; set; }
        public virtual DbSet<oc_customer_login> oc_customer_login { get; set; }
        public virtual DbSet<oc_customer_online> oc_customer_online { get; set; }
        public virtual DbSet<oc_customer_reward> oc_customer_reward { get; set; }
        public virtual DbSet<oc_customer_search> oc_customer_search { get; set; }
        public virtual DbSet<oc_customer_transaction> oc_customer_transaction { get; set; }
        public virtual DbSet<oc_customer_wishlist> oc_customer_wishlist { get; set; }
        public virtual DbSet<oc_download> oc_download { get; set; }
        public virtual DbSet<oc_download_description> oc_download_description { get; set; }
        public virtual DbSet<oc_event> oc_event { get; set; }
        public virtual DbSet<oc_extension> oc_extension { get; set; }
        public virtual DbSet<oc_extension_install> oc_extension_install { get; set; }
        public virtual DbSet<oc_extension_path> oc_extension_path { get; set; }
        public virtual DbSet<oc_filter> oc_filter { get; set; }
        public virtual DbSet<oc_filter_description> oc_filter_description { get; set; }
        public virtual DbSet<oc_filter_group> oc_filter_group { get; set; }
        public virtual DbSet<oc_filter_group_description> oc_filter_group_description { get; set; }
        public virtual DbSet<oc_geo_zone> oc_geo_zone { get; set; }
        public virtual DbSet<oc_googleshopping_category> oc_googleshopping_category { get; set; }
        public virtual DbSet<oc_googleshopping_product> oc_googleshopping_product { get; set; }
        public virtual DbSet<oc_googleshopping_product_status> oc_googleshopping_product_status { get; set; }
        public virtual DbSet<oc_googleshopping_product_target> oc_googleshopping_product_target { get; set; }
        public virtual DbSet<oc_googleshopping_target> oc_googleshopping_target { get; set; }
        public virtual DbSet<oc_information> oc_information { get; set; }
        public virtual DbSet<oc_information_description> oc_information_description { get; set; }
        public virtual DbSet<oc_information_to_layout> oc_information_to_layout { get; set; }
        public virtual DbSet<oc_information_to_store> oc_information_to_store { get; set; }
        public virtual DbSet<oc_language> oc_language { get; set; }
        public virtual DbSet<oc_layout> oc_layout { get; set; }
        public virtual DbSet<oc_layout_module> oc_layout_module { get; set; }
        public virtual DbSet<oc_layout_route> oc_layout_route { get; set; }
        public virtual DbSet<oc_length_class> oc_length_class { get; set; }
        public virtual DbSet<oc_length_class_description> oc_length_class_description { get; set; }
        public virtual DbSet<oc_location> oc_location { get; set; }
        public virtual DbSet<oc_manufacturer> oc_manufacturer { get; set; }
        public virtual DbSet<oc_manufacturer_to_store> oc_manufacturer_to_store { get; set; }
        public virtual DbSet<oc_marketing> oc_marketing { get; set; }
        public virtual DbSet<oc_modification> oc_modification { get; set; }
        public virtual DbSet<oc_module> oc_module { get; set; }
        public virtual DbSet<oc_option> oc_option { get; set; }
        public virtual DbSet<oc_option_description> oc_option_description { get; set; }
        public virtual DbSet<oc_option_value> oc_option_value { get; set; }
        public virtual DbSet<oc_option_value_description> oc_option_value_description { get; set; }
        public virtual DbSet<oc_order> oc_order { get; set; }
        public virtual DbSet<oc_order_history> oc_order_history { get; set; }
        public virtual DbSet<oc_order_option> oc_order_option { get; set; }
        public virtual DbSet<oc_order_product> oc_order_product { get; set; }
        public virtual DbSet<oc_order_recurring> oc_order_recurring { get; set; }
        public virtual DbSet<oc_order_recurring_transaction> oc_order_recurring_transaction { get; set; }
        public virtual DbSet<oc_order_shipment> oc_order_shipment { get; set; }
        public virtual DbSet<oc_order_status> oc_order_status { get; set; }
        public virtual DbSet<oc_order_total> oc_order_total { get; set; }
        public virtual DbSet<oc_order_voucher> oc_order_voucher { get; set; }
        public virtual DbSet<oc_product> oc_product { get; set; }
        public virtual DbSet<oc_product_attribute> oc_product_attribute { get; set; }
        public virtual DbSet<oc_product_description> oc_product_description { get; set; }
        public virtual DbSet<oc_product_discount> oc_product_discount { get; set; }
        public virtual DbSet<oc_product_filter> oc_product_filter { get; set; }
        public virtual DbSet<oc_product_image> oc_product_image { get; set; }
        public virtual DbSet<oc_product_option> oc_product_option { get; set; }
        public virtual DbSet<oc_product_option_value> oc_product_option_value { get; set; }
        public virtual DbSet<oc_product_recurring> oc_product_recurring { get; set; }
        public virtual DbSet<oc_product_related> oc_product_related { get; set; }
        public virtual DbSet<oc_product_reward> oc_product_reward { get; set; }
        public virtual DbSet<oc_product_special> oc_product_special { get; set; }
        public virtual DbSet<oc_product_to_category> oc_product_to_category { get; set; }
        public virtual DbSet<oc_product_to_download> oc_product_to_download { get; set; }
        public virtual DbSet<oc_product_to_layout> oc_product_to_layout { get; set; }
        public virtual DbSet<oc_product_to_store> oc_product_to_store { get; set; }
        public virtual DbSet<oc_recurring> oc_recurring { get; set; }
        public virtual DbSet<oc_recurring_description> oc_recurring_description { get; set; }
        public virtual DbSet<oc_return> oc_return { get; set; }
        public virtual DbSet<oc_return_action> oc_return_action { get; set; }
        public virtual DbSet<oc_return_history> oc_return_history { get; set; }
        public virtual DbSet<oc_return_reason> oc_return_reason { get; set; }
        public virtual DbSet<oc_return_status> oc_return_status { get; set; }
        public virtual DbSet<oc_review> oc_review { get; set; }
        public virtual DbSet<oc_seo_url> oc_seo_url { get; set; }
        public virtual DbSet<oc_session> oc_session { get; set; }
        public virtual DbSet<oc_setting> oc_setting { get; set; }
        public virtual DbSet<oc_shipping_courier> oc_shipping_courier { get; set; }
        public virtual DbSet<oc_statistics> oc_statistics { get; set; }
        public virtual DbSet<oc_stock_status> oc_stock_status { get; set; }
        public virtual DbSet<oc_store> oc_store { get; set; }
        public virtual DbSet<oc_tax_class> oc_tax_class { get; set; }
        public virtual DbSet<oc_tax_rate> oc_tax_rate { get; set; }
        public virtual DbSet<oc_tax_rate_to_customer_group> oc_tax_rate_to_customer_group { get; set; }
        public virtual DbSet<oc_tax_rule> oc_tax_rule { get; set; }
        public virtual DbSet<oc_theme> oc_theme { get; set; }
        public virtual DbSet<oc_translation> oc_translation { get; set; }
        public virtual DbSet<oc_upload> oc_upload { get; set; }
        public virtual DbSet<oc_user> oc_user { get; set; }
        public virtual DbSet<oc_user_group> oc_user_group { get; set; }
        public virtual DbSet<oc_voucher> oc_voucher { get; set; }
        public virtual DbSet<oc_voucher_history> oc_voucher_history { get; set; }
        public virtual DbSet<oc_voucher_theme> oc_voucher_theme { get; set; }
        public virtual DbSet<oc_voucher_theme_description> oc_voucher_theme_description { get; set; }
        public virtual DbSet<oc_weight_class> oc_weight_class { get; set; }
        public virtual DbSet<oc_weight_class_description> oc_weight_class_description { get; set; }
        public virtual DbSet<oc_zone> oc_zone { get; set; }
        public virtual DbSet<oc_zone_to_geo_zone> oc_zone_to_geo_zone { get; set; }
        public virtual DbSet<product_product_desc_view> product_product_desc_view { get; set; }
    }
}