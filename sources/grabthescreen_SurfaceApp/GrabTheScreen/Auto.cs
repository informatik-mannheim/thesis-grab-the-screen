﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrabTheScreen
{
    public class Auto
    {
        // Du kannst die Eigenschaften des Modells mit C#-Properties darstellen.
        // Properties kannst du wie ein Feld benutzen, aber du musst die 
        // get- und set-Methoden nicht von Hand schreiben; das private Feld wird automatisch angelegt!

        // Bsp.: (ersetzt den private String model und die get- und set-Methode mit einer Zeile code!)
        // public String model { get; set; }

        public String model;
        public String modelDescription;
        public String price;
        public String source;
        public String id;
        public String color;
        public Boolean status;

        public Auto(String car_model, String car_modelDescription, String car_price, String car_source, String car_id, String car_color, Boolean car_status)
        {
            this.model = car_model;
            this.modelDescription = car_modelDescription;
            this.price = car_price;
            this.source = car_source;
            this.id = car_id;
            this.color = car_color;
            this.status = car_status;
        }

        public Auto()
        {

        }

        // ----- Getter & Setter 

        public void setModel(String model)
        {
            this.model = model;
        }

        public String getModel()
        {
            return this.model;
        }

        public void setModelDescription(String modelDescription)
        {
            this.modelDescription = modelDescription;
        }

        public String getModelDescription()
        {
            return this.modelDescription;
        }

        public void setPrice(String price)
        {
            this.price = price;
        }

        public String getPrice()
        {
            return this.price;
        }

        public void setSource(String source)
        {
            this.source = source;
        }

        public String getSource()
        {
            return this.source;
        }


        public void setId(String id)
        {
            this.id = id;
        }

        public String getId()
        {
            return this.id;
        }

        public String getColor()
        {
            return this.color;
        }

        public void setColor(String color) { 
            this.color = color;
        }

        public Boolean getStatus() {
            return this.status;
        }

        public void setStatus(Boolean status) {
            this.status = status;
        }
    }


}
