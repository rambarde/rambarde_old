using UnityEngine;

namespace Music {
    public class MusicInput {

        public static int GetInput() {
            if (Input.GetKeyDown(KeyCode.Alpha1)) {
                return 1;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2)) {
                return 2;
            }
            if (Input.GetKeyDown(KeyCode.Alpha3)) {
                return 3;
            }
            if (Input.GetKeyDown(KeyCode.Alpha4)) {
                return 4;
            }

            return 0;
        }
    }
}