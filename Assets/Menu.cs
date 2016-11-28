<<<<<<< HEAD
﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
=======
﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
>>>>>>> 54196b18866191fde8018fcfcdc0d22de1d9576c
public class Menu : MonoBehaviour {
    public Button yourButton;
    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        Application.LoadLevel("01");
<<<<<<< HEAD
    }
}
=======
    }
}
>>>>>>> 54196b18866191fde8018fcfcdc0d22de1d9576c
