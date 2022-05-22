using TMPro;
using UnityEngine;

namespace Game
{
    public class GameplayUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text laserCharges;
        [SerializeField] private TMP_Text laserCooldown;
        [SerializeField] private TMP_Text shipCoordinatesX;
        [SerializeField] private TMP_Text shipCoordinatesY;
        [SerializeField] private TMP_Text shipTurnAngle;
        [SerializeField] private TMP_Text shipSpeed;

        public TMP_Text ShipSpeed => shipSpeed;
        public TMP_Text ShipTurnAngle => shipTurnAngle;
        public TMP_Text ShipCoordinatesX => shipCoordinatesX;
        public TMP_Text ShipCoordinatesY => shipCoordinatesY;
        public TMP_Text LaserCharges => laserCharges;
        public TMP_Text LaserCooldown => laserCooldown;
    }
}