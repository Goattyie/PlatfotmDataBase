   M S B A M L   `   `   `  ���� C  ?Database, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:Database.ViewDatabase.View      Database.View.OrderWindow   \ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -localclr-namespace:Database.View       � Title$  Заказы��$	��768��$
��1366��.��� $��10q� � ColumnDefinitions �� $	��25* ��� $
��107* �.+��  � RowDefinitions �� $
��373* ��� $	��21* ��� $	��21* ��� $	��21* ��� $	��21* �.+��� ���� *	OrderList+ �� SelectedItem �� *SelectedOrder+���� .R��� .+��� ���� *Id+ �� StringFormat$ Заказ №{0}���� ���� *Date+$+ %Дата заказа: {0:dd/MM/yyyy}���� ���� *
OrderSum+$$ Сумма закупки: {0}���� ���� *
DeliverSum+$&  Сумма доставки: {0}���� ���� *AllSum+$  Общая сумма: {0}���� $��1��   
BaseButton#��[   
 Medium#��[ $!��Добавить заказ������ *AddOrderCommand+{��� *
isActive+�� $��2��#��[  #��[ $$��Добавить в архив������ *AddOrderToArchiveCommand+{��� *
isActive+�� $��3��#��[  #��[ $��Вывод в Excel������ *ExcelExportCommand+�� $��4��#��[  #��[ $��Удалить заказ������ *RemoveOrderCommand+� $��1�� �� $	��67* ��� $��5* �.+��  �� �� �� �� &   System.Windows.Controls.DataGrid     CanUserAddRows$ False��   AutoGenerateColumns$ False��$��10,10,10,10q�
������ ���� *OrderNodesList+ �� *SelectedOrderNode+   Columns 0  *System.Windows.Controls.DataGridTextColumn    HeaderCenterDataGrid,  &System.Windows.Controls.DataGridColumn   HeaderStyle# [ 	   Header$	 
Товар��
   Width5  /System.Windows.Controls.DataGridLengthConverter$	
 400 1  +System.Windows.Controls.DataGridBoundColumn   Binding ��  �� Path$ Product.Name�  # [   CenterGridColumn   	CellStyle# [ $	 Количество��$	
 100  �� $ Count�  # [ # [ $	 Получено��$	
 100  �� $ CurrentCount�  # [ # [ $	 Цена закупки��$	
 100  �� $ 	OrderCost�  # [ # [ $	 Цена доставки��$	
 100  �� $ DeliverCost�  # [ # [ $	 Цена оплаты��$	
 100  �� $ SummCost�  # [ # [ $	 Оплачено��$	
 100  �� $ CurrentCost�  # [ # [ $	 Поставщик��$	
 140  �� $ Deliver.Name�  # [ # [ $	 Дата заказа��$	
 140  �� $ 	OrderDate�$ dd/MM/yy��� $��1�� �� �� �� �� .+��� $��2��$��0��$#��Добавить запись��$��
10,0,10,10q�{��� *
isActive+����   
BlueButton �� Source# [ ���� *
AddCommand+�� $��2��$��1��$ ��Редактировать��$��
10,0,10,10q�{��� *
isActive+���� # [ ���� *EditCommand+�� $��2��$��2��$!��Удалить запись��$��
10,0,10,10q�{��� *
isActive+���� # [ ���� *RemoveCommand+�� $��2��$��3��$��Принять товар��$��
10,0,10,10q�{��� *
isActive+���� # [ ���� *AcceptCommand+