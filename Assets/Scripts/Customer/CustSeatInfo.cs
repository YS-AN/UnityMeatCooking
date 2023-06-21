using System;

public class CustSeatInfo
{
	private Seat _seat;
	public Seat Seat { get { return _seat; } }

	private Chair _chair;
	public Chair Chair { get { return _chair; } }

	//private Chair _anoChair;
	//public Chair AnoChair { get { return _anoChair; } }

	public void Init(Seat seat, int num)
	{
		_seat = seat;
		_chair = _seat.Chairs[num];
		//_anoChair = _seat.Chairs[num == 0 ? 1 : 0];
	}
}