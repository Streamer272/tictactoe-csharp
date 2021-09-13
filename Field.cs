using System;
using System.Collections.Generic;

namespace tictactoe
{
    public class Field
    {
        private readonly List<Box> _boxes = new (new Box[9]);
        private readonly List<Team> _teams;

        public const int Up = 0;
        public const int Right = 1;
        public const int Down = 2;
        public const int Left = 3;

        public Field(List<Team> teams)
        {
            for (int i = 0; i < 9; i++)
            {
                Box box = new Box();
                if (i == 4)
                {
                    box.Selected = true;
                }

                _boxes[i] = box;
            }

            _teams = teams;
        }

        private Team GetTeamById(int id)
        {
            foreach (Team team in _teams)
            {
                if (team.Id == id)
                {
                    return team;
                }
            }

            return null;
        }

        private string GetSymbolForBox(Box box)
        {
            return box.Value == Box.Empty ? "0" : GetTeamById(box.TeamId).Signature;
        }

        public override string ToString()
        {
            string output = "";
            string horizontalSeparator = "-------------\n";
            string horizontalSpace = "";
            string verticalSpace = "";
            for (int i = 0; i < (Console.WindowWidth - 13) / 2 - (Console.WindowWidth - 13) / 2 % 1; i++) horizontalSpace += " ";
            for (int i = 0; i < (Console.WindowHeight - 7) / 2 - (Console.WindowHeight - 7) / 2 % 1; i++) verticalSpace += "\n";

            horizontalSeparator = horizontalSpace + horizontalSeparator;

            output += verticalSpace;
            output += horizontalSeparator;

            for (int i = 0; i < 9; i++)
            {
                switch (i % 3)
                {
                    case 0:
                        output += horizontalSpace;

                        if (_boxes[i].Selected) output += "[ ";
                        else output += "| ";

                        output += GetSymbolForBox(_boxes[i]);

                        break;
                    case 1:
                        if (_boxes[i].Selected) output += " [ ";
                        else if (_boxes[i - 1].Selected) output += " ] ";
                        else output += " | ";

                        output += GetSymbolForBox(_boxes[i]);

                        if (_boxes[i].Selected) output += " ] ";
                        else if (_boxes[i + 1].Selected) output += " [ ";
                        else output += " | ";

                        break;
                    case 2:
                        output += GetSymbolForBox(_boxes[i]);

                        if (_boxes[i].Selected) output += " ]\n";
                        else output += " |\n";

                        if (i != 8) output += horizontalSeparator;

                        break;
                }
            }

            output += horizontalSeparator;
            output += verticalSpace;

            return output;
        }

        public void Select(int dir)
        {
            for (int i = 0; i < 9; i++)
            {
                if (_boxes[i].Selected)
                {
                    _boxes[i].Selected = false;

                    switch (dir)
                    {
                        case Up:
                            if (i - 3 >= 0) _boxes[i - 3].Selected = true;
                            break;
                        case Right:
                            if (i + 1 <= 8) _boxes[i + 1].Selected = true;
                            break;
                        case Down:
                            if (i + 3 <= 8) _boxes[i + 3].Selected = true;
                            break;
                        case Left:
                            if (i - 1 >= 0) _boxes[i - 1].Selected = true;
                            break;
                    }
                    
                    return;
                }
            }
        }

        public bool IsFieldFull()
        {
            foreach (Box box in _boxes)
            {
                if (box.Value == Box.Empty)
                {
                    return false;
                }
            }

            return true;
        }

        private int? GetTeamIdOfBox(Box box)
        {
            return box.Value == Box.Empty ? null : box.TeamId;
        }

        private bool DoBoxesHaveSameValue(Box box1, Box box2, Box box3)
        {
            return
                GetTeamIdOfBox(box1) == GetTeamIdOfBox(box2) &&
                GetTeamIdOfBox(box2) == GetTeamIdOfBox(box3) &&
                GetTeamIdOfBox(box3) != null;
        }

        public Team GetWinner()
        {
            int? teamId = null;

            // rows
            if (DoBoxesHaveSameValue(_boxes[0], _boxes[1], _boxes[2]))
            {
                teamId = GetTeamIdOfBox(_boxes[0]);
            }
            if (DoBoxesHaveSameValue(_boxes[3], _boxes[4], _boxes[5]))
            {
                teamId = GetTeamIdOfBox(_boxes[3]);
            }
            if (DoBoxesHaveSameValue(_boxes[6], _boxes[7], _boxes[8]))
            {
                teamId = GetTeamIdOfBox(_boxes[6]);
            }

            // columns
            if (DoBoxesHaveSameValue(_boxes[0], _boxes[3], _boxes[6]))
            {
                teamId = GetTeamIdOfBox(_boxes[0]);
            }
            if (DoBoxesHaveSameValue(_boxes[1], _boxes[4], _boxes[7]))
            {
                teamId = GetTeamIdOfBox(_boxes[1]);
            }
            if (DoBoxesHaveSameValue(_boxes[2], _boxes[5], _boxes[8]))
            {
                teamId = GetTeamIdOfBox(_boxes[2]);
            }

            // other
            if (DoBoxesHaveSameValue(_boxes[0], _boxes[4], _boxes[8]))
            {
                teamId = GetTeamIdOfBox(_boxes[0]);
            }
            if (DoBoxesHaveSameValue(_boxes[2], _boxes[4], _boxes[6]))
            {
                teamId = GetTeamIdOfBox(_boxes[3]);
            }

            if (teamId != null)
            {
                return GetTeamById(Int32.Parse(teamId.ToString()));
            }

            return null;
        }

        public bool MarkSelected(Team team)
        {
            for (int i = 0; i < 9; i++)
            {
                if (_boxes[i].Selected && _boxes[i].Value == Box.Empty)
                {
                    _boxes[i].Value = Box.Used;
                    _boxes[i].TeamId = team.Id;

                    return true;
                }
            }

            return false;
        }

        public void SelectRandomAs(Team team)
        {
            if (IsFieldFull()) return;
            if (GetWinner() != null) return;

            while (true)
            {
                int random = Bot.GetRandomPosition(9);

                if (_boxes[random].Value == Box.Empty)
                {
                    _boxes[random].Value = Box.Used;
                    _boxes[random].TeamId = team.Id;

                    return;
                }
            }
        }
    }
}
