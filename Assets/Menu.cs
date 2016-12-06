<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> f7f9e00c37d4bf67c3bb83f2138e54e1be5d3302
>>>>>>> c9fa15492ef5482d2d9f2e875b473434d9719d27
﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
=======
﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
>>>>>>> 54196b18866191fde8018fcfcdc0d22de1d9576c
>>>>>>> f7f9e00c37d4bf67c3bb83f2138e54e1be5d3302
>>>>>>> c9fa15492ef5482d2d9f2e875b473434d9719d27
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
<<<<<<< HEAD
    }
}
=======
<<<<<<< HEAD
    }
}
=======
    }
}
>>>>>>> 54196b18866191fde8018fcfcdc0d22de1d9576c
>>>>>>> f7f9e00c37d4bf67c3bb83f2138e54e1be5d3302
>>>>>>> c9fa15492ef5482d2d9f2e875b473434d9719d27
