   M S B A M L   `   `   `  ���� C  ?Database, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:Database.View.Pages.TablesDatabase.View.Pages.Tables  C&clr-namespace:Database.VeiwModel.PagesDatabase.VeiwModel.Pages  /    )Database.View.Pages.Tables.DeliverProduct   \ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     :local(clr-namespace:Database.View.Pages.Tables     8pages&clr-namespace:Database.VeiwModel.Pages       N� Title$  DeliverProduct�� N� 
Background
 ����� � DataContext :  4Database.VeiwModel.Pages.PageDeliverProductViewModel   � 	Resources    Btn �� 
TargetType �� Property �� Value��%c   &	        �� " ��.���  � ��$ 
10,0,10,10q���  � ��
 ���� ��  � ��$ 20(�.���  � RowDefinitions �� $	��37* ��� $
��362* ��� $	��31* �.+��� $��	10,10,0,0q�$��Center=�  HeadTextBlock#��[   Big#��[ 53Таблица: Товары поставщиков&   System.Windows.Controls.DataGrid     CanUserAddRows$ False��	   AutoGenerateColumns$	 False��$��1��$��3��$��10,10,10,10q�
������ ���� *DeliverProductList+
 �� SelectedItem
 �� *SelectedDeliverProduct+   Columns 0  *System.Windows.Controls.DataGridTextColumn    HeaderCenterDataGrid,  &System.Windows.Controls.DataGridColumn   HeaderStyle# [    Header$ Id��   Width5  /System.Windows.Controls.DataGridLengthConverter$ 50 1  +System.Windows.Controls.DataGridBoundColumn   Binding ��  �� Path$ Id�  # [ $ 
Товар��$
 300*  �� $ Product.Name�  # [ $ Поставщик��$
 300*  �� $ Deliver.Name�� $��2�� � ColumnDefinitions �� �� .+��� $��2��$��0��$#��Добавить запись��$��
10,0,10,10q�����   
BlueButton �� Source# [ ���� *
AddCommand+�� $��2��$��2��$!��Удалить запись��$��
10,0,10,10q����� # [ ���� *RemoveCommand+