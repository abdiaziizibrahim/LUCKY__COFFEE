using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LuckyCoffee
{
    public partial class Form1 : Form
    {
         const double TeaPrice = 1.00;
        const double CoffeePrice = 2.00;
        const double JuicePrice = 3.00;
        const double DiscountRate = 0.10;

        double total = 0;
        string luckyMessage = "No prize this time";

    
        public Form1()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
                        string customername = txtcustomername.Text;

            if (customername == "")
            {


                MessageBox.Show("Customer name is required");
                return;
            }
            if (!customername.All(char.IsLetter))
            {
                MessageBox.Show("Name must contain only letters");
                return;
            }
            




            int quantity;
            try
            {
                quantity = int.Parse(txtQuantity.Text);
            }
            catch
            {
                MessageBox.Show("Please enter a valid quantity");
                return;
            }
            if (quantity < 1)
            {
                MessageBox.Show("Quantity must be greater than zero.");
            }

            double drinkPrice = 0;
            string drinkName = "";
            if (radioTea.Checked)
            {
                drinkPrice = TeaPrice;
                drinkName = "Tea";
            }
            else if (radioCoffee.Checked)
            {
                drinkPrice = CoffeePrice;
                drinkName = "Coffee";
            }
            else if (radioJuice.Checked)
            {
                drinkPrice = JuicePrice;
                drinkName = "Juice";

            }
            else
            {
                MessageBox.Show("Please choose a drink before continuing.");
                return;
            }
            listorder.Items.Clear();
            Random random = new Random();
            Boolean gotPrize = false;
            int prizeNumber = 0;

            for (int i = 1; i <= quantity; i++)
            {
                int luckyNumber = random.Next(1, 51);
                if (luckyNumber == 7 || luckyNumber == 17)
                {
                    gotPrize = true;
                    prizeNumber = luckyNumber;
                }
                string order = i + ") " + customername + " _ " + drinkName +
                    " _ lucky " + luckyNumber + " _ $" + drinkPrice.ToString("0.00");
                listorder.Items.Add(order);
            }
            total = drinkPrice * quantity;
            double totalBeforeDiscount = total;
            if (checkedStudent.Checked)
            {
                total = total - (total * DiscountRate);
                listorder.Items.Add("Member Discount 10%");
            }
            listorder.Items.Add("Total Before Discount $" +
                totalBeforeDiscount.ToString("0.00"));
            listorder.Items.Add("Total After Discount $" + total.ToString("0.00"));



            lbltotal.Text = "Total price : "  + total.ToString("c2");

            if (gotPrize == true)
            {
                luckyMessage = " Lucky Message : Congratulations! You won a free cookie!";
                lbllucky.Text = luckyMessage;
                listorder.Items.Add(" You got a free cookie!" + "(" + prizeNumber + ")");


            }
            else
            {
                luckyMessage = "Sorry! Better luck next time.";
                lbllucky.Text = luckyMessage;
            }
        }

        private void BtnsaveFile_Click(object sender, EventArgs e)
        {
        
            if (listorder.Items.Count == 0)
            {
                MessageBox.Show("There is no order available to save.");
                return;
            }
            try
            {
                StreamWriter outputFile;
                outputFile = File.AppendText("Order.txt");
                for (int i = 0; i < listorder.Items.Count; i++)
                {
                    outputFile.WriteLine(listorder.Items[i]);
                }
                outputFile.WriteLine("Total price :" + total.ToString("0.00"));
                outputFile.WriteLine("Lucky message " + luckyMessage);
                outputFile.WriteLine("------------------------------");
                outputFile.Close();
                MessageBox.Show("Your order has been saved successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

       
            private void btnLoad_Click(object sender, EventArgs e)
        {
            if (File.Exists("Order.txt") == false)
            {
                MessageBox.Show("No file foud");
                return;
            }
            try
            {
                listorder.Items.Clear();
                StreamReader inputFile;
                inputFile = File.OpenText("Order.txt");
                while (inputFile.EndOfStream == false)
                {
                    string line = inputFile.ReadLine();
                    listorder.Items.Add(line);

                }
                inputFile.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void btnload_Click_1(object sender, EventArgs e)
        {
            if (File.Exists("Order.txt") == false)
            {
                MessageBox.Show("No file foud");
                return;
            }
            try
            {
                listorder.Items.Clear();
                StreamReader inputFile;
                inputFile = File.OpenText("Order.txt");
                while (inputFile.EndOfStream == false)
                {
                    string line = inputFile.ReadLine();
                    listorder.Items.Add(line);

                }
                inputFile.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtcustomername.Clear();
            txtQuantity.Clear();
            listorder.Items.Clear();
            lbltotal.Text = "";
            lbllucky.Text = "";
            checkedStudent.Checked = false;
            radioCoffee.Checked = false;
            radioJuice.Checked = false;
            radioTea.Checked = false;

            txtcustomername.Focus();

        }
    }
}



