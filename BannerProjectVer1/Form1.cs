using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BannerProjectVer1
{
    public partial class Form1 : Form
    {

        Controller controller = new Controller();

        public Form1()
        {
            InitializeComponent();

            //run combobox fillers
            FillComboboxesColors();   
            FillComboboxCategorys();
            FillComboboxesPatterns();


            textBox_categorysNewName.Clear(); //because the selected indexs change event would write something weird there at startup otherwise
        }

        public void ShowSuccess(String message)
        {
            label_ResponsFeild.ForeColor = System.Drawing.Color.Black; //System.Drawing.Color.Black - instead of just Color.Black since I've got a class named Color too
            label_ResponsFeild.Text = message;
        }

        public void ShowError(String message)
        {
            label_ResponsFeild.ForeColor = System.Drawing.Color.Red; //System.Drawing.Color.Red - instead of just Color.Red since I've got a class named Color too
            label_ResponsFeild.Text = message;
        }

//DataSource Lists
        //To give each combobox with colors their own dataSource and not fill them all with the same color list so that all color choices need to be the same - that would not give meaningful banner patterns
        private List<Category> GetCategoryList()
        {
            List<Category> categorys = controller.ReadAllCategorys(); //TO BE CHANGED to ReadAllSelectableCategorys once aoutmatic categorys are automated

            return categorys;
        }

        private List<Color> GetColorList()
        {
            List<Color> colors = controller.ReadAllColors();

            return colors;
        }

        private List<Pattern> GetPatternList()
        {
            List<Pattern> patterns = controller.ReadAllPatterns();

            return patterns;
        }


 //Combobox fillers
        private void FillComboboxCategorys()
        {
            //comboBoxCategoryCategorys.Items.Clear(); not nessesary when populating combobox with dataSource it seems

            
            comboBoxCategoryCategorys.DataSource = GetCategoryList();
            // comboBoxBannerPrimCategory.DataSource = GetCategoryList();
            comboBoxBannerPrimCategory.DataSource = controller.ReadAllSelectableCategorys(); //temporary while GetCategoryList calls on ReadAllCategorys
            checkedListBoxBannerSecCategory.DataSource = GetCategoryList();
            
            comboBoxCategoryCategorys.DisplayMember = comboBoxBannerPrimCategory.DisplayMember =
               checkedListBoxBannerSecCategory.DisplayMember = "CategoryName";

            comboBoxCategoryCategorys.ValueMember = comboBoxBannerPrimCategory.ValueMember =
                checkedListBoxBannerSecCategory.ValueMember = "CategoryID";

        }

        private void FillComboboxesColors()
        {
            /* how we did in school project - worked when what we wanted to display was also the ID 
             * List<Color> colors = controller.ReadAllColors(); 
             * 
             * foreach (Color c in colors)
             {
                 comboBoxColors.Items.Add(c.ColorName);
                 comboBoxColors2.Items.Add(c.ColorName);
             }*/

            comboBoxBannerColor.DataSource = GetColorList();
            comboBoxBannerStep1Color.DataSource = GetColorList();
            comboBoxBannerStep2Color.DataSource = GetColorList();
            comboBoxBannerStep3Color.DataSource = GetColorList();
            comboBoxBannerStep4Color.DataSource = GetColorList();
            comboBoxBannerStep5Color.DataSource = GetColorList();
            comboBoxBannerStep6Color.DataSource = GetColorList();

            comboBoxBannerColor.DisplayMember = comboBoxBannerStep1Color.DisplayMember = comboBoxBannerStep2Color.DisplayMember =
               comboBoxBannerStep3Color.DisplayMember = comboBoxBannerStep4Color.DisplayMember = comboBoxBannerStep5Color.DisplayMember =
               comboBoxBannerStep6Color.DisplayMember = "ColorName";

            comboBoxBannerColor.ValueMember = comboBoxBannerStep1Color.ValueMember = comboBoxBannerStep2Color.ValueMember =
               comboBoxBannerStep3Color.ValueMember = comboBoxBannerStep4Color.ValueMember = comboBoxBannerStep5Color.ValueMember =
               comboBoxBannerStep6Color.ValueMember = "ColorID";
        }

        private void FillComboboxesPatterns()
        {
            comboBoxBannerStep1Pattern.DataSource = GetPatternList();
            comboBoxBannerStep2Pattern.DataSource = GetPatternList();
            comboBoxBannerStep3Pattern.DataSource = GetPatternList();
            comboBoxBannerStep4Pattern.DataSource = GetPatternList();
            comboBoxBannerStep5Pattern.DataSource = GetPatternList();
            comboBoxBannerStep6Pattern.DataSource = GetPatternList();

            comboBoxBannerStep1Pattern.DisplayMember = comboBoxBannerStep2Pattern.DisplayMember = comboBoxBannerStep3Pattern.DisplayMember =
                comboBoxBannerStep4Pattern.DisplayMember = comboBoxBannerStep5Pattern.DisplayMember =
                comboBoxBannerStep6Pattern.DisplayMember = "PatternName";

            comboBoxBannerStep1Pattern.ValueMember = comboBoxBannerStep2Pattern.ValueMember = comboBoxBannerStep3Pattern.ValueMember =
                comboBoxBannerStep4Pattern.ValueMember = comboBoxBannerStep5Pattern.ValueMember =
                comboBoxBannerStep6Pattern.ValueMember = "PatternID";
        }


    //Category buttons
        private void Button_AddCategory_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textBox_categoryCreateName.Text))
            {
                ShowError("Insert a name for your new category");
            }
            else
            {
                String catName = textBox_categoryCreateName.Text;

                try
                {
                    controller.CreateCategory(catName);

                    FillComboboxCategorys(); //Update the comboboxes

                    ShowSuccess("The category " + catName + " was successfully added");
                    textBox_categoryCreateName.Clear();
                }
                catch (Exception ex)
                {
                    ShowError("create category form error: " + ex.Message);  //to be done via exeption handler later
                }
            }

        }

        private void Button_DeleteCategory_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(comboBoxCategoryCategorys.Text))
            {
                ShowError("Select a category to delete");
            }
            else
            {
                try
                {
                    int categoryID = Convert.ToInt32(comboBoxCategoryCategorys.SelectedValue);
                    controller.DeleteCategory(categoryID);
                    //could ckeck if rows affected is 1 here
                    ShowSuccess("Category deleted");

                    FillComboboxCategorys(); //updatetes all category comboboxes
                }
                catch (Exception ex)
                {
                    ShowError(ex.Message); //to be done via exeption handler later
                }
                
            }
            
        }

        private void Button_UpdateCategory_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(comboBoxCategoryCategorys.Text))
            {
                ShowError("Select a category to delete");
            }
            else if (String.IsNullOrEmpty(textBox_categorysNewName.Text))
            {
                ShowError("Insert a name to update the category to");
            }
            else
            {
                try
                {
                    int categoryID = Convert.ToInt32(comboBoxCategoryCategorys.SelectedValue);
                    String categoryName = textBox_categorysNewName.Text;


                    controller.UpdateCategory(categoryID, categoryName);
                    //could ckeck if rows affected is 1 here
                    ShowSuccess("Category updated");

                    FillComboboxCategorys(); //updatetes all category comboboxes

                    textBox_categorysNewName.Clear();
                }
                catch (Exception ex)
                {
                    ShowError(ex.Message); //to be done via exeption handler later
                }
            }
        }

        //Fill the new name feild with the selected name so user can correct a typo without having to re-write the whole category name
        private void ComboBoxCategoryCategorys_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedCat = comboBoxCategoryCategorys.Text;
            textBox_categorysNewName.Text = selectedCat;
        }

