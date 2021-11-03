using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorCycler : MonoBehaviour
{
    public Color[] Colors;
    public float Speed = 5;
    int _currentIndex = 0;
    Camera _cam;
    public bool _shouldChange = false;

    void Start()
    {
        _cam = GetComponent<Camera>();

        _currentIndex = 0;
        SetColor(Colors[_currentIndex]);
    }

    public void SetColor(Color color)
    {
        _cam.backgroundColor = color;
    }

    public void Cycle()
    {
        _shouldChange = true;
    }

    void Update()
    {
        if (_shouldChange)
        {
            var startColor = _cam.backgroundColor;

            var endColor = Colors[0];
            if (_currentIndex + 1 < Colors.Length)
            {
                endColor = Colors[_currentIndex + 1];
            }


            var newColor = Color.Lerp(startColor, endColor, Time.deltaTime * Speed);
            SetColor(newColor);

            if (newColor == endColor)
            {
                _shouldChange = false;
                if (_currentIndex + 1 < Colors.Length)
                {
                    _currentIndex++;
                }
                else
                {
                    _currentIndex = 0;
                }
            }
        }
    }
}
