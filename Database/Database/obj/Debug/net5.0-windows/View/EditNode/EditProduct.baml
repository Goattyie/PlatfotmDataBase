   M S B A M L   `   `   `  ����C  ?Database, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:Database.View.EditNodeDatabase.View.EditNode  5   	   G(clr-namespace:Database.View.Pages.TablesDatabase.View.Pages.Tables  6<   I)clr-namespace:Database.VeiwModel.EditNodeDatabase.VeiwModel.EditNode  6t   (    "Database.View.EditNode.EditProduct   5   	   \ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6local$clr-namespace:Database.View.EditNode     5   	   ;local1(clr-namespace:Database.View.Pages.Tables     6<   >editnode)clr-namespace:Database.VeiwModel.EditNode     6t     � Title$  Товары��5   	   $	��352��6   $	��445��6%   .��5
      � $��10q�6    � RowDefinitions 5   
   �� 5      �� 5      �� 5      �� 5      �� 5      .+�5   
   ��    TextBoxWithWatermark#��[  5      
 � Tag$# Название товара��5      $��	0,12,0,12q�5      ��5      �� *Name+ �� ValidatesOnDataErrors$
 True�� �� ValidatesOnExceptions$
 True�� �� UpdateSourceTrigger$ PropertyChanged=�� 5   
   $��1��6    5      �� 5      �� 5      .+�5      ��   HeadTextBlock#��[ 5       $��Цена закупки��5!      $��Center=�5"      $��Center=�5#      �� 5%      $��1��5&      #��[  5'      $ Цена закупки��5(      ��5)      �� *	OrderCost+$
 True��$
 True��$ PropertyChanged=�5*      � 5,   
   $��2��6    5-      �� 5.      �� 5/      .+�52      �� #��[ 53      $$��Цена с доставкой��54      $��Center=�55      $��Center=�56      �� 58      $��1��59      #��[  5:      $$ Цена с доставкой��5;      ��5<      �� *DeliverCost+$
 True��$
 True��$ PropertyChanged=�5=      � 5?   
   $��3��6    5@      �� 5A      �� 5B      .+�5E      �� #��[ 5F      $��Цена продажи��5G      $��Center=�5H      $��Center=�5I      �� 5K      $��1��5L      #��[  5M      $ Цена продажи��5N      ��5O      �� *
SellCost+$
 True��$
 True��$ PropertyChanged=�5P      �� 5R   
   -   5        $��4��5S        
BaseButton#��[ 5T      $��Сохранить��5U      $��Center=�5W      $��Center=�5X      ��5V      �� *AddButtonClick+{�5Y      �� *	IsValid+6+   5[      5\      