/** BANNER TAB**/

//Banner Steps enablers 
        //Feels like this should be possible to do in a neater way with like one generic enabler 
        private void CheckBoxStep1_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxBannerStep1Color.Enabled = checkBoxStep1.Checked;
            comboBoxBannerStep1Pattern.Enabled = checkBoxStep1.Checked;
            checkBoxStep2.Enabled = checkBoxStep1.Checked;
        }

        private void CheckBoxStep2_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxBannerStep2Color.Enabled = checkBoxStep2.Checked;
            comboBoxBannerStep2Pattern.Enabled = checkBoxStep2.Checked;
            checkBoxStep3.Enabled = checkBoxStep2.Checked;
        }

        private void CheckBoxStep3_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxBannerStep3Color.Enabled = checkBoxStep3.Checked;
            comboBoxBannerStep3Pattern.Enabled = checkBoxStep3.Checked;
            checkBoxStep4.Enabled = checkBoxStep2.Checked;
        }

        private void CheckBoxStep4_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxBannerStep4Color.Enabled = checkBoxStep4.Checked;
            comboBoxBannerStep4Pattern.Enabled = checkBoxStep4.Checked;
            checkBoxStep5.Enabled = checkBoxStep4.Checked;
        }

        private void CheckBoxStep5_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxBannerStep5Color.Enabled = checkBoxStep5.Checked;
            comboBoxBannerStep5Pattern.Enabled = checkBoxStep5.Checked;
            checkBoxStep6.Enabled = checkBoxStep5.Checked;
        }

        private void CheckBoxStep6_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxBannerStep6Color.Enabled = checkBoxStep6.Checked;
            comboBoxBannerStep6Pattern.Enabled = checkBoxStep6.Checked;
        }
