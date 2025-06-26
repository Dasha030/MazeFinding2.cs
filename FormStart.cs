using System;
using System.Windows.Forms;

namespace MazeWinForms
{
    public partial class FormStart : Form
    {
        public Maze MazeInstance { get; private set; }
        public Algorithm SelectedAlgorithm { get; private set; }
        public RunMode SelectedRunMode { get; private set; }

        public FormStart()
        {
            InitializeComponent();

            comboBoxRunMode.Items.Add("Автоматичний");
            comboBoxRunMode.Items.Add("Покроковий");
            comboBoxRunMode.SelectedIndex = 0;

            numericUpDownDifficulty.Minimum = 1;
            numericUpDownDifficulty.Maximum = 4;

            comboBoxAlgorithm.Items.Clear();
            comboBoxAlgorithm.Items.Add("Дейкстри");
            comboBoxAlgorithm.Items.Add("A* (манхеттенська евристика)");
            comboBoxAlgorithm.Items.Add("A* (евклідова евристика)");
            comboBoxAlgorithm.SelectedIndex = 0;
        }

        // Кнопка "Згенерувати початок і кінець"
        private void buttonGenerateStartFinish_Click(object sender, EventArgs e)
        {
            int length = (int)numericUpDownLength.Value;
            int width = (int)numericUpDownWidth.Value;
            int difficulty = (int)numericUpDownDifficulty.Value;
            Algorithm algo = (Algorithm)comboBoxAlgorithm.SelectedIndex;
            RunMode runMode = (RunMode)comboBoxRunMode.SelectedIndex;

            Maze maze;
            int maxTries = 300;
            int tries = 0;
            do
            {
                maze = new Maze();
                maze.GenerateRandom(length, width, difficulty);
                tries++;
                Application.DoEvents();
            }
            while (tries < maxTries && !maze.HasFreeCell());

            if (!maze.HasFreeCell())
            {
                MessageBox.Show("Не вдалося згенерувати лабіринт з вільними клітинками.");
                return;
            }

            // Відкриваємо одразу форму вибору старту/фінішу, після вибору вона запускає MainForm і закриває програму
            var chooser = new StartFinishChooser(maze, algo, runMode);
            chooser.Show();
            this.Hide();
        }

        // Якщо потрібні ці кнопки, додайте відповідні кнопки у форму!
        private void buttonRandom_Click(object sender, EventArgs e)
        {
            int length = (int)numericUpDownLength.Value;
            int width = (int)numericUpDownWidth.Value;
            int difficulty = (int)numericUpDownDifficulty.Value;
            Algorithm algo = (Algorithm)comboBoxAlgorithm.SelectedIndex;
            RunMode runMode = (RunMode)comboBoxRunMode.SelectedIndex;

            Maze maze;
            PathResult result;
            int maxTries = 300;
            int tries = 0;
            do
            {
                maze = new Maze();
                maze.GenerateRandom(length, width, difficulty);
                result = PathFinder.FindPath(maze, algo);
                tries++;
                Application.DoEvents();
            }
            while ((result.Path == null || result.Path.Count == 0) && tries < maxTries);

            if (result.Path == null || result.Path.Count == 0)
            {
                MessageBox.Show("У цьому випадку розв'язку немає.");
                return;
            }

            this.MazeInstance = maze;
            this.SelectedAlgorithm = algo;
            this.SelectedRunMode = runMode;
            OpenMainForm();
        }

        private void buttonLoadMaze_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Maze files (*.txt)|*.txt|All files (*.*)|*.*";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Maze maze = new Maze();
                if (maze.LoadFromFile(dlg.FileName))
                {
                    this.MazeInstance = maze;
                    this.SelectedAlgorithm = (Algorithm)comboBoxAlgorithm.SelectedIndex;
                    this.SelectedRunMode = (RunMode)comboBoxRunMode.SelectedIndex;
                    MessageBox.Show("Лабіринт завантажено.");
                    OpenMainForm();
                }
                else
                {
                    MessageBox.Show("Помилка завантаження лабіринту.");
                }
            }
        }

        private void OpenMainForm()
        {
            MainForm mainForm = new MainForm(MazeInstance, SelectedAlgorithm, SelectedRunMode);
            mainForm.Show();
        }
    }
}