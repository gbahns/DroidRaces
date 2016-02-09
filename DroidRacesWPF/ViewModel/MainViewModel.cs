using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Model;
using System;
using System.Windows;

namespace DroidRacesWPF.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
		public Board Board { get; private set; }
		public string[] Instructions { get; private set; }
		public Instruction Droid1SelectedInstruction { get; set; }
		public RelayCommand Droid1AddInstructionCommand { get; set; }
		public RelayCommand ExecutePhaseCommand { get; set; }
		public Droid Droid1 { get; set; }

		public MainViewModel()
        {
			////if (IsInDesignMode)
			////{
			////    // Code runs in Blend --> create design time data.
			////}
			////else
			////{
			////    // Code runs "for real"
			////}
			Instructions = Enum.GetNames(typeof(Instruction));
			Board = new Board();
			Droid1 = new Droid();
			Droid1.color = DroidColor.Yellow;
			Droid1.position = new Position(1, 1);
			Droid1.direction = Direction.Right;
			Droid1.Instructions.Add(Instruction.Move1);
			Droid1.Instructions.Add(Instruction.Move1);
			//Board.boardElements.Add(Droid1);
			Board.Droids.Add(Droid1);

			ExecutePhaseCommand = new RelayCommand(() => ExecutePhaseButtonAction());

			Droid1AddInstructionCommand = new RelayCommand(
				() => {
					//MessageBox.Show(String.Format("You you want add this instruction: {0}", Droid1SelectedInstruction));
					Droid1.Instructions.Add(Droid1SelectedInstruction);
				},
				() => Droid1.Instructions.Count < 5)
				;
		}

		private void ExecutePhaseButtonAction()
		{
			//var droidDisplayElements = Board.Droids.Where(e => e.boardElement.GetType() == typeof(Droid));
			//var droids = board.boardElements.Where(e => e.GetType() == typeof(Droid)).Cast<Droid>();
			foreach (var droid in Board.Droids)
			{
				//var droid = (Droid)droidDisplayElement.boardElement;
				//var shape = (DroidShape)droidDisplayElement.displayElement;
				var instructions = droid.Instructions;
				if (instructions.Count > 0)
				{
					//var instruction = droid.instructions.Dequeue();
					droid.ExecuteInstruction();
					//Grid.SetColumn(shape, droid.position.X);
					//Grid.SetRow(shape, droid.position.Y);
					//shape.SetDirection(droid.direction);
				}
			}
		}

	}
}