/**KNOWN BUG **/        //Need disablers too? If you enable all and then disable one the ones below will still be checked altho disabled
          
            
        //Add Banner including add bannerSteps and secondaryCategorys
        private void Button_addBanner_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textBoxBannerName.Text))
            {
                ShowError("Insert a name for your new banner");
            }
            else if (!checkBoxStep1.Checked)
            {
                ShowError("You probably want at least one step to your banner"); //I kinda wanna change this to a pup up with two alternatives that allowers the user to proceed to create a banner with no steps if they confirm they want that
            }
            else
            {
                //now to gather all info the DAL/Controller create Banner want (will want when Ive made them)
//put try here
                String bannerName = textBoxBannerName.Text;
                String bannerColor = Convert.ToString(comboBoxBannerColor.SelectedValue);
                String bannerPicture = "notCodedYetLOL";
                int bannerCategoryId = Convert.ToInt32(comboBoxBannerPrimCategory.SelectedValue);
                //not TryParse since it was an int when assignd as ValueMember and should not be possible to not be int, might change later for extra foolproof 

                var secondaryCategoryIDs = new List<int>(); //seems more effective to just send the IDs but this could also be a list of Category objects

                foreach(var itemChecked in checkedListBoxBannerSecCategory.CheckedItems)
                {
                    var category = (Category)itemChecked; //cast it as my class Category

                    int id = category.CategoryID;
                    secondaryCategoryIDs.Add(id);
                    
                }

                List<BannerStep> bannerSteps = new List<BannerStep>();

                //now to get the banner steps, in this nestled structure so the bug that allowers you to in the GUI add later steps with with erlier stpes unchecked don't affect database input
                if (checkBoxStep1.Checked)
                {
                    BannerStep temp = new BannerStep();
                    temp.BannerStepNumber = 1;
                    temp.BannerStepColorID = Convert.ToString(comboBoxBannerStep1Color.SelectedValue);
                    temp.BannerStepPatternID = Convert.ToString(comboBoxBannerStep1Pattern.SelectedValue);
                    bannerSteps.Add(temp);

                    if (checkBoxStep2.Checked)
                        {
                        BannerStep temp2 = new BannerStep();

                        temp2.BannerStepNumber = 2;
                        temp2.BannerStepColorID = Convert.ToString(comboBoxBannerStep2Color.SelectedValue);
                        temp2.BannerStepPatternID = Convert.ToString(comboBoxBannerStep2Pattern.SelectedValue);

                        bannerSteps.Add(temp2);

                        if (checkBoxStep3.Checked)
                        {
                            BannerStep temp3 = new BannerStep();
                            temp3.BannerStepNumber = 3;
                            temp3.BannerStepColorID = Convert.ToString(comboBoxBannerStep3Color.SelectedValue);
                            temp3.BannerStepPatternID = Convert.ToString(comboBoxBannerStep3Pattern.SelectedValue);

                            bannerSteps.Add(temp3);

                            if (checkBoxStep4.Checked)
                            {
                                BannerStep temp4 = new BannerStep();
                                temp4.BannerStepNumber = 4;
                                temp4.BannerStepColorID = Convert.ToString(comboBoxBannerStep4Color.SelectedValue);
                                temp4.BannerStepPatternID = Convert.ToString(comboBoxBannerStep4Pattern.SelectedValue);

                                bannerSteps.Add(temp4);

                                if (checkBoxStep5.Checked)
                                {
                                    BannerStep temp5 = new BannerStep();
                                    temp5.BannerStepNumber = 5;
                                    temp5.BannerStepColorID = Convert.ToString(comboBoxBannerStep5Color.SelectedValue);
                                    temp5.BannerStepPatternID = Convert.ToString(comboBoxBannerStep5Pattern.SelectedValue);

                                    bannerSteps.Add(temp5);

                                    if (checkBoxStep6.Checked)
                                    {
                                        BannerStep temp6 = new BannerStep();
                                        temp6.BannerStepNumber = 6;
                                        temp6.BannerStepColorID = Convert.ToString(comboBoxBannerStep6Color.SelectedValue);
                                        temp6.BannerStepPatternID = Convert.ToString(comboBoxBannerStep6Pattern.SelectedValue);

                                        bannerSteps.Add(temp6);
                                    }
                                }
                            }
                        }
                    }
                } //end of banner steps

                controller.CreateBanner(bannerName, bannerColor, bannerPicture, bannerCategoryId, secondaryCategoryIDs, bannerSteps);

                ShowSuccess("Banner probably added"); //temp
                //could be nice to clear all feilds here when the banner is created
            }

        } //end off add banner button



    }
}
