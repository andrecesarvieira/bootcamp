using DesafioPOO.Models;

Console.Clear();

Console.WriteLine("Nokia:");
Smartphone nokia = new Nokia("123456789", "Nokia 3310", "000000000000001", 256);
nokia.Ligar();
nokia.ReceberLigacao();
nokia.InstalarAplicativo("WhatsApp");

Console.WriteLine("\niPhone:");
Smartphone iphone = new Iphone("987654321", "iPhone 13", "000000000000002", 256);
iphone.Ligar();
iphone.ReceberLigacao();
iphone.InstalarAplicativo("Instagram");
